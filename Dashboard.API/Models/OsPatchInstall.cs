namespace Dashboard.API.Models;

public class OsPatchInstall
{
    public string Id { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public string Severity { get; set; } = string.Empty;

    public string Status { get; set; } = string.Empty;

    public string Type { get; set; } = string.Empty;

    public long InstalledAt { get; set; }

    public long DeviceId { get; set; }

    public long Timestamp { get; set; }

    public string KbNumber { get; set; } = string.Empty;
}