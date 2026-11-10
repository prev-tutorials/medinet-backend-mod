using MediatR;

namespace Api.Modules.Doctors.Application.Features.GetDoctors;

public static class GetDoctorsEndpoint
{
    public static void MapGetDoctors(this IEndpointRouteBuilder endpoints)
        => endpoints.MapGet("/", GetDoctors);

    private static async Task<IResult> GetDoctors(
        [AsParameters] GetDoctorsRequest request,
        ISender sender,
        CancellationToken ct
    )
    {
        var query = new GetDoctorsQuery(request);
        var result = await sender.Send(query, ct);
        return Results.Ok(result);
    }
}