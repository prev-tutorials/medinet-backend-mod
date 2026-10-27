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

        DateTime? birth = (!string.IsNullOrWhiteSpace(birthDate))
            ? ParseBirthDate(birthDate)
            : null;


        return new Person(
            dni,
            firstName,
            lastName,
            birth,
            contact
        );
    }


    public void Update(
        string? dni,
        string? firstName,
        string? lastName,
        string? birthDate,
        string? Email,
        string? Phone,
        string? Address
    )
    {
        if(dni != null) Dni = dni;
        if(firstName != null) FirstName = firstName;
        if(lastName != null) LastName = lastName;
        if (!string.IsNullOrWhiteSpace(birthDate))
            BirthDate = ParseBirthDate(birthDate);

        PersonContact.Update(
            Email, 
            Phone, 
            Address
        );
    }


    private static DateTime? ParseBirthDate(string birthDate)
        => DateTime.ParseExact(
            birthDate,
            ["yyyy-MM-dd"],
            CultureInfo.InvariantCulture, 
            DateTimeStyles.None
        );
}