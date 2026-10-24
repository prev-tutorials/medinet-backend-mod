using Api.Modules.Doctors.Application.Contracts;
using Api.Modules.Doctors.Domain;
using Api.Modules.Doctors.Domain.ValueObjects;
using Api.Modules.Shared.Domain;
using MediatR;

namespace Api.Modules.Doctors.Application.Features.CreateDoctor;


public class CreateDoctorHandler(
    IDoctorRepository repository
): IRequestHandler<CreateDoctorCommand, Guid>
    {
        private readonly IDoctorRepository _repository = repository;
        
        public async Task<Guid> Handle(
            CreateDoctorCommand command,
            CancellationToken ct
        )
        {
            var doctor = command.Doctor;
            var person = command.Doctor.Person;

            var newPerson = Person.Create(
                person.Dni,
                person.FirstName,
                person.LastName,
                person.BirthDate,
                person.Email,
                person.Phone,
                person.Address 
            );

            var newDoctor = Doctor.Create(
                Speciality.From(doctor.Speciality),
                doctor.NRegister,
                false,
                newPerson
            );

            var response = await _repository.AddAsync(newDoctor, ct);
            return response.Id;
        }
    }