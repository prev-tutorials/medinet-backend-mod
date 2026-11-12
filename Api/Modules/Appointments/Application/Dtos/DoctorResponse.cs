

using Api.Modules.Shared.Application.Dtos.Persons;

namespace Api.Modules.Appointments.Application.Dtos;

public record DoctorResponse(
    Guid Id,
    string Speciality,
    string NRegister,
    bool Active,
    PersonResponse Person
);

