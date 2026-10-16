using MediatR;

namespace Api.Modules.Doctors.Application.Features.DeleteDoctor;


public record DeleteDoctorCommand(
    Guid Id
): IRequest<Guid>;