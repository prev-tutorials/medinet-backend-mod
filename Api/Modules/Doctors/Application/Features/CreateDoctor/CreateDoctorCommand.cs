using MediatR;

namespace Api.Modules.Doctors.Application.Features.CreateDoctor;


public record CreateDoctorRequest(
    string Speciality,
    string NRegister,
    string Dni,
    string FirsName,
    string LastName,
    string? BirthDate,
    string? Phone,
    string? Email,
    string? Address
);

public record CreateDoctorCommand(
    Guid Id,
    string Speciality,
    string NRegister,
    string Dni,
    string FirsName,
    string LastName,
    string? BirthDate,
    string? Phone,
    string? Email,
    string? Address
):IRequest<Guid>;