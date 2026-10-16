using Api.Modules.Shared.Domain;

namespace Api.Modules.Patients.Domain;

public class Patient:Entity
{
    public Guid PersonId {get; set; }
    public Person Person { get; set; } = null!;
}