namespace DevsApp.Core.Contracts.Models;

public interface IAddress
{
    public string Host { get; set; }

    public int Port { get; set; }
    
    public string Username { get; set; }

    public string Password { get; set; }
}