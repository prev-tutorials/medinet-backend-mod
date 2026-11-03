using MediatR;

namespace Api.Modules.Appointments.Application.Features.DeleteAppointment;

public static class DeleteAppointmentEndpoint
{
    public static void MapDeleteAppointment(this IEndpointRouteBuilder endpoints)
        => endpoints.MapDelete("/{id:guid}", DeleteAppointment);

    private static async Task<IResult> DeleteAppointment(
        Guid id,
        ISender sender,
        CancellationToken ct
    )
    {
        var command = new DeleteAppointmentCommand(id);
        var result = await sender.Send(command, ct);
        return Results.Ok(new { id = result });
    }
}
