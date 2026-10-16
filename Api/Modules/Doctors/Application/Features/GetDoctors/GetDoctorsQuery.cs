using Api.Modules.Doctors.Application.Dtos;
using MediatR;

namespace Api.Modules.Doctors.Application.Features.GetDoctors;

public record GetDoctorsQuery(
    Guid? Id
) : IRequest<DoctorResponse[]>;
