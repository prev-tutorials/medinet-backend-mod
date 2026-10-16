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

    public Doctor(){}

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
        return new Doctor(
            speciality,
            nRegister,
            active,
            person
        );
    }
}