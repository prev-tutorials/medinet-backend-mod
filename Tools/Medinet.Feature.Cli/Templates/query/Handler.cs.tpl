using MediatR;
{{HandlerUsings}}
namespace {{Namespace}};

public class {{Name}}Handler{{HandlerParameters}}
    : IRequestHandler<{{Name}}Query, {{HandlerResponseType}}>
{
    public Task<{{HandlerResponseType}}> Handle(
        {{Name}}Query query,
        CancellationToken ct
    )
    {
{{HandlerPrologue}}        throw new NotImplementedException();
    }
}
