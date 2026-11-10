using MediatR;

namespace Api.Modules.Shared.Application.Features.HealthCheck;

public static class HealthCheckEndpoint
{
    public static void MapHealthCheck(this IEndpointRouteBuilder endpoints)
        => endpoints
            .MapGet("/health", GetHealthCheck)
            .WithTags("Health");


    private static async Task<IResult> GetHealthCheck(
        ISender sender,
        CancellationToken ct
    )
    {
        var query = new HealthCheckQuery();
        var result = await sender.Send(query, ct);
        return Results.Ok(result);
    }
}
