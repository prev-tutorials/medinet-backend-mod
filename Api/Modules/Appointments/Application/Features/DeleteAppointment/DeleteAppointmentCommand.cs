using MediatR;

namespace Api.Modules.Appointments.Application.Features.DeleteAppointment;



public record DeleteAppointmentCommand(Guid Id) : IRequest<Guid>;


