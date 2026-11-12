using Api.Modules.Doctors.Application.Dtos;
using Api.Modules.Patients.Application.Dtos;

namespace Api.Modules.Appointments.Application.Contracts;

public interface IAppointmentService
{
    Task<DoctorResponse[]> GetDoctorsByIdAsync(
        Guid[] ids,
        CancellationToken ct
    );

    Task<PatientResponse[]> GetPatientsByIdAsync(
        Guid[] ids,
        CancellationToken ct 
    );


    Task DoctorExist(
        Guid? doctorId,
        CancellationToken ct
    );

    Task PatientExist(
        Guid? patientId,
        CancellationToken ct
    );
}