namespace Medinet.Feature.Cli.Generation;

public static class OperationResolver
{
    public static FeatureOperation Resolve(FeatureKind kind, string name)
    {
        if (kind == FeatureKind.Query)
        {
            return name.StartsWith("Get", StringComparison.Ordinal)
                   && name.Contains("ById", StringComparison.Ordinal)
                ? FeatureOperation.GetById
                : FeatureOperation.Get;
        }

        if (name.StartsWith("Create", StringComparison.Ordinal))
            return FeatureOperation.Create;

        if (name.StartsWith("Update", StringComparison.Ordinal))
            return FeatureOperation.Update;

        if (name.StartsWith("Delete", StringComparison.Ordinal))
            return FeatureOperation.Delete;

        return FeatureOperation.Custom;
    }

    public static string HttpMethod(FeatureKind kind, FeatureOperation operation)
        => kind switch
        {
            FeatureKind.Query => "MapGet",
            _ => operation switch
            {
                FeatureOperation.Create or FeatureOperation.Custom => "MapPost",
                FeatureOperation.Update => "MapPatch",
                FeatureOperation.Delete => "MapDelete",
                _ => "MapPost"
            }
        };

    public static string DefaultRoute(FeatureKind kind, FeatureOperation operation)
        => kind switch
        {
            FeatureKind.Query => operation == FeatureOperation.GetById
                ? "/{id:guid}"
                : "/",
            _ => operation switch
            {
                FeatureOperation.Update or FeatureOperation.Delete => "/{id:guid}",
                _ => "/"
            }
        };
}
