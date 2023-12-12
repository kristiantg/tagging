namespace Application.Contracts.Requests;

public record GetCampaignsQuery(
    string? SearchTerm, 
    string? SortColumn,
    string? SortOrder,
    int? Page,
    int? PageSize);