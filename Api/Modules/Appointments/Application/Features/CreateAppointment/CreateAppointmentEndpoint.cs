using MediatR;

namespace Api.Modules.Appointments.Application.Features.CreateAppointment;

public static class CreateAppointmentEndpoint
{
    public static void MapCreateAppointment(this IEndpointRouteBuilder endpoints)
        => endpoints.MapPost("/", CreateAppointment);

    private static async Task<IResult> CreateAppointment(
        CreateAppointmentRequest request,
        ISender sender,
        CancellationToken ct
    )
    {
        var command = new CreateAppointmentCommand(request);
        var result = await sender.Send(command, ct);
        return Results.Ok(new { id = result });
    }
}
