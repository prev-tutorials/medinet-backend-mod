using Api.Modules.Doctors.Domain;
using Api.Modules.Shared.Application.Dtos;

namespace Api.Modules.Doctors.Application.Dtos;


public record DoctorResponse(
    Guid Id,
    string Speciality,
    string NRegister,
    bool Active,
    PersonResponse Person
);



public static class DoctorResponseMapper
{
    public static DoctorResponse ToResponse(this Doctor doctor)
        => new(
            doctor.Id,
            doctor.Speciality.Value,
            doctor.NRegister,
            doctor.Active,
            doctor.Person.ToResponse()
        );
}