using Api.Modules.Patients.Domain;
using Api.Modules.Shared.Application.Dtos.Persons;

namespace Api.Modules.Patients.Application.Dtos;

public record PatientResponse(
    Guid Id,
    string Nhc,
    PersonResponse Person
);

public static class PatientResponseMapper
{
    public static PatientResponse ToResponse(this Patient patient)
        => new(
            patient.Id,
            patient.Nhc,
            patient.Person.ToResponse()
        );
}
