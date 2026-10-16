using Api.Modules.Doctors.Application.Contracts;
using MediatR;

namespace Api.Modules.Doctors.Application.Features.DeleteDoctor;

public class DeleteDoctorHandler(IDoctorRepository repository)
: IRequestHandler<DeleteDoctorCommand, Guid>
{
    private readonly IDoctorRepository _repository = repository;

    public async Task<Guid> Handle(
        DeleteDoctorCommand command, 
        CancellationToken ct
    )
    {
        var doctor = await _repository.GetById(command.Id, ct) 
            ?? throw new ArgumentException("Doctor not found", nameof(command.Id));

        var deletedDoctor = await _repository.DeleteAsync(doctor,ct);
        return deletedDoctor.Id;
    }
}