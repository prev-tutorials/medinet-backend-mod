using Api.Modules.Doctors.Application.Contracts;
using Api.Modules.Doctors.Application.Dtos;
using Api.Modules.Doctors.Domain.ValueObjects;
using MediatR;

namespace Api.Modules.Doctors.Application.Features.GetDoctors;

public class GetDoctorsHandler(
    IDoctorRepository repository
): IRequestHandler<GetDoctorsQuery, DoctorResponse[]>
{
    private readonly IDoctorRepository _repository = repository;

    public async Task<DoctorResponse[]> Handle(
        GetDoctorsQuery query, 
        CancellationToken ct
    )
    {
        var request = query.Request;

        var speciality = string.IsNullOrWhiteSpace(request.Speciality)
            ? null
            : Speciality.From(request.Speciality);

        var doctors = await _repository.GetFilteredAsync(
            request.Id,
            speciality,
            request.Search,
            ct
        );

        return doctors
            .Select(d => d.ToResponse())
            .ToArray();
    }
}