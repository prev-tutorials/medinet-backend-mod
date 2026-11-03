using Api.Modules.Appointments.Domain;
using Api.Modules.Shared.Application.Contracts;

namespace Api.Modules.Appointments.Application.Contracts;


public interface IAppointmentRepository:IRepository<Appointment>;