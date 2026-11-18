namespace Api.Modules.Appointments.Domain;


public static class AppointmentSchedule
{
    public static readonly TimeOnly Start = new(8, 0);
    public static readonly TimeOnly End = new(17, 0);
    public const int SlotMinutes = 30;

    public static IEnumerable<TimeOnly> Slots()
    {
        for (var time = Start; time < End; time = time.AddMinutes(SlotMinutes))
            yield return time;
    }
}
