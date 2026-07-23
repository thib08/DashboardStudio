using DashboardStudio.Models.Widgets;

namespace DashboardStudio.Models.Dashboards;

public sealed class Page
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Name { get; set; } = "Nouvelle page";

    public int Order { get; set; }

    public List<Widget> Widgets { get; set; } = [];
}