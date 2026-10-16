using Api.Modules.Doctors.Application.Contracts;
using Api.Modules.Doctors.Domain;
using Api.Modules.Doctors.Domain.ValueObjects;
using Api.Modules.Shared.Domain;
using FluentValidation;
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
            var person = Person.Create(
                command.Dni,
                command.FirstName,
                command.LastName,
                command.BirthDate,
                command.Email,
                command.Phone,
                command.Address 
            );

            var doctor = Doctor.Create(
                Speciality.From(command.Speciality),
                command.NRegister,
                false,
                person
            );

            var newDoctor = await _repository.AddAsync(doctor, ct);
            return newDoctor.Id;
        }
    }