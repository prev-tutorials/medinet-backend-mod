using MediatR;

namespace Api.Modules.Patients.Application.Features.DeletePatient;

public record DeletePatientCommand(
    Guid Id
) : IRequest<Guid>;

