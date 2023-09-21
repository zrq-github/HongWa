#nullable enable
using DevsApp.Core.Contracts.Services;
using DevsApp.Core.Models;

namespace DevsApp.Core.Services;

public class HWService : IHWService
{
    private Address? _ftpAddress;
    private Address? _httpAddress;

    #region IHWService Members

    public Address GetFtpAddress()
    {
        _ftpAddress ??= DefaultFtpAddress();
        return _ftpAddress;
    }

    public Address GetHttpAddress()
    {
        _httpAddress ??= DefaultHttpAddress();
        return _httpAddress;
    }

    #endregion

    private static Address DefaultFtpAddress()
    {
        Address internalFtp = new()
        {
            Host = @"192.168.0.210",
            Port = 21,
            Username = "hwclient",
            Password = "hw_ftpa206"
        };
        return internalFtp;
    }

    private static Address DefaultHttpAddress()
    {
        Address internalHttp = new()
        {
            Host = @"192.168.0.210",
            Port = 8001
        };
        return internalHttp;
    }
}