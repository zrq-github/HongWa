using CommunityToolkit.WinUI.Notifications;
using DevsApp.Contracts.Services;
using DevsApp.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.Windows.AppNotifications;
using Microsoft.Windows.AppNotifications.Builder;

namespace DevsApp.Views;

public sealed partial class MainPage : Page
{
    public MainViewModel ViewModel
    {
        get;
    }

    public MainPage()
    {
        ViewModel = App.GetService<MainViewModel>();
        InitializeComponent();
    }

    private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        var toastButton = new ToastButton("snoozeTime", "123");

        var appNotificationService = App.GetService<IAppNotificationService>();

        var builder = new AppNotificationBuilder()
            .SetScenario(AppNotificationScenario.Default)
            .AddText($"下载完成")
            .AddButton(new AppNotificationButton("open the folder")
                .AddArgument("action", "OpenFolder")
                .AddArgument("filePath","path"))
            .AddButton(new AppNotificationButton("Remind me later")
                .AddArgument("action", "remindLater"))
            .SetGroup("下载完成")
            .SetTag("1");
        appNotificationService.Show(builder.BuildNotification());

        // Requires Microsoft.Toolkit.Uwp.Notifications NuGet package version 7.0 or greater
        //new ToastContentBuilder()
        //    .AddArgument("action", "viewConversation")
        //    .AddArgument("conversationId", 9813)
        //    .AddText("Andrew sent you a picture")
        //    .AddText("Check this out, The Enchantments in Washington!")

        //    .Show(); // Not seeing the Show() method? Make sure you have version 7.0, and if you're using .NET 6 (or later), then your TFM must be net6.0-windows10.0.17763.0 or greater

        //int conversationId = 384928;

        // Generate the toast notification content and pop the toast
        //new ToastContentBuilder()
        //    .SetToastScenario(ToastScenario.Reminder)
        //    .AddArgument("action", "viewEvent")
        //    .AddArgument("eventId", 1983)
        //    .AddText("Adaptive Tiles Meeting")
        //    .AddText("Conf Room 2001 / Building 135")
        //    .AddText("10:00 AM - 10:30 AM")
        //    .AddComboBox("snoozeTime", "15", ("1", "1 minute"),
        //        ("15", "15 minutes"),
        //        ("60", "1 hour"),
        //        ("240", "4 hours"),
        //        ("1440", "1 day"))
        //    .AddButton(new ToastButton())
        //    .Show();


        //var builder = new AppNotificationBuilder()
        //    .AddArgument("conversationId", "9813")
        //    .AddText("Conf Room 2001 / Building 135")
        //    .AddText("10:00 AM - 10:30 AM");

        //AppNotificationManager.Default.Show(builder.BuildNotification());


        //App.GetService<IAppNotificationService>().Show(null);

    }

    private void BtnShowDialog_OnClick(object sender, RoutedEventArgs e)
    {
        try
        {
            var dialog = new ContentDialog();

            // XamlRoot must be set in the case of a ContentDialog running in a Desktop app
            dialog.XamlRoot = App.MainWindow.Content.XamlRoot;
            dialog.Style = Application.Current.Resources["DefaultContentDialogStyle"] as Style;
            dialog.Title = "Save your work?";
            dialog.PrimaryButtonText = "Save";
            dialog.SecondaryButtonText = "Don't Save";
            dialog.CloseButtonText = "Cancel";
            dialog.DefaultButton = ContentDialogButton.Primary;
            dialog.Content = "123";

            var result = dialog.ShowAsync();
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            throw;
        }
    }
}
