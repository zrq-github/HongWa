using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DevsApp.Contracts.Services;
using DevsApp.Contracts.ViewModels;
using DevsApp.Core.Contracts.Services;
using DevsApp.Models;
using Newtonsoft.Json;

namespace DevsApp.ViewModels;

public partial class DevSoftViewModel : ObservableRecipient, INavigationAware
{
    private readonly IDataService _dataService;

    [ObservableProperty]
    private ObservableCollection<DevSoftDown> _devSoftDowns = new();

    [ObservableProperty]
    private DevSoftDown? _select;

    [ObservableProperty]
    private int _selectIndex;

    public DevSoftViewModel(IDataService dataService)
    {
        _dataService = dataService;
    }

    #region INavigationAware Members

    public async void OnNavigatedTo(object parameter)
    {
        var devSoftData = await _dataService.GetDevSoftDownAsync();
        DevSoftDowns = new ObservableCollection<DevSoftDown>(devSoftData);
    }

    public void OnNavigatedFrom() => _dataService?.SaveDevSoftDownAsync();

    #endregion

    [RelayCommand]
    private async Task AddSelect()
    {
        if (DevSoftDowns.Count == 0)
        {
            return;
        }

        var newDevSoftDown = this.DevSoftDowns[this.SelectIndex];

        var str = JsonConvert.SerializeObject(newDevSoftDown);
        var cloneObject = JsonConvert.DeserializeObject<DevSoftDown>(str);
        if (null == cloneObject)
        {
            return;
        }

        await _dataService.AddDevSoftDownAsync(cloneObject);
        DevSoftDowns.Add(cloneObject);
    }

    [RelayCommand]
    private async Task RemoveSelect()
    {
        switch (DevSoftDowns.Count)
        {
            case 0:
            case 1:
                return;
        }

        var select = DevSoftDowns[SelectIndex];

        await _dataService.RemoveDevSoftDownAsync(select);
        DevSoftDowns.Remove(select);
    }
}