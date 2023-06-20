using DevsApp.ViewModels;

using Microsoft.UI.Xaml.Controls;

namespace DevsApp.Views;

// TODO: Change the grid as appropriate for your app. Adjust the column definitions on DataGridPage.xaml.
// For more details, see the documentation at https://docs.microsoft.com/windows/communitytoolkit/controls/datagrid.
public sealed partial class 内容网格1Page : Page
{
    public 内容网格1ViewModel ViewModel
    {
        get;
    }

    public 内容网格1Page()
    {
        ViewModel = App.GetService<内容网格1ViewModel>();
        InitializeComponent();
    }
}
