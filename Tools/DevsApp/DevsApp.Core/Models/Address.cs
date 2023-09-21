using DevsApp.Core.Contracts.Models;

namespace DevsApp.Core.Models;

public class Address : IAddress, ICloneable
{
    public string Host
    {
        get;
        set;
    } = "127.0.0.0";

    public int Port
    {
        get;
        set;
    } = 80;

    public string Username
    {
        get;
        set;
    } = string.Empty;

    public string Password
    {
        get;
        set;
    } = string.Empty;

    #region ICloneable Members
    public object Clone() => MemberwiseClone();
    #endregion
}