using DevsApp.Models;

namespace DevsApp.Contracts.Services;

/// <summary>
/// 缓存的数据中心
/// </summary>
public interface IDataService
{
    Task<IEnumerable<DevSoftDown>> GetDevSoftDownAsync();
    Task SaveDevSoftDownAsync();
    Task AddDevSoftDownAsync(DevSoftDown devSoftDown);
    Task RemoveDevSoftDownAsync(DevSoftDown devSoftDown);
}