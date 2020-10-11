using DeviceConsoleManager.DataModel;
using DeviceConsoleManager.Models;
using DeviceConsoleManager.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Transactions;

namespace DeviceConsoleManager.Repositories.Implementation
{
    public class DeviceRepository : IDeviceRepository
    {
        private readonly DatabaseContext context;
        private readonly IDeviceSerailNumberRepository deviceSerailNumberRepository;
        private readonly IErrorLogRepository errorLogRepository;

        public DeviceRepository(DatabaseContext context, IDeviceSerailNumberRepository deviceSerailNumberRepository, IErrorLogRepository errorLogRepository)
        {
            this.context = context;
            this.deviceSerailNumberRepository = deviceSerailNumberRepository;
            this.errorLogRepository = errorLogRepository;
        }

        public bool Delete(long deviceInfoId)
        {

            this.DeleteGroupByUserId(deviceInfoId);
            var user = context.DeviceInformations.Where(x => x.Id == deviceInfoId).FirstOrDefault();
            context.Entry(user).State = EntityState.Deleted;
            var result = context.SaveChanges();
            return result > 0;
        }

        private void DeleteGroupByUserId(long deviceId)
        {
            var device = context.UserGroups.Where(x => x.DeviceId == deviceId).FirstOrDefault();
            if (device != null)
            {
                context.Entry(device).State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public List<DeviceInformationsDataModel> GetAll()
        {
            var result = (from device in context.DeviceInformations
                          join dl in context.DeviceLocation on device.DeviceLocationId equals dl.Id
                          select new DeviceInformationsDataModel()
                          {
                              Id = device.Id,
                              DeviceCode = device.DeviceCode,
                              DeviceName = device.DeviceName,
                              DeviceLocationId = device.DeviceLocationId,
                              DeviceLocationName = dl.LocationName,
                              DeivceToken = device.DeivceToken
                          }).ToList();
            return result;
        }

        public bool Save(DeviceInformations deviceInformations)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    var model = new DeviceInformations
                    {
                        DeviceGuid = Guid.NewGuid().ToString(),
                        DeviceLocationId = deviceInformations.DeviceLocationId,
                        DeviceName = deviceInformations.DeviceName,
                        DeivceToken = Guid.NewGuid().ToString().ToUpper(), //Regex.Replace(Guid.NewGuid().ToString(), "[^0-9a-zA-Z]", string.Empty).ToUpper(),
                        DeviceCode = deviceInformations.DeviceCode,
                        CreatedOn = DateTime.Now,
                        UpdatedOn = DateTime.Now
                    };

                    context.Add(model);
                    context.SaveChanges();

                    //update device serial number
                    this.deviceSerailNumberRepository.SetDeviceSerialNumber(model.DeviceCode);

                    //if all success - commit first step (second step was success completed)
                    transaction.Commit();

                    return true;
                }
                catch (Exception Ex)
                {
                    errorLogRepository.Insert(new ErrorLog
                    {
                        EvenName = "Save Device Informations Failed",
                        ErrorDetails = Ex.GetBaseException().Message
                    });
                    //if we have error - rollback first step (second step not be able accepted)
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public bool Update(DeviceInformations deviceInformations)
        {
            var result = (
                    from di in context.DeviceInformations
                    where di.Id == deviceInformations.Id
                    select di
                ).FirstOrDefault();
            result.UpdatedOn = DateTime.Now;
            result.DeviceLocationId = deviceInformations.DeviceLocationId;
            result.DeviceCode = deviceInformations.DeviceCode;
            result.DeviceName = deviceInformations.DeviceName;

            context.Entry(result).State = EntityState.Modified;
            return context.SaveChanges() > 0;
        }

        public UserReqeustStatusDataModel checkUserhasDevicePermission(string device_token, string chip_id)
        {
            var userRequestStatus = new UserReqeustStatusDataModel
            {
                IsSuccess = false,
                IsVaildUser = false,
                AvailableBalance = 0.0,
                CustomerName = string.Empty
            };

            var user_data = (from ua in context.UserAccounts
                             join u in context.User on ua.UserId equals u.Id
                             join ug in context.UserGroups on ua.UserId equals ug.UserId
                             join d in context.DeviceInformations on ug.DeviceId equals d.Id
                             join uc in context.UserCards on ua.UserId equals uc.UserId
                             where d.DeivceToken == device_token && uc.ChipCardNo.EndsWith(chip_id)
                             select new
                             {
                                 customerName = u.UserName,
                                 available_balance = ua.AvailableBalance
                             }).FirstOrDefault();

            if (user_data != null)
            {
                userRequestStatus.IsSuccess = true;
                userRequestStatus.IsVaildUser = true;
                userRequestStatus.AvailableBalance = user_data.available_balance;
                userRequestStatus.CustomerName = user_data.customerName;
            }

            return userRequestStatus;
        }

        public UserReqeustStatusDataModel userChipCardValidation(string device_token, string chip_id)
        {
            var result = this.checkUserhasDevicePermission(device_token, chip_id);
            if (result.IsVaildUser == false)
            {
                result.Unit = 0;
                result.UnitRate = 0;

                return result;
            }
            else
            {
                var unitDetails = (from sc in context.UserServiceConfigurations
                                   join d in context.DeviceInformations on sc.DeviceId equals d.Id
                                   where d.DeivceToken == device_token
                                   select sc).FirstOrDefault();
                if (unitDetails != null)
                {
                    result.Unit = unitDetails.Unit;
                    result.UnitRate = unitDetails.UnitRate;
                }
                return result;
            }
        }

        public UserServiceConfigurations GetDeviceServiceConfiguration()
        {
            var userServiceConfigurations = context.UserServiceConfigurations.FirstOrDefault();
            return userServiceConfigurations;
        }

        public bool SeviceEnd(string chip_id, string device_token, string start_time, string end_time, double service_amount)
        {
            try
            {
                errorLogRepository.Insert(new ErrorLog
                {
                    EvenName = "Callign of ServiceEnd API with Params",
                    ErrorDetails = "Chip ID:" + chip_id + ", Device Token:" + device_token + ", Start Time:" + start_time + ", End Time:" + end_time + ", Service Amount: " + service_amount.ToString()
                });

                var user_id = context.UserCards.Where(x => x.ChipCardNo.EndsWith(chip_id)).FirstOrDefault().UserId;
                var unitDetails = (from sc in context.UserServiceConfigurations
                                   join d in context.DeviceInformations on sc.DeviceId equals d.Id
                                   where d.DeivceToken == device_token
                                   select sc).FirstOrDefault();

                //Calculate Service Amount
                DateTime startTime1 = Convert.ToDateTime(start_time);
                DateTime endTime1 = Convert.ToDateTime(end_time);
                var totalServiceTime = (decimal)endTime1.Subtract(startTime1).TotalMinutes;
                service_amount = unitDetails.UnitRate * (Convert.ToDouble(totalServiceTime) / unitDetails.Unit);

                var data = this.checkUserhasDevicePermission(device_token, chip_id);
                if (data.IsVaildUser == false)
                {
                    errorLogRepository.Insert(new ErrorLog
                    {
                        EvenName = "IsVaildUser of ServiceEnd",
                        ErrorDetails = device_token + ", " + chip_id
                    });

                    return false;
                }
                else
                {
                    using (var transaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            //Store Service Details
                            long service_id = this.InsertIntoService(device_token, user_id, start_time, end_time, service_amount);

                            //Store Transaction Details
                            InsertIntoTransiction(service_id, service_amount);

                            //Update User Account Balance
                            double total_amount = data.AvailableBalance - service_amount;
                            this.InsertIntoUserAccount(user_id, total_amount);

                            //Update Device Status
                            var result = UpdateDeviceStatus(device_token, user_id);

                            //Insert Success Log
                            errorLogRepository.Insert(new ErrorLog
                            {
                                EvenName = "ServiceEnd Successfull",
                                ErrorDetails = "Chip ID:" + chip_id + ", Device Token:" + device_token + ", TotalServiceTime:" + totalServiceTime.ToString() + ", Service Amount:" + service_amount.ToString()
                            });

                            //if all success - commit first step (second step was success completed)
                            transaction.Commit();

                            return true;
                        }
                        catch (Exception Ex)
                        {
                            errorLogRepository.Insert(new ErrorLog
                            {
                                EvenName = "Transaction Error of ServiceEnd",
                                ErrorDetails = Ex.GetBaseException().Message
                            });
                            //if we have error - rollback first step (second step not be able accepted)
                            transaction.Rollback();
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                errorLogRepository.Insert(new ErrorLog
                {
                    EvenName = "Most Outer Try Catch Block of ServiceEnd",
                    ErrorDetails = Ex.GetBaseException().Message
                });

                throw;
            }

            return false;
        }

        public bool ServiceEnd(string chip_id, string device_token, string start_time, string end_time, double service_amount)
        {
            try
            {
                errorLogRepository.Insert(new ErrorLog
                {
                    EvenName = "Callign of ServiceEnd API with Params",
                    ErrorDetails = "Chip ID:" + chip_id + ", Device Token:" + device_token + ", Start Time:" + start_time + ", End Time:" + end_time + ", Service Amount: " + service_amount.ToString()
                });

                var user_id = context.UserCards.Where(x => x.ChipCardNo.EndsWith(chip_id)).FirstOrDefault().UserId;
                var unitDetails = (from sc in context.UserServiceConfigurations
                                   join d in context.DeviceInformations on sc.DeviceId equals d.Id
                                   where d.DeivceToken == device_token
                                   select sc).FirstOrDefault();

                //Calculate Service Amount
                DateTime startTime1 = Convert.ToDateTime(start_time);
                DateTime endTime1 = Convert.ToDateTime(end_time);
                var totalServiceTime = (decimal)endTime1.Subtract(startTime1).TotalMinutes;
                service_amount = unitDetails.UnitRate * (Convert.ToDouble(totalServiceTime) / unitDetails.Unit);

                var data = this.checkUserhasDevicePermission(device_token, chip_id);
                if (data.IsVaildUser == false)
                {
                    errorLogRepository.Insert(new ErrorLog
                    {
                        EvenName = "IsVaildUser of ServiceEnd",
                        ErrorDetails = device_token + ", " + chip_id
                    });

                    return false;
                }
                else
                {
                    using (var transaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            //Store Service Details
                            long service_id = this.InsertIntoService(device_token, user_id, start_time, end_time, service_amount);

                            //Store Transaction Details
                            InsertIntoTransiction(service_id, service_amount);

                            //Update User Account Balance
                            double total_amount = data.AvailableBalance - service_amount;
                            this.InsertIntoUserAccount(user_id, total_amount);
                            
                            //Update Device Status
                            var result = UpdateDeviceStatus(device_token, user_id);

                            //Insert Success Log
                            errorLogRepository.Insert(new ErrorLog
                            {
                                EvenName = "ServiceEnd Successfull",
                                ErrorDetails = "Chip ID:" + chip_id + ", Device Token:" + device_token + ", TotalServiceTime:" + totalServiceTime.ToString() + ", Service Amount:" + service_amount.ToString()
                            });

                            //if all success - commit first step (second step was success completed)
                            transaction.Commit();

                            return true;
                        }
                        catch (Exception Ex)
                        {
                            errorLogRepository.Insert(new ErrorLog
                            {
                                EvenName = "Transaction Error of ServiceEnd",
                                ErrorDetails = Ex.GetBaseException().Message
                            });
                            //if we have error - rollback first step (second step not be able accepted)
                            transaction.Rollback();
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                errorLogRepository.Insert(new ErrorLog
                {
                    EvenName = "Most Outer Try Catch Block of ServiceEnd",
                    ErrorDetails = Ex.GetBaseException().Message
                });

                throw;
            }

            return false;
        }

        public bool ServiceStart(string chip_id, string device_token, string start_time)
        {
            try
            {
                //Log
                errorLogRepository.Insert(new ErrorLog
                {
                    EvenName = "ServiceStart",
                    ErrorDetails = "Chip ID:" + chip_id + ", Device Token:" + device_token + ", Start Time:" + start_time
                });

                //Convert to DateTime
                DateTime startTime = Convert.ToDateTime(start_time);

                var data = checkUserhasDevicePermission(device_token, chip_id);
                if (data.IsVaildUser == false)
                {
                    errorLogRepository.Insert(new ErrorLog
                    {
                        EvenName = "In Vaild User to ServiceStart",
                        ErrorDetails = device_token + ", " + chip_id
                    });

                    return false;
                }
                else
                {
                    var userId = context.UserCards.Where(x => x.ChipCardNo.EndsWith(chip_id)).FirstOrDefault().UserId;
                    long deviceId = context.DeviceInformations.Where(x => x.DeivceToken == device_token).FirstOrDefault().Id;
                    var model = new DeviceStatus
                    {
                        DeviceId = deviceId,
                        UserId = userId,
                        IsActive = true,
                        CreatedOn = startTime
                    };
                    context.Add(model);
                    context.SaveChanges();

                    return true;
                }
            }
            catch (Exception ex)
            {
                errorLogRepository.Insert(new ErrorLog
                {
                    EvenName = "Most Outer Try Catch Block of ServiceStart",
                    ErrorDetails = ex.GetBaseException().Message
                });

                throw;
            }
        }

        public long InsertIntoService(string device_token, long user_id, string start_time, string end_time, double service_amount)
        {
            var userServices = new UserServices
            {
                DeviceId = context.DeviceInformations.Where(x => x.DeivceToken == device_token).FirstOrDefault().Id,
                UserId = user_id,
                StartTime = Convert.ToDateTime(start_time),
                EndTime = Convert.ToDateTime(end_time),
                IsActive = true,
                CreatedOn = DateTime.Now,
                UpdatedOn = DateTime.Now,
                TotalAmount = service_amount
            };
            context.Add(userServices);
            context.SaveChanges();

            return userServices.Id;
        }

        public bool InsertIntoTransiction(long service_id, double service_amount)
        {
            try
            {
                var transictions = new Transictions
                {
                    Guid = Guid.NewGuid().ToString().ToUpper(),
                    ServiceId = service_id,
                    TransictionAmount = service_amount,
                    TransictionDate = DateTime.Now,
                    IsActive = true,
                    CreatedOn = DateTime.Now,
                    UpdatedOn = DateTime.Now
                };

                context.Add(transictions);
                context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.GetBaseException().Message;
            }
            return false;
        }

        public bool InsertIntoUserAccount(long user_id, double total_amount)
        {
            var result = (from ua in context.UserAccounts
                          where ua.UserId == user_id
                          select ua).FirstOrDefault();

            result.AvailableBalance = total_amount;

            context.Entry(result).State = EntityState.Modified;
            return context.SaveChanges() > 0;
        }

        public UserServiceConfigurations GetUserServices()
        {
            var result = context.UserServiceConfigurations.FirstOrDefault();
            return result;
        }

        public bool UpdateUserService(UserServiceConfigurations userServicesconfig)
        {
            context.Entry(userServicesconfig).State = EntityState.Modified;
            return context.SaveChanges() > 0;
        }

        private long InsertServiceStartRecord(string device_token, long user_id, string start_time)
        {
            var model = new DeviceStatus
            {
                DeviceId = context.DeviceInformations.Where(x => x.DeivceToken == device_token).FirstOrDefault().Id,
                UserId = user_id,
                IsActive = true,
                CreatedOn = Convert.ToDateTime(start_time)
            };
            context.Add(model);
            context.SaveChanges();

            return model.Id;
        }

        private bool UpdateDeviceStatus(string deviceToken, long userId)
        {
            long deviceId = context.DeviceInformations.Where(x => x.DeivceToken == deviceToken).FirstOrDefault().Id;
            var result = context.DeviceStatus.Where(x => x.DeviceId == deviceId && x.UserId == userId && x.IsActive).OrderByDescending(x => x.Id).FirstOrDefault();
            if (result != null)
            {
                result.IsActive = false;
                result.UpdatedOn = DateTime.Now;
                context.Entry(result).State = EntityState.Modified;
                return context.SaveChanges() > 0;
            }
            else
                return false;
        }
    }
}
