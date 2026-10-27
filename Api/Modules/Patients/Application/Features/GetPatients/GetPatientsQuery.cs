using Api.Modules.Patients.Application.Dtos;
using MediatR;

namespace Api.Modules.Patients.Application.Features.GetPatients;

public record GetPatientsQuery(
    Guid? Id
) : IRequest<PatientResponse[]>;

