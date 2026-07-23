namespace DashboardStudio.Models.Widgets;

public sealed class Widget
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Type { get; set; } = string.Empty;

    public string Name { get; set; } = "Nouveau widget";

    public int X { get; set; }

    public int Y { get; set; }

    public int Width { get; set; } = 2;

    public int Height { get; set; } = 2;

    public int MinWidth { get; set; } = 1;

    public int MinHeight { get; set; } = 1;

    public int MaxWidth { get; set; } = 12;

    public int MaxHeight { get; set; } = 12;
}