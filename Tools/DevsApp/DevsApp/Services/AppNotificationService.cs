using System.Collections.Specialized;
using System.Web;
using CommunityToolkit.WinUI.Notifications;
using DevsApp.Contracts.Services;
using DevsApp.Core.Contracts.Services;
using DevsApp.ViewModels;
using Microsoft.Windows.AppNotifications;

namespace DevsApp.Notifications;

public class AppNotificationService : IAppNotificationService
{
    private readonly IFileService _fileService;
    private readonly INavigationService _navigationService;

    public AppNotificationService(INavigationService navigationService, IFileService fileService)
    {
        _navigationService = navigationService;
        _fileService = fileService;
    }

    #region IAppNotificationService Members

    public void Initialize()
    {
        AppNotificationManager.Default.NotificationInvoked += OnNotificationInvoked;
        ToastNotificationManagerCompat.OnActivated += ToastNotificationManagerCompat_OnActivated;

        AppNotificationManager.Default.Register();
    }

    public bool Show(in string payload)
    {
        var appNotification = new AppNotification(payload);
        return Show(appNotification);
    }

    public bool Show(in AppNotification appNotification)
    {
        AppNotificationManager.Default.Show(appNotification);
        return appNotification.Id != 0;
    }


    public NameValueCollection ParseArguments(in string arguments) => HttpUtility.ParseQueryString(arguments);

    public void Unregister() => AppNotificationManager.Default.Unregister();

    #endregion

    public void OnNotificationInvoked(AppNotificationManager sender, AppNotificationActivatedEventArgs args)
    {
        var arguments = args.Arguments;

        // 打开文件的操作
        Task.Run(() =>
        {
            arguments.TryGetValue("action", out var activateValue);
            switch (activateValue)
            {
                case nameof(IFileService.OpenFolderAndSelectItem):
                    OpenFolderAndSelectItem(sender, arguments);
                    break;
                case nameof(IFileService.OpenFolder):
                    OpenFolder(sender, arguments);
                    break;
            }
        });


        // Navigate to a specific page based on the notification arguments.
        if (ParseArguments(args.Argument)["action"] == "Settings")
        {
            App.MainWindow.DispatcherQueue.TryEnqueue(() => { _navigationService.NavigateTo(typeof(SettingsViewModel).FullName!); });
        }

        // todo: 通知
        //App.MainWindow.DispatcherQueue.TryEnqueue(() =>
        //{
        //    App.MainWindow.ShowMessageDialogAsync("TODO: Handle notification invocations when your app is already running.", "Notification Invoked");

        //    App.MainWindow.BringToFront();
        //});
    }

    private void OpenFolder(in AppNotificationManager sender, in IDictionary<string, string> arguments)
    {
        arguments.TryGetValue("filePath", out var filePath);
        _fileService.OpenFolder(filePath);

        if (arguments.TryGetValue("tagId", out var tagId))
        {
            _ = sender.RemoveByTagAsync(tagId);
        }
    }

    private void ToastNotificationManagerCompat_OnActivated(ToastNotificationActivatedEventArgsCompat e) { }

    private void OpenFolderAndSelectItem(in AppNotificationManager sender, in IDictionary<string, string> arguments)
    {
        arguments.TryGetValue("filePath", out var filePath);
        // 在这里打开文件夹
        _fileService.OpenFolderAndSelectItem(filePath);

        if (arguments.TryGetValue("tagId", out var tagId))
        {
            _ = sender.RemoveByTagAsync(tagId);
        }
    }

    ~AppNotificationService()
    {
        Unregister();
    }
}