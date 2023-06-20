using CommunityToolkit.Mvvm.ComponentModel;

using DevsApp.Contracts.ViewModels;
using DevsApp.Core.Contracts.Services;
using DevsApp.Core.Models;

namespace DevsApp.ViewModels;

public partial class 内容网格DetailViewModel : ObservableRecipient, INavigationAware
{
    private readonly ISampleDataService _sampleDataService;

    [ObservableProperty]
    private SampleOrder? item;

    public 内容网格DetailViewModel(ISampleDataService sampleDataService)
    {
        _sampleDataService = sampleDataService;
    }

    public async void OnNavigatedTo(object parameter)
    {
        if (parameter is long orderID)
        {
            var data = await _sampleDataService.GetContentGridDataAsync();
            Item = data.First(i => i.OrderID == orderID);
        }
    }

    public void OnNavigatedFrom()
    {
    }
}
