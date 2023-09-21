using DevsApp.Contracts.Services;
using DevsApp.Core.Contracts.Services;
using DevsApp.Core.Helpers;
using DevsApp.Core.Models;
using DevsApp.Models;
using Microsoft.Extensions.Options;

namespace DevsApp.Services;

public class DataService : IDataService
{
    private List<DevSoftDown>? _allDevSoftDown;

    private readonly string _defaultDevSoftDownFile = $"{nameof(DevSoftDown)}.json";

    private readonly IFileService _fileService;

    private readonly string _localApplicationData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
    private readonly string _applicationDataFolder;
    private readonly string _devSoftDownFile;

    public DataService(IFileService fileService, IOptions<LocalSettingsOptions> options)
    {
        _fileService = fileService;
        var options1 = options.Value;

        _applicationDataFolder = Path.Combine(_localApplicationData, options1.ApplicationDataFolder ?? LocalSettingsService.DefaultApplicationDataFolder);
        _devSoftDownFile = options1.LocalSettingsFile ?? _defaultDevSoftDownFile;
    }

    #region IDataService Members

    public async Task<IEnumerable<DevSoftDown>> GetDevSoftDownAsync()
    {
        if (_allDevSoftDown != null)
        {
            return _allDevSoftDown;
        }

        var devSoftDowns = _fileService.Read<IEnumerable<DevSoftDown>>(_applicationDataFolder, _defaultDevSoftDownFile);
        _allDevSoftDown = devSoftDowns?.ToList() ?? new List<DevSoftDown>(AllDevSoftDown());

        await Task.CompletedTask;
        return _allDevSoftDown;
    }


    public async Task SaveDevSoftDownAsync()
    {
        await Task.Run(() => _fileService.Save(_applicationDataFolder, _defaultDevSoftDownFile, _allDevSoftDown));
    }

    public Task AddDevSoftDownAsync(DevSoftDown devSoftDown)
    {
        _allDevSoftDown?.Add(devSoftDown);
        return Task.CompletedTask;
    }

    public Task RemoveDevSoftDownAsync(DevSoftDown devSoftDown)
    {
        _allDevSoftDown?.Remove(devSoftDown);
        return Task.CompletedTask;
    }

    #endregion

    private static IEnumerable<DevSoftDown> AllDevSoftDown()
    {
        // The following is order summary data
        var companies = AllCompanies();
        return companies;
    }

    private static IEnumerable<DevSoftDown> AllCompanies()
    {
        var hwServer = App.GetService<IHWService>();
        var paths = App.GetService<IPathService>();

        return new List<DevSoftDown>
        {
            new()
            {
                DevSoft = new DevSoft()
                {
                    Id = "1",
                    Name = "轻量化Revit插件",
                    ScrumTeam = "图形很行项目组",
                    Version = "v1.0.0",
                },
                FtpAddress = hwServer.GetFtpAddress(),
                RemoteFolder = @$"/BuildMaster(Dazzle)/Dazzle.RevitApp/",
                DownloadFolder = paths.DownloadsPath,
            }
        };
    }
}