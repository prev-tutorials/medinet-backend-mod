
using Api.Modules.Shared.Domain;

namespace Api.Modules.Shared.Application.Dtos.Persons;

public record PersonResponse(
    Guid Id,
    string Dni,
    string FirstName,
    string LastName,
    DateTime? BirthDate,
    PersonContactResponse Contact
);



public static class PersonResponseMapper
{
    public static PersonResponse ToResponse(this Person person)
        => new(
            person.Id,
            person.Dni,
            person.FirstName,
            person.LastName,
            person.BirthDate,
            person.PersonContact.ToResponse()
        );
}