
using Api.Modules.Shared.Domain;

namespace Api.Modules.Shared.Application.Dtos.Persons;

public record PersonContactResponse(
    Guid Id,
    string? Email,
    string? Phone,
    string? Address
);


public static class PersonContactResponseMapper
{
    public static PersonContactResponse ToResponse(this PersonContact personContact)
        => new(
            personContact.Id,
            personContact.Email,
            personContact.Phone,
            personContact.Address
        );
}