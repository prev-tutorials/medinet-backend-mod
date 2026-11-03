using MediatR;
{{HandlerUsings}}
namespace {{Namespace}};

public class {{Name}}Handler{{HandlerParameters}}
    : IRequestHandler<{{Name}}Command, Guid>
{
    public Task<Guid> Handle(
        {{Name}}Command command,
        CancellationToken ct
    )
    {
{{HandlerPrologue}}        throw new NotImplementedException();
    }
}
