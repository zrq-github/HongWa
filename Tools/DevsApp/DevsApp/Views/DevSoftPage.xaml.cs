using DevsApp.ViewModels;

using Microsoft.UI.Xaml.Controls;

namespace DevsApp.Views;

// TODO: Change the grid as appropriate for your app. Adjust the column definitions on DataGridPage.xaml.
// For more details, see the documentation at https://docs.microsoft.com/windows/communitytoolkit/controls/datagrid.
public partial class DevSoftPage : Page
{
    public DevSoftViewModel ViewModel
    {
        get;
    }

    public DevSoftPage()
    {
        ViewModel = App.GetService<DevSoftViewModel>();
        InitializeComponent();
    }
}
