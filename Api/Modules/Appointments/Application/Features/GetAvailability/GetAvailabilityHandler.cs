using Api.Modules.Appointments.Application.Contracts;
using Api.Modules.Appointments.Application.Dtos;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace Api.Modules.Appointments.Application.Features.GetAvailability;


public class GetAvailabilityHandler(
    IAppointmentRepository repository,
    IDoctorService doctorService
): IRequestHandler<GetAvailabilityQuery, DoctorAvailabilityResponse[]>
{
    private readonly IAppointmentRepository _repository = repository;
    private readonly IDoctorService _doctorService = doctorService;

    public async Task<DoctorAvailabilityResponse[]> Handle(
        GetAvailabilityQuery query,
        CancellationToken ct
    )
    {
        var request = query.Request;

        if (string.IsNullOrWhiteSpace(request.Speciality))
            throw new ValidationException(
                [new ValidationFailure("Speciality", "Speciality is required")]
            );

        if (request.Date is null)
            throw new ValidationException(
                [new ValidationFailure("Date", "Date is required")]
            );

        var date = request.Date.Value;

        var doctors = await _doctorService.GetDoctorsBySpecialityAsync(
            request.Speciality,
            ct
        );

        if (doctors.Length == 0)
            return [];

        var doctorIds = doctors.Select(d => d.Id).ToArray();

        var appointments = await _repository.GetByDoctorsAndDateAsync(
            doctorIds,
            date,
            ct
        );

        var reservationsByDoctor = appointments
            .GroupBy(a => a.DoctorId)
            .ToDictionary(
                g => g.Key,
                g => g.Select(a => a.Time)
                    .Distinct()
                    .OrderBy(time => time)
                    .Select(time => time.ToString("HH:mm"))
                    .ToArray()
            );

        return doctors
            .Select(doctor => new DoctorAvailabilityResponse(
                doctor,
                date,
                reservationsByDoctor.GetValueOrDefault(doctor.Id) ?? []
            ))
            .ToArray();
    }
}
