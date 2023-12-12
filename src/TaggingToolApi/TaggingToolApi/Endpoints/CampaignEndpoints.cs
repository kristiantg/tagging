using Application.Contracts.Requests;
using Application.Mapping;
using Domain;
using Domain.Common;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace TaggingToolApi.Endpoints;

public static class CampaignEndpoints
{
    public static void MapCampaignEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/campaigns").RequireAuthorization();
        
        group.MapPost("", CreateCampaign)
            .WithName(nameof(CreateCampaign))
            .WithOpenApi();
        
        group.MapGet("{guid}", GetCampaign)
            .WithName(nameof(GetCampaign))
            .WithOpenApi();
        
        group.MapGet("", GetCampaigns)
            .WithName(nameof(GetCampaigns))
            .WithOpenApi();
        
        group.MapPut("{guid}", UpdateCampaign)
            .WithName(nameof(UpdateCampaign))
            .WithOpenApi();
        
        group.MapDelete("{guid}", DeleteCampaign)
            .WithName(nameof(DeleteCampaign))
            .WithOpenApi();
    }
    
    private static async Task<IResult> CreateCampaign(
        [FromBody] CreateCampaignRequest request, ICampaignService campaignService)
    {
        var campaign = Campaign.Create(
            new Name(request.Name),
            new Status(request.Status),
            new TagStatus(request.TagStatus),
            new Tags(request.Tags),
            request.Channels?.Select(channel => new Channel(
                new Title(channel.Title),
                new Tags(channel.Tags),
                new LaunchDate(channel.LaunchDate)
            )),
            new LastModified(DateTime.UtcNow),
            new Created(DateTime.UtcNow)
        );

        await campaignService.CreateAsync(campaign);

        var campaignResponse = campaign.ToCampaignResponse();
        
        return Results.Ok(campaignResponse);
    }
    
    private static async Task<IResult> GetCampaigns(
        ICampaignService campaignService,
        string? searchTerm,
        string? searchColumn,
        string? searchOrder,
        int? page,
        int? pageSize)
    {
        var query = new GetCampaignsQuery(searchTerm, searchColumn, searchOrder, page, pageSize);
        var campaigns = await campaignService.GetAllAsync(query);

        var campaignResponse = campaigns.Select(campaign => campaign.ToCampaignResponse());

        return Results.Ok(campaignResponse);
    }

    private static async Task<IResult> GetCampaign(
        [FromRoute] Guid guid, ICampaignService campaignService)
    {
        var campaign = await campaignService.GetAsync(guid);

        if (campaign is null)
        {
            return Results.NotFound();
        }
        
        var campaignResponse = campaign.ToCampaignResponse();

        return Results.Ok(campaignResponse);
    }
    
    private static async Task<IResult> UpdateCampaign(
     [FromRoute] Guid guid, [FromBody] UpdateCampaignRequest request, ICampaignService campaignService)
    {
        var existingCampaign = await campaignService.GetAsync(guid);
        
        if (existingCampaign is null)
        {
            return Results.NotFound();
        }

        var campaign = Campaign.CreateUpdate(
            request.CampaignId,
            new Name(request.Name),
            new Status(request.Status),
            new TagStatus(request.TagStatus),
            new Tags(request.Tags),
            request.Channels?.Select(channel => new Channel(
                new Title(channel.Title),
                new Tags(channel.Tags),
                new LaunchDate(channel.LaunchDate)
            )),
            new LastModified(DateTime.UtcNow),
            new Created(existingCampaign.Created.Value)
        );
        
        await campaignService.UpdateAsync(campaign);

        var campaignResponse = campaign.ToCampaignResponse();

        return Results.Ok(campaignResponse);
    }
    
    private static async Task<IResult> DeleteCampaign(
        [FromRoute] Guid guid, ICampaignService campaignService)
    {
        var deleted = await campaignService.DeleteAsync(guid);

        if (!deleted)
        {
            return Results.NotFound();
        }

        return Results.Ok();
    }
}