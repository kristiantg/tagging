namespace TaggingToolApi.Endpoints;

public static class HealthCheckEndpoints
{
    public static void MapHealthCheckEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/").RequireAuthorization();

        group.MapGet("", HealthCheck)
            .WithName(nameof(HealthCheck))
            .WithOpenApi();
    }

    private static async Task<IResult> HealthCheck()
    {
        return Results.Ok();
    }
}