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
        var doctor = await _repository.GetById(command.Id, ct)
            ?? throw new ArgumentException("Doctor with id not found");

        var specialityParsed = !string.IsNullOrWhiteSpace(command.Speciality)
            ? Speciality.From(command.Speciality)
            : null;

        doctor.Update(
            specialityParsed,
            command.NRegister,
            command.Active
        );

        doctor.Person.Update(
            command.Dni,
            command.FirstName,
            command.LastName,
            command.BirthDate,
            command.Email,
            command.Phone,
            command.Address
        );

        var doctorUpdated = await _repository.UpdateAsync(doctor, ct);
        return doctorUpdated.Id;
    }
}