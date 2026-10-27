using MediatR;

namespace  Api.Modules.Doctors.Application.Features.DeleteDoctor;

public static class DeleteDoctorEndpoint
{
    public static void MapDeleteDoctor(this IEndpointRouteBuilder endpoints)   
        => endpoints.MapDelete("/{id:guid}",DeleteDoctor);


    private static async Task<IResult> DeleteDoctor(
        Guid id,
        ISender sender,
        CancellationToken ct
    )
    {
        var command = new DeleteDoctorCommand(id);
        var result = await sender.Send(command, ct);
        return Results.Ok(new{id=result});
    }
}