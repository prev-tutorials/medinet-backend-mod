using Api.Modules.Appointments.Application.Contracts;
using Api.Modules.Appointments.Application.Dtos;
using Api.Modules.Doctors.Application.Contracts;
using MediatR;

namespace Api.Modules.Appointments.Application.Features.GetAppointments;

public class GetAppointmentsHandler(
    IAppointmentRepository repository,
    IDoctorReader doctorReader
    // IPatientReader patientReader
): IRequestHandler<GetAppointmentsQuery, AppointmentResponse[]>
{
    private readonly IAppointmentRepository _repository = repository;
    private readonly IDoctorReader _doctorReader = doctorReader;
    // private readonly IPatientReader _patientReader = patientReader;


    public async Task<AppointmentResponse[]> Handle(
        GetAppointmentsQuery query,
        CancellationToken ct
    )
    {
        var appointments = await _repository.GetAllAsync(ct);
        var doctorIds  = appointments.Select(a => a.DoctorId).Distinct().ToArray();
        var doctors  = await _doctorReader.GetByIdsAsync(doctorIds, ct);
        // var patients = await _patientReader.GetByIdsAsync(patientIds, ct);
        
        return appointments
            .Select(a => a.ToResponse(
                doctors.GetValueOrDefault(a.DoctorId)
                // patients.GetValueOrDefault(a.PatientId)
            ))
            .ToArray();
    }
}
