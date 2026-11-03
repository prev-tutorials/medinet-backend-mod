namespace Medinet.Feature.Cli.Generation;

public sealed record SliceContext(
    FeatureKind Kind,
    FeatureOperation Operation,
    string Name,
    string Entity,
    string Module,
    string Namespace,
    string Route,
    string MapMethod,
    LocatedType? Repository,
    LocatedType? Response
)
{
    public string ResponseType => Response?.TypeName ?? $"{Entity}Response";
}
