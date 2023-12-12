using Amazon;
using Amazon.DynamoDBv2;
using Application.Settings;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DepedencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<IAmazonDynamoDB>(_ => new AmazonDynamoDBClient(RegionEndpoint.EUWest1));
        
        services.AddSingleton<ICampaignRepository, CampaignRepository>();
        services.AddSingleton<ICampaignService, CampaignService>();
        return services;
    }
}