using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeviceConsoleManager.DataModel;
using DeviceConsoleManager.Models;
using DeviceConsoleManager.Repositories.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PCSC;
using PCSC.Iso7816;

namespace DeviceConsoleManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserCardController : ControllerBase
    {
		private readonly IUserCardRepository userCardRepository;
		private readonly IHostingEnvironment hostingEnvironment;
		private readonly IPrefixRepository prefixRepository;

		public UserCardController(IUserCardRepository userCardRepository, IHostingEnvironment hostingEnvironment,IPrefixRepository prefixRepository)
		{
			this.userCardRepository = userCardRepository;
			this.hostingEnvironment = hostingEnvironment;
			this.prefixRepository = prefixRepository;
		}
		[HttpGet]
		[Route("GetAll")]
		public ActionResult GetCardUsers()
		{
			try
			{
				var result = this.userCardRepository.GetCardUsers();
				return Ok(new { success = true, data = result });
			}
			catch (Exception ex)
			{
				return Ok(new { success = false, errorMessage = ex.GetBaseException() });
			}

		}

		[HttpGet]
		[Route("GetAllCardUsers")]
		public ActionResult GetAllCardUsers()
		{
			try
			{
				var result = this.userCardRepository.GetCardUsers();
				return Ok(result);
			}
			catch (Exception ex)
			{
				return Ok(new { success = false, errorMessage = ex.GetBaseException() });
			}

		}

		[HttpGet]
		[Route("GetPrefix")]
		public ActionResult GetPrefix()
		{
			try
			{
				var result = this.prefixRepository.GeneratePrefix();
				return Ok(new { success = true, data = result });
			}
			catch (Exception ex)
			{
				return Ok(new { success = false, errorMessage = ex.GetBaseException() });
			}

		}

		[HttpGet]
		[Route("GetCardPrefix")]
		public ActionResult GetCardPrefix()
		{
			try
			{
				var result = prefixRepository.GeneratePrefix();
				return Ok(new { value = result });
			}
			catch (Exception ex)
			{
				return Ok(new { success = false, errorMessage = ex.GetBaseException() });
			}
		}


		[HttpGet]
		[Route("GetAllUsers")]
		public ActionResult GetAllUsers()
		{
			try
			{
				var result = this.userCardRepository.GetAllUsersNotHasCard();
				return Ok(new { success = true, data = result });
			}
			catch (Exception ex)
			{
				return Ok(new { success = false, errorMessage = ex.GetBaseException() });
			}

		}

		[HttpGet]
		[Route("GetAllCardUsers")]
		public ActionResult GetAllUsersNotHasCard()
		{
			try
			{
				var result = this.userCardRepository.GetAllUsersNotHasCard();
				return Ok(result);
			}
			catch (Exception ex)
			{
				return Ok(new { success = false, errorMessage = ex.GetBaseException() });
			}

		}


		[HttpPost]
		[Route("WriteToCard")]
		public ActionResult WriteToCard(UserCards userCards,string date)
		{
			try
			{
				SCardContext hContext = new SCardContext();
				hContext.Establish(SCardScope.System);
				//// Retrieve the list of Smartcard readers
				string[] szReaders = hContext.GetReaders();
				SCardReader reader = new SCardReader(hContext);

				//// Connect to the card
				SCardError err = reader.Connect(szReaders[0],
				SCardShareMode.Shared,
				SCardProtocol.T0 | SCardProtocol.T1);

				var code = userCards.ChipCardNo.Substring(Math.Max(0, userCards.ChipCardNo.Length - 4));

				byte[] pbRecvBuffer = new byte[256];
				byte[] pbRecvBuffer1 = new byte[256];
				byte[] pbRecvBuffer2 = new byte[256];
				byte[] pbRecvBuffer3 = new byte[256];
				byte[] databytes = Encoding.ASCII.GetBytes(code.ToString());
				byte[] count = Encoding.ASCII.GetBytes(databytes.Length.ToString());
				var apdu = new CommandApdu(IsoCase.Case3Extended, reader.ActiveProtocol)
				{
					CLA = 0xFF, 
					INS = 0xD0,
					P1 = 0x00, 
					P2 = 0x69,
					Data = databytes
				};


				//// Send SELECT command
				byte[] selecteCommand = new byte[] { 0xFF, 0xA4, 0x00, 0x00, 0x01, 0x06 };
				byte[] password = new byte[] { 0xFF, 0x20, 0x00, 0x00, 0x03, 0xFF, 0xFF, 0xFF };
				byte[] writeMemmoryCard = apdu.ToArray();

				reader.Transmit(selecteCommand, ref pbRecvBuffer);
				reader.Transmit(password, ref pbRecvBuffer1);
				reader.Transmit(writeMemmoryCard, ref pbRecvBuffer2);
				
		
				hContext.Release();
				userCards.ExpairDate = Convert.ToDateTime(date);
				userCardRepository.WriteCardUsers(userCards);
				return Ok(new { success = true, successMessage  = "Write Sucessfully" });
			}
			catch (Exception ex)
			{
				return Ok(new { success = false, errorMessage = ex.GetBaseException() });
			}
			
		}


		[HttpPost]
		[Route("UpdateCustomerCardInfo")]
		public ActionResult UpdateCustomerCardInfo(UserCards userCards)
		{
			try
			{
				userCardRepository.WriteCardUsers(userCards);
				return Ok(new { success = true, successMessage = "Write Sucessfully" });
			}
			catch (Exception ex)
			{
				return Ok(new { success = false, errorMessage = ex.GetBaseException() });
			}

		}

        [HttpPost]
        [Route("Update")]
        public ActionResult Update(UserCards card)
        {
            try
            {
                userCardRepository.Update(card);
                return Ok(new { success = true, successMessage = "Write Sucessfully" });
            }
            catch (Exception ex)
            {
                return Ok(new { success = false, errorMessage = ex.GetBaseException() });
            }

        }

        [HttpPost]
        [Route("Delete")]
        public ActionResult Delete(long chipCardId)
        {
            try
            {
                var result = userCardRepository.Delete(chipCardId);

                return Ok(new { success = true, data = result });
            }
            catch (Exception ex)
            {
                return Ok(new { success = false, errorMessage = ex.GetBaseException() });
            }
        }

    }
}