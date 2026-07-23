using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using DashboardStudio.Models.Dashboards;

namespace DashboardStudio.App.ViewModels;

public partial class MainViewModel : ObservableObject
{
    [ObservableProperty]
    private string title = "Dashboard Studio";

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(SelectedDashboardName))]
    private DashboardItemViewModel? selectedDashboard;

    public ObservableCollection<DashboardItemViewModel> Dashboards { get; } = [];

    public string SelectedDashboardName =>
        SelectedDashboard?.Name
        ?? "Aucun dashboard sélectionné";

    public DashboardViewModel? SelectedDashboardViewModel =>
        SelectedDashboard?.ViewModel;

    [ObservableProperty]
    private PageViewModel? selectedPage;

    public MainViewModel()
    {
        CreateDefaultDashboards();

        SelectedDashboard = Dashboards[0];
    }

    private void CreateDefaultDashboards()
    {
        var home = new Dashboard
        {
            Name = "Accueil"
        };

        home.Pages.Add(
            new Page
            {
                Name = "Vue principale",
                Order = 0
            });

        home.Pages.Add(
            new Page
            {
                Name = "Monitoring",
                Order = 1
            });

        home.Pages.Add(
            new Page
            {
                Name = "Contrôles",
                Order = 2
            });

        var cockpit = new Dashboard
        {
            Name = "Cockpit"
        };

        cockpit.Pages.Add(
            new Page
            {
                Name = "Vue principale",
                Order = 0
            });

        var msfs = new Dashboard
        {
            Name = "Microsoft Flight Simulator"
        };

        msfs.Pages.Add(
            new Page
            {
                Name = "Vue principale",
                Order = 0
            });

        Dashboards.Add(
            new DashboardItemViewModel(home));

        Dashboards.Add(
            new DashboardItemViewModel(cockpit));

        Dashboards.Add(
            new DashboardItemViewModel(msfs));
    }

    partial void OnSelectedDashboardChanged(
        DashboardItemViewModel? value)
    {
        foreach (var dashboard in Dashboards)
        {
            dashboard.IsSelected =
                dashboard == value;
        }

        OnPropertyChanged(
            nameof(SelectedDashboardViewModel));

        SelectedPage =
            value?.ViewModel.SelectedPage;
    }
}