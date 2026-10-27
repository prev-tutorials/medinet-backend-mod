using MediatR;

namespace Api.Modules.Patients.Application.Features.UpdatePatient;


public static class UpdatePatientEndpoint{

    public static void MapUpdatePatient(this IEndpointRouteBuilder endpoint)
        => endpoint.MapPatch("/{id:guid}", UpdatePatient);


    private static async Task<IResult> UpdatePatient(
        Guid id,
        UpdatePatientRequest request,
        ISender sender,
        CancellationToken ct
    )
    {
        var command = new UpdatePatientCommand(id, request);
        var result = await sender.Send(command, ct);
        return Results.Ok(new{id = result});
    }
}