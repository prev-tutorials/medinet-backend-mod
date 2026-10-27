namespace Api.Modules.Doctors.Application.Features.CreateDoctor;

using MediatR;

public static class CreateDoctorEndpoint
{
    
    public static void MapCreateDoctor(
        this IEndpointRouteBuilder endpoints
    ) => endpoints.MapPost("/", CreateDoctor);


    private static async Task<IResult> CreateDoctor(
        CreateDoctorRequest request,
        ISender sender,
        CancellationToken ct
    )
    {
        var command = new CreateDoctorCommand(request);
        var doctorId = await sender.Send(command, ct);
        return Results.Ok(new{id = doctorId});
    }
}
