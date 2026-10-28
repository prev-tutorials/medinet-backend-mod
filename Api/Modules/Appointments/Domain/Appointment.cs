using Api.Modules.Shared.Domain;

namespace Api.Modules.Appointments.Domain;


public class Appointment:Entity
{
    public Guid DoctorId { get; private set; }
    public Guid PatientId { get; private set; }
    public AppointmentStatus Status { get; private set; }
    public DateOnly Date { get; private set; }
    public TimeOnly Time { get; private set; }
    public string? Reason { get; private set; }


     private Appointment() { }

    private Appointment(
        Guid doctorId,
        Guid patientId,
        DateOnly date,
        TimeOnly time,
        string? reason,
        AppointmentStatus status
    )
    {
        DoctorId = doctorId;
        PatientId = patientId;
        Date = date;
        Time = time;
        Reason = reason;
        Status = status;
    }


    public static Appointment Create(
        Guid doctorId,
        Guid patientId,
        DateOnly date,
        TimeOnly time,
        string? reason = null
    )
    {
        if (doctorId == Guid.Empty) 
            throw new ArgumentException("DoctorId is required");

        if (patientId == Guid.Empty) 
            throw new ArgumentException("PatientId is required");

        if (date == default) 
            throw new ArgumentException("Date is required");

        if (time == default) 
            throw new ArgumentException("Time is required");

        return new Appointment(
            doctorId,
            patientId,
            date,
            time,
            reason?.Trim(),
            AppointmentStatus.Scheduled
        );
    }


    public void Cancel()
    {
        if (Status == AppointmentStatus.Completed)
            throw new InvalidOperationException("Cannot cancel a completed appointment");
        
        Status = AppointmentStatus.Cancelled;
    }


    public void Complete()
    {
        if (Status == AppointmentStatus.Cancelled)
            throw new InvalidOperationException("Cannot complete a cancelled appointment");
        
        Status = AppointmentStatus.Completed;
    }

}
