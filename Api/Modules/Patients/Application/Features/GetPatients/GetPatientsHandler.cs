using Api.Modules.Patients.Application.Contracts;
using Api.Modules.Patients.Application.Dtos;
using MediatR;

namespace Api.Modules.Patients.Application.Features.GetPatients;

public class GetPatientsHandler(
    IPatientRepository repository
) : IRequestHandler<GetPatientsQuery, PatientResponse[]>
{
    private readonly IPatientRepository _repository = repository;

    public async Task<PatientResponse[]> Handle(
        GetPatientsQuery query,
        CancellationToken ct
    )
    {
        var patients = await _repository.GetFilteredAsync(
            query.Request.Id,
            query.Request.Dni,
            query.Request.Search,
            ct
        );

        return patients
            .Select(p => p.ToResponse())
            .ToArray();
    }
}
