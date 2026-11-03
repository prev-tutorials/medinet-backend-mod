using Api.Modules.Appointments.Application.Contracts;
using Api.Modules.Appointments.Application.Dtos;
using MediatR;

namespace Api.Modules.Appointments.Application.Features.GetAppointments;

public class GetAppointmentsHandler(
    IAppointmentRepository repository
): IRequestHandler<GetAppointmentsQuery, AppointmentResponse[]>
{
    private readonly IAppointmentRepository _repository = repository;

    public async Task<AppointmentResponse[]> Handle(
        GetAppointmentsQuery query,
        CancellationToken ct
    )
    {
        var appointments = await _repository.GetAllAsync(ct);
        
        return appointments
            .Select(a => a.ToResponse())
            .ToArray();
    }
}
