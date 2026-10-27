using Api.Modules.Patients.Application.Contracts;
using MediatR;

namespace Api.Modules.Patients.Application.Features.DeletePatient;

public class DeletePatientHandler(IPatientRepository repository)
    : IRequestHandler<DeletePatientCommand, Guid>
{
    private readonly IPatientRepository _repository = repository;

    public async Task<Guid> Handle(
        DeletePatientCommand command,
        CancellationToken ct
    )
    {
        var patient = await _repository.GetByIdAsync(command.Id, ct)
            ?? throw new KeyNotFoundException($"Patient with id {command.Id} not found");

        var deletedPatient = await _repository.DeleteAsync(patient, ct);
        return deletedPatient.Id;
    }
}
