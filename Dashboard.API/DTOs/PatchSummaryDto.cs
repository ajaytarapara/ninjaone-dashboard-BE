namespace Dashboard.API.DTOs;

public class PatchSummaryDto
{
    public int TotalPatches { get; set; }

    public int InstalledPatches { get; set; }

    public int PendingPatches { get; set; }

    public int FailedPatches { get; set; }

    public int CriticalPatches { get; set; }
}