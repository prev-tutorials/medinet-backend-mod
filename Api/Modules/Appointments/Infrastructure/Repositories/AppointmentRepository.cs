using Api.Modules.Appointments.Application.Contracts;
using Api.Modules.Appointments.Domain;
using Api.Modules.Shared.Infrastructure.Persistence;
using Api.Modules.Shared.Infrastructure.Repositories;

namespace Api.Modules.Appointments.Infrastructure.Repositories;


public class AppointmentRepository(
    AppDbContext context
):Repository<Appointment>(context), IAppointmentRepository
{}