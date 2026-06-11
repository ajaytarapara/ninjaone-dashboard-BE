namespace Dashboard.API.Models;

public class Alert
{
    public string? Uid { get; set; }

    public int DeviceId { get; set; }

    public string? Message { get; set; }

    public long CreateTime { get; set; }

    public long UpdateTime { get; set; }

    public string? Subject { get; set; }

    public string? Severity { get; set; }
}