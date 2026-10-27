using MediatR;

namespace Api.Modules.Patients.Application.Features.DeletePatient;

public static class DeletePatientEndpoint
{
    public static void MapDeletePatient(this IEndpointRouteBuilder endpoints)
        => endpoints.MapDelete("/{id:guid}", DeletePatient);

    private static async Task<IResult> DeletePatient(
        Guid id,
        ISender sender,
        CancellationToken ct
    )
    {
        var command = new DeletePatientCommand(id);
        var result = await sender.Send(command, ct);
        return Results.Ok(new { id = result });
    }
}
