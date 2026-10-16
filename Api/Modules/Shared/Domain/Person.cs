using System.Globalization;

namespace Api.Modules.Shared.Domain;

public class Person:Entity
{
    public string Dni { get; private set; } = null!;
    public string FirstName { get; private set; } = null!;
    public string LastName { get; private set; } = null!;
    public DateTime? BirthDate { get; private set; }
    public Guid PersonContactId { get; private set; }
    public PersonContact PersonContact { get; private set; } = null!;

    public Person(){}

    private Person(
        string dni,
        string firstName,
        string lastName,
        DateTime? birthDate,
        PersonContact personContact
    )
    {
        Dni           = dni;
        FirstName     = firstName;
        LastName      = lastName;
        BirthDate     = birthDate;
        PersonContact = personContact;
    }


    public static Person Create(
        string dni,
        string firstName,
        string lastName,
        string? birthDate,
        string? email,
        string? phone,
        string? address
    )
    {
        var contact = PersonContact.Create(
            email, 
            phone, 
            address
        );

        DateTime? birth = null;

        if (!string.IsNullOrWhiteSpace(birthDate))
            birth = DateTime.ParseExact(
                birthDate,
                ["dd/MM/yyyy", "d/M/yyyy", "yyyy-MM-dd"],
                CultureInfo.InvariantCulture,
                DateTimeStyles.None
            );


        return new Person(
            dni,
            firstName,
            lastName,
            birth,
            contact
        );
    }
}