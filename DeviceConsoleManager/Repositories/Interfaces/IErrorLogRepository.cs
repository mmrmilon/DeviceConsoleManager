using DeviceConsoleManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceConsoleManager.Repositories.Interfaces
{
    public interface IErrorLogRepository
    {
        ErrorLog Insert(ErrorLog model);
    }
}
