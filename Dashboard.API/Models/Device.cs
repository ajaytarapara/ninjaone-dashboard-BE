namespace Dashboard.API.Models;

public class Device
{
    public int Id { get; set; }

    public string? SystemName { get; set; }

    public bool Offline { get; set; }

    public int OrganizationId { get; set; }

    public int LocationId { get; set; }

     public string? AssignedOwnerUid { get; set; }
}