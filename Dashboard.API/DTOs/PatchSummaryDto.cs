namespace Dashboard.API.DTOs;

public class PatchSummaryDto
{
    public KpiMetricDto TotalPatches { get; set; } = new();

    public KpiMetricDto InstalledPatches { get; set; } = new();

    public KpiMetricDto PendingPatches { get; set; } = new();

    public KpiMetricDto FailedPatches { get; set; } = new();

    public KpiMetricDto CriticalPatches { get; set; } = new();

    public KpiMetricDto ComplianceScore { get; set; } = new();

    public List<PatchStatusDto> PatchStatusDistribution { get; set; } = [];

    public List<PatchSeverityDto> SeverityDistribution { get; set; } = [];
    public List<ComplianceTrendDto> ComplianceTrend { get; set; } = [];

    public List<OrganizationComplianceDto> ComplianceByOrganization { get; set; } = [];
}