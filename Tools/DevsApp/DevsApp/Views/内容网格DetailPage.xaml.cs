using CommunityToolkit.WinUI.UI.Animations;

using DevsApp.Contracts.Services;
using DevsApp.ViewModels;

using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace DevsApp.Views;

public sealed partial class 内容网格DetailPage : Page
{
    public 内容网格DetailViewModel ViewModel
    {
        get;
    }

    public 内容网格DetailPage()
    {
        ViewModel = App.GetService<内容网格DetailViewModel>();
        InitializeComponent();
    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
        base.OnNavigatedTo(e);
        this.RegisterElementForConnectedAnimation("animationKeyContentGrid", itemHero);
    }

    protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
    {
        base.OnNavigatingFrom(e);
        if (e.NavigationMode == NavigationMode.Back)
        {
            var navigationService = App.GetService<INavigationService>();

            if (ViewModel.Item != null)
            {
                navigationService.SetListDataItemForNextConnectedAnimation(ViewModel.Item);
            }
        }
    }
}
