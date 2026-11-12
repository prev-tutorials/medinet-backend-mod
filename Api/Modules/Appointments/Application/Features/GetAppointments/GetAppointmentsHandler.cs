using Api.Modules.Appointments.Application.Contracts;
using Api.Modules.Appointments.Application.Dtos;
using MediatR;

namespace Api.Modules.Appointments.Application.Features.GetAppointments;

public class GetAppointmentsHandler(
    IAppointmentRepository repository,
    IDoctorService doctorService,
    IPatientService patientService
): IRequestHandler<GetAppointmentsQuery, AppointmentResponse[]>
{
    private readonly IAppointmentRepository _repository = repository;
    private readonly IDoctorService _doctorService = doctorService;
    private readonly IPatientService _patientService = patientService;

    public async Task<AppointmentResponse[]> Handle(
        GetAppointmentsQuery query,
        CancellationToken ct
    )
    {
        var appointments = await _repository.GetAllAsync(ct);

        var doctorIds  = appointments.Select(a => a.DoctorId).Distinct().ToArray();
        var patientIds = appointments.Select(a => a.PatientId).Distinct().ToArray();

        var doctors  = await _doctorService.GetDoctorsByIdAsync(doctorIds, ct);
        var patients = await _patientService.GetPatientsByIdAsync(patientIds, ct);

        var doctorsById  = doctors.ToDictionary(d => d.Id);
        var patientsById = patients.ToDictionary(p => p.Id);
        
        return appointments
            .Select(a => a.ToResponse(
                doctorsById.GetValueOrDefault(a.DoctorId),
                patientsById.GetValueOrDefault(a.PatientId)
            ))
            .ToArray();
    }
}
