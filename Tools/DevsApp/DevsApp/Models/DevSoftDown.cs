using System.IO.Compression;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DevsApp.Contracts.Services;
using DevsApp.Core.Contracts.Models;
using DevsApp.Core.Contracts.Services;
using DevsApp.Core.Models;
using FluentFTP;
using Microsoft.UI.Dispatching;
using Microsoft.UI.Xaml.Controls;
using Microsoft.Windows.AppNotifications.Builder;
using Newtonsoft.Json;

#pragma warning disable MVVMTK0034

namespace DevsApp.Models;

/// <summary>
/// 产品下载记录
/// </summary>
[JsonObject(MemberSerialization.OptIn)]
public partial class DevSoftDown : ObservableObject
{
    [ObservableProperty]
    [property: JsonProperty]
    private IDevSoft _devSoft = new DevSoft();

    [ObservableProperty]
    [property: JsonProperty]
    private string _downloadFolder = string.Empty;

    [ObservableProperty]
    private DownloadProgress _downloadProgress = new();

    [ObservableProperty]
    [property: JsonProperty]
    private IAddress _ftpAddress = new Address();

    private FtpProgress? _ftpProgress;

    [ObservableProperty]
    private bool _isDownloading;

    [ObservableProperty]
    [property: JsonProperty]
    private bool _isUnzip;

    [ObservableProperty]
    [property: JsonProperty]
    private string _remoteFolder = string.Empty;

    [ObservableProperty]
    [property: JsonProperty]
    private string _unzipFolder = string.Empty;

    private static void DeleteDirectory(string targetDirectory)
    {
        var files = Directory.GetFiles(targetDirectory);
        var dirs = Directory.GetDirectories(targetDirectory);

        foreach (var file in files)
        {
            File.SetAttributes(file, FileAttributes.Normal);
            File.Delete(file);
        }

        foreach (var dir in dirs)
        {
            DeleteDirectory(dir);
        }

        Directory.Delete(targetDirectory, false);
    }


    private void ShowMessage(string message)
    {
        async void Callback()
        {
            var dialog = new ContentDialog
            {
                XamlRoot = App.MainWindow.Content.XamlRoot,
                Title = "提示",
                Content = $"{message}",
                PrimaryButtonText = "OK",
                DefaultButton = ContentDialogButton.Primary
            };
            await dialog.ShowAsync();
        }

        App.MainWindow.DispatcherQueue.TryEnqueue(DispatcherQueuePriority.Normal, Callback);
    }

    private IProgress<FtpProgress> GetFtpProgressReport() => new Progress<FtpProgress>(progress => { DownloadProgress.Progress = progress.Progress; });

    [RelayCommand]
    private async void Down()
    {
        if (!Directory.Exists(_downloadFolder)
         || (!string.IsNullOrEmpty(_unzipFolder) && !Directory.Exists(_unzipFolder)))
        {
            ShowMessage("路径不存在");
            return;
        }

        if (IsDownloading)
        {
            return;
        }

        // 重置进度条状态
        IsDownloading = true;
        DownloadProgress.Reset();

        await Task.Run(async () =>
        {
            var ftpService = App.GetService<IFluentFTPService>();
            var ftpClient = ftpService.CreateAsyncFtpClient(FtpAddress);

            var ftpRemoteDirPath = Path.Combine(_remoteFolder, _devSoft.Version);
            await ftpClient.Connect();

            try
            {
                var ftpListItems = ftpClient.GetListing(ftpRemoteDirPath).WaitAsync(CancellationToken.None).Result.ToList();
                ftpListItems.Sort((a, b) => -a.Modified.CompareTo(b.Modified));
                var ftpListItem = ftpListItems[0];

                var downloadFullName = ftpListItem.FullName;
                var downloadFolder = Path.Combine(_downloadFolder, ftpListItem.Name);

                // 开始下载
                await using var timer = CreateUpdateProgressTimer();
                await ftpClient.DownloadFile(downloadFolder, downloadFullName, FtpLocalExists.Resume, FtpVerify.Retry, new Progress<FtpProgress>(progress => { _ftpProgress = progress; }));
                NotifyProgressChanged(_ftpProgress);

                // 解压
                if (!IsUnzip)
                {
                    DownloadCompleteMessage(downloadFullName, DevSoft.Id, downloadFolder);
                    return;
                }
                if (!Directory.Exists(_unzipFolder))
                {
                    return;
                }

                DeleteDirectory(_unzipFolder);
                ZipFile.ExtractToDirectory(downloadFolder, _unzipFolder);
                UnzipCompleteMessage(ftpListItem.Name, DevSoft.Id, _unzipFolder);
            }
            catch (Exception e)
            {
                ShowMessage(e.ToString());
            }
            finally
            {
                ftpClient?.Disconnect();
                App.GetService<IDataService>()?.SaveDevSoftDownAsync();
                _ftpProgress = null;
            }
        });

        IsDownloading = false;
    }

    /// <summary>
    /// 创建一个定时器，每秒更新一次进度
    /// </summary>
    /// <returns></returns>
    private Timer CreateUpdateProgressTimer()
    {
        var timer = new Timer(state =>
        {
            NotifyProgressChanged(_ftpProgress);
        }, null, TimeSpan.Zero, TimeSpan.FromMilliseconds(1000));
        return timer;
    }

    private void NotifyProgressChanged(FtpProgress? progress)
    {
        if (progress == null)
        { return; }

        App.MainWindow.DispatcherQueue.TryEnqueue(DispatcherQueuePriority.Low, () => { DownloadProgress.Progress = progress.Progress; });
    }


    private void DownloadCompleteMessage(in string downloadFileName, in string tagId, in string filePath)
    {
        var appNotificationService = App.GetService<IAppNotificationService>();

        var builder = new AppNotificationBuilder()
            .SetScenario(AppNotificationScenario.Default)
            .AddText("下载完成")
            .AddText($"{downloadFileName}")
            .AddButton(new AppNotificationButton("open")
                .AddArgument("action", nameof(IFileService.OpenFolderAndSelectItem))
                .AddArgument("filePath", $"{filePath}")
                .AddArgument("tagId", $"{tagId}"))
            .SetGroup("DownloadComplete")
            .SetTag($"{tagId}");

        appNotificationService.Show(builder.BuildNotification());
    }

    private void UnzipCompleteMessage(in string downloadFileName, in string tagId, in string folder)
    {
        var appNotificationService = App.GetService<IAppNotificationService>();

        var builder = new AppNotificationBuilder()
            .SetScenario(AppNotificationScenario.Default)
            .AddText("解压完成")
            .AddText($"{downloadFileName}")
            .AddButton(new AppNotificationButton("open")
                .AddArgument("action", nameof(IFileService.OpenFolder))
                .AddArgument("filePath", $"{folder}")
                .AddArgument("tagId", $"{tagId}"))
            .SetGroup("DownloadComplete")
            .SetTag($"{tagId}");

        appNotificationService.Show(builder.BuildNotification());
    }
}