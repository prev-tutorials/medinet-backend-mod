using Api.Modules.Doctors.Application.Contracts;
using Api.Modules.Doctors.Application.Dtos;
using MediatR;

namespace Api.Modules.Doctors.Application.Features.GetDoctors;

public class GetDoctorsHandler(
    IDoctorRepository repository
)
: IRequestHandler<GetDoctorsQuery, DoctorResponse[]>
{
    private readonly IDoctorRepository _repository = repository;

    public async Task<DoctorResponse[]> Handle(
        GetDoctorsQuery query, 
        CancellationToken ct
    )
    {
        var doctores = await _repository.GetAllAsync(ct);
        
        return doctores
            .Select(d => d.ToResponse())
            .ToArray();
    }
}