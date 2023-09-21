using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevsApp.Core.Contracts.Models;
using FluentFTP;

namespace DevsApp.Core.Contracts.Services;

public interface IFluentFTPService
{
    public FtpClient CreateFtpClient(IAddress address,IFtpLogger ftpLogger = null);

    public AsyncFtpClient CreateAsyncFtpClient(IAddress address, IFtpLogger ftpLogger = null);
}
