using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using DashboardStudio.Models.Dashboards;

namespace DashboardStudio.App.ViewModels;

public partial class DashboardItemViewModel : ObservableObject
{
    private readonly Dashboard dashboard;

    public Guid Id => dashboard.Id;

    public string Name => dashboard.Name;

    [ObservableProperty]
    private bool isSelected;

    public DashboardViewModel ViewModel { get; }

    public DashboardItemViewModel(
        Dashboard dashboard)
    {
        this.dashboard = dashboard;

        ViewModel = new DashboardViewModel(
            dashboard);
    }
}