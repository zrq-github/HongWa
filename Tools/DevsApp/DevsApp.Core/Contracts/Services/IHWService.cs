using DevsApp.Core.Models;

namespace DevsApp.Core.Contracts.Services;

public interface IHWService
{
    public Address GetFtpAddress();

    public Address GetHttpAddress();
}