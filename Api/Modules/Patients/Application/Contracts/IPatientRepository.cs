using Api.Modules.Patients.Domain;
using Api.Modules.Shared.Application.Contracts;

namespace Api.Modules.Patients.Application.Contracts;

public interface IPatientRepository:IRepository<Patient>
{}