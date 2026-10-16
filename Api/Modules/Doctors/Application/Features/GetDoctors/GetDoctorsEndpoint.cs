using MediatR;

namespace Api.Modules.Doctors.Application.Features.GetDoctors;

public static class GetDoctorsEndpoint
{
    public static void MapGetDoctors(this IEndpointRouteBuilder endpoints)
        => endpoints.MapGet("/", GetDoctors);

    
    private static async Task<IResult> GetDoctors(
        Guid? Id,
        ISender sender,
        CancellationToken ct
    )
    {
        var query = new GetDoctorsQuery(Id);
        var result = await sender.Send(query, ct);
        return Results.Ok(result);
    }
}