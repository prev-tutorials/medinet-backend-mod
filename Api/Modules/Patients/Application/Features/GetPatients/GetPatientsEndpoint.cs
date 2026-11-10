using MediatR;

namespace Api.Modules.Patients.Application.Features.GetPatients;

public static class GetPatientsEndpoint
{
    public static void MapGetPatients(this IEndpointRouteBuilder endpoints)
        => endpoints.MapGet("/", GetPatients);

    private static async Task<IResult> GetPatients(
        [AsParameters] GetPatientsRequest request,
        ISender sender,
        CancellationToken ct
    )
    {
        var query = new GetPatientsQuery(request);
        var result = await sender.Send(query, ct);
        return Results.Ok(result);
    }
}
