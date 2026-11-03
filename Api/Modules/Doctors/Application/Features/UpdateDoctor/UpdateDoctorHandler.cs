using Api.Modules.Doctors.Application.Contracts;
using Api.Modules.Doctors.Domain.ValueObjects;
using MediatR;

namespace Api.Modules.Doctors.Application.Features.UpdateDoctor;


public class UpdateDoctorHandler(
    IDoctorRepository repository
) : IRequestHandler<UpdateDoctorCommand, Guid>
{
    private readonly IDoctorRepository _repository = repository;
    public async Task<Guid> Handle(
        UpdateDoctorCommand command, 
        CancellationToken ct
    )
    {
        var doctor = command.Doctor;
        var person = command.Doctor.Person;

        var foundDoctor = await _repository.GetByIdAsync(command.Id, ct)
            ?? throw new ArgumentException("Doctor with id not found");

        var specialityParsed = !string.IsNullOrWhiteSpace(doctor.Speciality)
            ? Speciality.From(doctor.Speciality)
            : null;

        foundDoctor.Person.Update(
            person.Dni,
            person.FirstName,
            person.LastName,
            person.BirthDate,
            person.Email,
            person.Phone,
            person.Address
        );

        foundDoctor.Update(
            specialityParsed,
            doctor.NRegister,
            doctor.Active
        );

        var doctorUpdated = await _repository.UpdateAsync(foundDoctor, ct);
        return doctorUpdated.Id;
    }
}