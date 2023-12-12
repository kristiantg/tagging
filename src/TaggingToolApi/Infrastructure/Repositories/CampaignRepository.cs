using System.Net;
using System.Text.Json;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using Application.Contracts.Data;
using Application.Contracts.Requests;
using Application.Settings;
using Microsoft.Extensions.Options;

namespace Infrastructure.Repositories;

public class CampaignRepository : ICampaignRepository
{
    private readonly IAmazonDynamoDB _dynamoDb;
    private readonly IOptions<DatabaseSettings> _databaseSettings;

    public CampaignRepository(IAmazonDynamoDB dynamoDb,
        IOptions<DatabaseSettings> databaseSettings)
    {
        _dynamoDb = dynamoDb;
        _databaseSettings = databaseSettings;
    }
    public async Task<bool> CreateAsync(CampaignDto campaign)
    {
        var campaignAsJson = JsonSerializer.Serialize(campaign);
        var itemAsDocument = Document.FromJson(campaignAsJson);
        var itemAsAttributes = itemAsDocument.ToAttributeMap();

        var createItemRequest = new PutItemRequest
        {
            TableName = _databaseSettings.Value.TableName,
            Item = itemAsAttributes
        };

        var response = await _dynamoDb.PutItemAsync(createItemRequest);
        return response.HttpStatusCode == HttpStatusCode.OK;
    }

    public async Task<CampaignDto?> GetAsync(Guid id)
    {
        var request = new GetItemRequest
        {
            TableName = _databaseSettings.Value.TableName,
            Key = new Dictionary<string, AttributeValue>
            {
                { "pk", new AttributeValue { S = id.ToString() } },
                { "sk", new AttributeValue { S = id.ToString() } }
            }
        };

        var response = await _dynamoDb.GetItemAsync(request);
        if (response.Item.Count == 0)
        {
            return null;
        }

        var itemAsDocument = Document.FromAttributeMap(response.Item);
        return JsonSerializer.Deserialize<CampaignDto>(itemAsDocument.ToJson());
    }

    public async Task<IEnumerable<CampaignDto>> GetAllAsync(GetCampaignsQuery query)
    {
        var campaigns = new List<CampaignDto>();
        Dictionary<string, AttributeValue> lastKeyEvaluated = null;

        do
        {
            var request = new ScanRequest
            {
                TableName = _databaseSettings.Value.TableName,
                Limit = query.PageSize ?? 200,
                ExclusiveStartKey = lastKeyEvaluated,
            };

            var response = await _dynamoDb.ScanAsync(request);
        
            foreach (var item in response.Items)
            {
                var itemAsDocument = Document.FromAttributeMap(item);
                var campaign = JsonSerializer.Deserialize<CampaignDto>(itemAsDocument.ToJson());
                campaigns.Add(campaign);
            }
        } while (lastKeyEvaluated != null && lastKeyEvaluated.Count != 0);
        
        return campaigns;
    }

    public async Task<bool> UpdateAsync(CampaignDto campaign)
    {
        var campaignAsJson = JsonSerializer.Serialize(campaign);
        var itemAsDocument = Document.FromJson(campaignAsJson);
        var itemAsAttributes = itemAsDocument.ToAttributeMap();

        var updateItemRequest = new PutItemRequest
        {
            TableName = _databaseSettings.Value.TableName,
            Item = itemAsAttributes
        };

        var response = await _dynamoDb.PutItemAsync(updateItemRequest);
        return response.HttpStatusCode == HttpStatusCode.OK;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var deleteItemRequest = new DeleteItemRequest
        {
            TableName = _databaseSettings.Value.TableName,
            Key = new Dictionary<string, AttributeValue>
            {
                { "pk", new AttributeValue { S = id.ToString() } },
                { "sk", new AttributeValue { S = id.ToString() } }
            }
        };

        var response = await _dynamoDb.DeleteItemAsync(deleteItemRequest);
        return response.HttpStatusCode == HttpStatusCode.OK;
    }
}