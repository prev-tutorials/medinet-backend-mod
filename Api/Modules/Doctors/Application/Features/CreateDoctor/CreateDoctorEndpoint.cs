namespace Api.Modules.Doctors.Application.Features.CreateDoctor;

using MediatR;

public static class CreateDoctorEndpoint
{
    
    public static RouteHandlerBuilder MapCreateDoctor(
        this IEndpointRouteBuilder endpoints
    ) => endpoints.MapPost("/", CreateDoctor);

    private static async Task<IResult> CreateDoctor(
        ISender sender,
        CreateDoctorRequest request
    )
    {
        var command = new CreateDoctorCommand(
            Guid.NewGuid(),
            request.Speciality,
            request.NRegister,
            request.Dni,
            request.FirsName,
            request.LastName,
            request.BirthDate,
            request.Phone,
            request.Email,
            request.Address
        );

        var doctorId = await sender.Send(command);
        return Results.Ok(new{id = doctorId});
    }
}
