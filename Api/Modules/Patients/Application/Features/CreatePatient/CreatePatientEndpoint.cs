using MediatR;

namespace Api.Modules.Patients.Application.Features.CreatePatient;


public static class CreatePatientEndpoint
{
    public static void MapCreatePatient(
        this  IEndpointRouteBuilder endpoints
    ) => endpoints.MapPost("/", CreatePatient);


    private static async Task<IResult> CreatePatient(
        CreatePatientRequest request,
        IMediator mediator, 
        CancellationToken ct
    )
    {
        var command = new CreatePatientCommand(request);
        var result = await mediator.Send(command, ct);
        return Results.Ok(new{id = result});
    }
}
