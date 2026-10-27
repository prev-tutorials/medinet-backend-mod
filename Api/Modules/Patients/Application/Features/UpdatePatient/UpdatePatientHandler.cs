using Api.Modules.Patients.Application.Contracts;
using MediatR;

namespace Api.Modules.Patients.Application.Features.UpdatePatient;


public class UpdatePatientHandler(
    IPatientRepository repository
): IRequestHandler<UpdatePatientCommand, Guid>
{

    private readonly IPatientRepository _repository = repository;

    public async Task<Guid> Handle(
        UpdatePatientCommand command, 
        CancellationToken ct
    )
    {
        var patient = command.Patient;
        var person = patient.Person;

        var foundPatient = await _repository.GetByIdAsync(command.Id, ct) 
            ?? throw new KeyNotFoundException($"No se encontro un paciente con el id {command.Id}");

        foundPatient.Person.Update(
            person.Dni,
            person.FirstName,
            person.LastName,
            person.BirthDate,
            person.Email,
            person.Phone,
            person.Address
        );

        foundPatient.Update(
            patient.Nhc
        );

        var result = await _repository.UpdateAsync(foundPatient, ct);
        return result.Id;
    }
}