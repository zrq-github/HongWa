using System.Collections.ObjectModel;

using CommunityToolkit.Mvvm.ComponentModel;

using DevsApp.Contracts.ViewModels;
using DevsApp.Core.Contracts.Services;
using DevsApp.Core.Models;

namespace DevsApp.ViewModels;

public partial class 内容网格1ViewModel : ObservableRecipient, INavigationAware
{
    private readonly ISampleDataService _sampleDataService;

    public ObservableCollection<SampleOrder> Source { get; } = new ObservableCollection<SampleOrder>();

    public 内容网格1ViewModel(ISampleDataService sampleDataService)
    {
        _sampleDataService = sampleDataService;
    }

    public async void OnNavigatedTo(object parameter)
    {
        Source.Clear();

        // TODO: Replace with real data.
        var data = await _sampleDataService.GetGridDataAsync();

        foreach (var item in data)
        {
            Source.Add(item);
        }
    }

    public void OnNavigatedFrom()
    {
    }
}
