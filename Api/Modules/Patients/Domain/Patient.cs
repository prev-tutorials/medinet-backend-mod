using Api.Modules.Shared.Domain;

namespace Api.Modules.Patients.Domain;

public class Patient:Entity
{
    public string Nhc { get; set; } = string.Empty;
    public Guid PersonId {get; set; }
    public Person Person { get; set; } = null!;


    protected Patient(){}

    private Patient(
        string nhc,
        Person person
    )
    {
        Nhc = nhc;
        Person = person;
    }


    public static Patient Create(
        string nhc,
        Person person
    )
    {
        return new(
            nhc,
            person
        );
    }

    public void Update(
        string? nhc
    )
    {
        if(nhc != null) Nhc = nhc;
    }
}