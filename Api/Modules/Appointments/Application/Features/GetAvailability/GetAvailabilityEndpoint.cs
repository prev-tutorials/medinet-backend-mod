using MediatR;

namespace Api.Modules.Appointments.Application.Features.GetAvailability;

public static class GetAvailabilityEndpoint
{
    public static void MapGetAvailability(this IEndpointRouteBuilder endpoints)
        => endpoints.MapGet("/availability", GetAvailability);

    private static async Task<IResult> GetAvailability(
        [AsParameters] GetAvailabilityRequest request,
        ISender sender,
        CancellationToken ct
    )
    {
        var query = new GetAvailabilityQuery(request);
        var result = await sender.Send(query, ct);
        return Results.Ok(result);
    }
}
