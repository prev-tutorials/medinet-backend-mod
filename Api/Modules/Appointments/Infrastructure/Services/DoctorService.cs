

using Api.Modules.Appointments.Application.Contracts;
using Api.Modules.Appointments.Application.Dtos;
using Api.Modules.Doctors.Application.Contracts;
using Api.Modules.Doctors.Domain.ValueObjects;
using FluentValidation;
using FluentValidation.Results;

namespace Api.Modules.Appointments.Infrastructure.Services;

public class DoctorService(IDoctorReader doctorReader) : IDoctorService
{
    private readonly IDoctorReader _doctorReader = doctorReader;

    public async Task<DoctorResponse[]> GetDoctorsByIdAsync(
        Guid[] ids, 
        CancellationToken ct
    )
    {   
      var doctors = await _doctorReader.GetByIdsAsync(ids, ct);
      return doctors.Select(d => new DoctorResponse(
        d.Id,
        d.Speciality,
        d.NRegister,
        d.Active,
        d.Person
      )).ToArray();
    }


    public async Task<DoctorResponse[]> GetFilteredAsync(
        Guid? id,
        string? speciality,
        string? search,
        CancellationToken ct
    )
    {
        Speciality? specialityFilter = string.IsNullOrWhiteSpace(speciality)
            ? null
            : Speciality.From(speciality);

        var doctors = await _doctorReader.GetFilteredAsync(
            id,
            specialityFilter,
            search,
            ct
        );

        return doctors.Select(d => new DoctorResponse(
            d.Id,
            d.Speciality,
            d.NRegister,
            d.Active,
            d.Person
        )).ToArray();
    }


    public async Task DoctorExist(
        Guid? doctorId,
        CancellationToken ct
    )
    {
        if (doctorId.HasValue)
        {
            var did = doctorId.Value;
            var doctors = await _doctorReader.GetByIdsAsync([did], ct);

            if (doctors.Length == 0)
            //    throw new ValidationException("Error: doctor with id not exist");
                throw new ValidationException(
                    [new ValidationFailure("DoctorId", "Doctor does not exist")]
                );
        }
    }

}