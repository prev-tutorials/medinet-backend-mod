using MediatR;

namespace Api.Modules.Appointments.Application.Features.DeleteAppointments;

public static class DeleteAppointmentsEndpoint
{
    public static void MapDeleteAppointments(this IEndpointRouteBuilder endpoints)
        => endpoints.MapDelete("/{id:guid}", DeleteAppointments);

    private static async Task<IResult> DeleteAppointments(
        Guid id,
        ISender sender,
        CancellationToken ct
    )
    {
        var command = new DeleteAppointmentsCommand(id);
        var result = await sender.Send(command, ct);
        return Results.Ok(new { id = result });
    }
}
