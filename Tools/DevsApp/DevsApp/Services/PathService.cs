using DevsApp.Contracts.Services;

namespace DevsApp.Services;

internal class PathService : IPathService
{
    #region IPathService Members

    public string DownloadsPath { get; set; } = GetDownloadsPath();

    #endregion

    private static string GetDownloadsPath()
    {
        var downloadsPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        downloadsPath = Path.Combine(downloadsPath, "Downloads");
        return downloadsPath;
    }
}