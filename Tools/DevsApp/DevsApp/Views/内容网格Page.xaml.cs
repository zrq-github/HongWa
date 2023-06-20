using DevsApp.ViewModels;

using Microsoft.UI.Xaml.Controls;

namespace DevsApp.Views;

public sealed partial class 内容网格Page : Page
{
    public 内容网格ViewModel ViewModel
    {
        get;
    }

    public 内容网格Page()
    {
        ViewModel = App.GetService<内容网格ViewModel>();
        InitializeComponent();
    }
}
