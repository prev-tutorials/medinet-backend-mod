using MediatR;

namespace Api.Modules.Appointments.Application.Features.GetAppointments;

public static class GetAppointmentsEndpoint
{
    public static void MapGetAppointments(this IEndpointRouteBuilder endpoints)
        => endpoints.MapGet("/", GetAppointments);

    private static async Task<IResult> GetAppointments(
        [AsParameters] Guid Id,
        ISender sender,
        CancellationToken ct
    )
    {
        var query = new GetAppointmentsQuery(Id);
        var result = await sender.Send(query, ct);
        return Results.Ok(result);
    }
}
