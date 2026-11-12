using Api.Modules.Shared.Application.Dtos.Persons;

namespace Api.Modules.Appointments.Application.Dtos;

public record PatientResponse(
    Guid Id,
    string Nhc,
    PersonResponse Person
);