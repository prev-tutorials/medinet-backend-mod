using MediatR;

namespace Api.Modules.Patients.Application.Features.GetPatients;

public static class GetPatientsEndpoint
{
    public static void MapGetPatients(this IEndpointRouteBuilder endpoints)
        => endpoints.MapGet("/", GetPatients);

    private static async Task<IResult> GetPatients(
        Guid? Id,
        ISender sender,
        CancellationToken ct
    )
    {
        var query = new GetPatientsQuery(Id);
        var result = await sender.Send(query, ct);
        return Results.Ok(result);
    }
}
