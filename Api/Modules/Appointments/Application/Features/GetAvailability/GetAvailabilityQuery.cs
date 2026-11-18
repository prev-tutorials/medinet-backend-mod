using Api.Modules.Appointments.Application.Dtos;
using MediatR;

namespace Api.Modules.Appointments.Application.Features.GetAvailability;


public record GetAvailabilityRequest(
    string? Speciality,
    DateOnly? Date
);

public record GetAvailabilityQuery(
    GetAvailabilityRequest Request
) : IRequest<DoctorAvailabilityResponse[]>;
