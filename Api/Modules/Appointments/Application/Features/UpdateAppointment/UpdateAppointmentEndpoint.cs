using MediatR;

namespace Api.Modules.Appointments.Application.Features.UpdateAppointment;

public static class UpdateAppointmentEndpoint
{
    public static void MapUpdateAppointment(this IEndpointRouteBuilder endpoints)
        => endpoints.MapPatch("/{id:guid}", UpdateAppointment);

    private static async Task<IResult> UpdateAppointment(
        Guid id,
        UpdateAppointmentRequest request,
        ISender sender,
        CancellationToken ct
    )
    {
        var command = new UpdateAppointmentCommand(id, request);
        var result = await sender.Send(command, ct);
        return Results.Ok(new { id = result });
    }
}
