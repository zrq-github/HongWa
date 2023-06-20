using DevsApp.ViewModels;

using Microsoft.UI.Xaml.Controls;

namespace DevsApp.Views;

// To learn more about WebView2, see https://docs.microsoft.com/microsoft-edge/webview2/.
public sealed partial class Web视图Page : Page
{
    public Web视图ViewModel ViewModel
    {
        get;
    }

    public Web视图Page()
    {
        ViewModel = App.GetService<Web视图ViewModel>();
        InitializeComponent();

        ViewModel.WebViewService.Initialize(WebView);
    }
}
