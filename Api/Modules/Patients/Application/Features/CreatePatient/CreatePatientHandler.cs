using Api.Modules.Patients.Application.Contracts;
using Api.Modules.Patients.Domain;
using Api.Modules.Shared.Domain;
using MediatR;

namespace Api.Modules.Patients.Application.Features.CreatePatient;


public class CreatePatientHandler(
    IPatientRepository repository
) : IRequestHandler<CreatePatientCommand, Guid>
{
    private readonly IPatientRepository _repository = repository;

    public async Task<Guid> Handle(
        CreatePatientCommand command, 
        CancellationToken ct
    )
    {
        var patient = command.Patient;
        var person  = command.Patient.Person;

        var newPerson = Person.Create(
            person.Dni,
            person.FirstName,
            person.LastName,
            person.BirthDate,
            person.Email,
            person.Phone,
            person.Address 
        );

        var newPatient = Patient.Create(
            patient.Nhc,
            newPerson
        );

        var result = await _repository.AddAsync(newPatient, ct);
        return result.Id;
    }
}