namespace DashboardStudio.Contracts.Runtime;

public sealed class RuntimeInfo
{
    public Guid DeviceId { get; set; }

    public string DeviceName { get; set; } = string.Empty;

    public string RuntimeVersion { get; set; } = string.Empty;

    public string OperatingSystem { get; set; } = string.Empty;

    public string Architecture { get; set; } = string.Empty;

    public int ScreenWidth { get; set; }

    public int ScreenHeight { get; set; }

    public bool IsTouchScreen { get; set; }

    public bool IsOnline { get; set; }
}
