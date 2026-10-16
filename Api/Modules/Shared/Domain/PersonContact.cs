namespace Api.Modules.Shared.Domain;

public class PersonContact: Entity
{
    public string? Email { get; private set; }
    public string? Phone { get; private set; }
    public string? Address { get; private set; }

    protected PersonContact(){}

    private PersonContact(
        string? email,
        string? phone,
        string? address
    )
    {
        Email   = email;
        Phone   = phone;
        Address = address;
    }


    public static PersonContact Create(
        string? email,
        string? phone,
        string? address
    )
    {
        return new PersonContact(
            email,
            phone,
            address
        );
    }


    public void Update(
        string? email,
        string? phone,
        string? address
    )
    {
        if(email != null) Email = email;
        if(phone != null) Phone = phone;
        if(address != null) Address = address;
    }
}