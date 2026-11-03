using MediatR;

namespace {{Namespace}};

public static class {{Name}}Endpoint
{
    public static void Map{{Name}}(this IEndpointRouteBuilder endpoints)
        => endpoints.{{MapMethod}}("{{Route}}", {{Name}});

    private static async Task<IResult> {{Name}}(
{{EndpointParameters}}        ISender sender,
        CancellationToken ct
    )
    {
{{EndpointBody}}
    }
}
