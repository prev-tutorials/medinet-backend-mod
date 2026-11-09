using Api.Modules.Doctors.Domain.ValueObjects;
using Api.Modules.Shared.Domain;

namespace Api.Modules.Doctors.Domain;


public class Doctor:Entity
{
    public Speciality Speciality { get; private set; } = null!;
    public string NRegister { get; private set; } = null!;
    public bool Active { get; private set; }
    public Guid PersonId {get; private set; }
    public Person Person { get; private set; } = null!;

    protected Doctor(){}

    private Doctor(
        Speciality speciality,
        string nRegister,
        bool active,
        Person person
    )
    {
        Speciality = speciality;
        NRegister  = nRegister;
        Active     = active;
        Person     = person;

    }


    public static Doctor Create(
        Speciality speciality,
        string nRegister,
        bool active,
        Person person
    )
    {
        return new(
            speciality,
            nRegister,
            active,
            person
        );
    }



    public void Update(
        Speciality? speciality,
        string? nRegister,
        bool? active
        // Person? person 
    )
    {
        if(speciality != null) Speciality = speciality;
        if(nRegister != null)  NRegister = nRegister;
        if(active.HasValue)    Active = active.Value;
        // if(person != null) Person = person;
    }
}