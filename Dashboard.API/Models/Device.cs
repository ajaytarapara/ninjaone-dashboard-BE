namespace Dashboard.API.Models;

public class Device
{
    public int Id { get; set; }

    public string? SystemName { get; set; }

    public bool Offline { get; set; }

    public int OrganizationId { get; set; }

    public int LocationId { get; set; }

    public string? AssignedOwnerUid { get; set; }
    public string? Uid { get; set; }
    public string? NodeClass { get; set; }

    public long LastContact { get; set; }

    public List<string> Tags { get; set; } = [];
}