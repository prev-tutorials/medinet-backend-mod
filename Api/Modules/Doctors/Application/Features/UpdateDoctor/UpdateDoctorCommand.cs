using MediatR;

namespace Api.Modules.Doctors.Application.Features.UpdateDoctor;

public record UpdateDoctorRequest(
    string? Speciality,
    string? NRegister,
    bool?   Active,
    string? Dni,
    string? FirstName,
    string? LastName,
    string? BirthDate,
    string? Phone,
    string? Email,
    string? Address
);

public record UpdateDoctorCommand(
    Guid Id,
    string? Speciality,
    string? NRegister,
    bool?   Active,
    string? Dni,
    string? FirstName,
    string? LastName,
    string? BirthDate,
    string? Phone,
    string? Email,
    string? Address
):IRequest<Guid>;