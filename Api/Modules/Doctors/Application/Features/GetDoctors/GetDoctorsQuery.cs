using Api.Modules.Doctors.Application.Dtos;
using MediatR;

namespace Api.Modules.Doctors.Application.Features.GetDoctors;

public record GetDoctorsRequest(
    Guid? Id,
    string? Speciality,
    string? Search
);

public record GetDoctorsQuery(
    GetDoctorsRequest Request
) : IRequest<DoctorResponse[]>;
