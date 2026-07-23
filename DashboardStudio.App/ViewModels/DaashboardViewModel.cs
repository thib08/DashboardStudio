using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using DashboardStudio.Models.Dashboards;

namespace DashboardStudio.App.ViewModels;

public partial class DashboardViewModel : ObservableObject
{
    private readonly Dashboard dashboard;

    public Guid Id => dashboard.Id;

    public string Name => dashboard.Name;

    public ObservableCollection<PageViewModel> Pages { get; } = [];

    [ObservableProperty]
    private PageViewModel? selectedPage;

    public DashboardViewModel(
        Dashboard dashboard)
    {
        this.dashboard = dashboard;

        foreach (var page in dashboard.Pages
                     .OrderBy(page => page.Order))
        {
            Pages.Add(
                new PageViewModel(page));
        }

        SelectedPage = Pages.FirstOrDefault();
    }
}