using Api.Modules.Doctors.Domain;
using Api.Modules.Shared.Application.Contracts;

namespace Api.Modules.Doctors.Application.Contracts;

public interface IDoctorRepository:IRepository<Doctor>
{}