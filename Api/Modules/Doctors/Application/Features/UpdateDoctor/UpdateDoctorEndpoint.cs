using MediatR;

namespace Api.Modules.Doctors.Application.Features.UpdateDoctor;


public static class UpdateDoctorEndpoint
{
    public static void MapUpdateDoctor(
        this IEndpointRouteBuilder endpoints
    ) => endpoints.MapPatch("/{id:guid}", UpdateDoctor);


    private static async Task<IResult> UpdateDoctor(
        Guid id,
        UpdateDoctorRequest request,
        ISender sender,
        CancellationToken ct
    )
    {
        var command = new UpdateDoctorCommand(
            id,
            request
        );

        var result = await sender.Send(command, ct);
        return Results.Ok(new {id = result});
    }
}