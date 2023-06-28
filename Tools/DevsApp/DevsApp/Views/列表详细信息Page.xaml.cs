﻿using CommunityToolkit.WinUI.UI.Controls;

using DevsApp.ViewModels;

using Microsoft.UI.Xaml.Controls;

namespace DevsApp.Views;

public sealed partial class 列表详细信息Page : Page
{
    public 列表详细信息ViewModel ViewModel
    {
        get;
    }

    public 列表详细信息Page()
    {
        ViewModel = App.GetService<列表详细信息ViewModel>();
        InitializeComponent();
    }

    private void OnViewStateChanged(object sender, ListDetailsViewState e)
    {
        if (e == ListDetailsViewState.Both)
        {
            ViewModel.EnsureItemSelected();
        }
    }
}
