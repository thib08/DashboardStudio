namespace DashboardStudio.Models.Dashboards;

public sealed class Dashboard
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Name { get; set; } = "Nouveau dashboard";

    public string Description { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime ModifiedAt { get; set; } = DateTime.UtcNow;

    public List<Page> Pages { get; set; } = [];
}