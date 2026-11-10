using Api.Modules.Patients.Application.Dtos;
using MediatR;

namespace Api.Modules.Patients.Application.Features.GetPatients;

public record GetPatientsRequest(
    Guid? Id,
    string? Search
);

public record GetPatientsQuery(
    GetPatientsRequest Request
) : IRequest<PatientResponse[]>;

