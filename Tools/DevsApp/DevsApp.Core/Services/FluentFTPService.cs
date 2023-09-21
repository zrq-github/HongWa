using DevsApp.Core.Contracts.Models;
using DevsApp.Core.Contracts.Services;
using FluentFTP;

namespace DevsApp.Core.Services;

public class FluentFtpService : IFluentFTPService
{
    #region IFluentFTPService Members

    public FtpClient CreateFtpClient(IAddress address, IFtpLogger ftpLogger = null)
    {
        var username = address.Username;
        var password = address.Password;
        var port = address.Port;
        var host = address.Host;
        FtpConfig ftpConfig = new()
        {
            EncryptionMode = FtpEncryptionMode.None,
            ValidateAnyCertificate = true
        };
        var ftpClient = new FtpClient(host, username, password, port, ftpConfig);
        return ftpClient;
    }

    public AsyncFtpClient CreateAsyncFtpClient(IAddress address, IFtpLogger ftpLogger = null)
    {
        var username = address.Username;
        var password = address.Password;
        var port = address.Port;
        var host = address.Host;
        FtpConfig ftpConfig = new()
        {
            EncryptionMode = FtpEncryptionMode.None,
            ValidateAnyCertificate = true
        };
        var ftpClient = new AsyncFtpClient(host, username, password, port, ftpConfig, ftpLogger);
        return ftpClient;
    }
    #endregion
}