using System.Text;
using Medinet.Feature.Cli.Cli;
using Medinet.Feature.Cli.Configuration;

namespace Medinet.Feature.Cli.Generation;

public sealed class SliceGenerator(string root, FeatureCliConfig config)
{
    private static readonly string NewLine = Environment.NewLine;

    public void Generate(FeatureCliOptions options)
    {
        var moduleDirectory = Path.Combine(root, config.ModulesPath, options.Module);
        if (!Directory.Exists(moduleDirectory))
            throw new CliException(
                $"No existe el modulo '{options.Module}' en '{Path.Combine(root, config.ModulesPath)}'.");

        var featuresDirectory = FeaturesLocator.Find(moduleDirectory, config);
        var sliceDirectory = Path.Combine(featuresDirectory, options.Name);

        if (Directory.Exists(sliceDirectory))
            throw new CliException($"El slice ya existe: '{Path.GetRelativePath(root, sliceDirectory)}'.");

        var entity = EntityResolver.Resolve(
            moduleDirectory,
            options.Name,
            options.Entity,
            config);

        var operation = OperationResolver.Resolve(options.Kind, options.Name);
        var route = options.Route
                    ?? OperationResolver.DefaultRoute(options.Kind, operation);
        var mapMethod = OperationResolver.HttpMethod(options.Kind, operation);
        var moduleNamespace = NamespaceResolver.ModuleNamespace(config, options.Module);
        var sliceNamespace = NamespaceResolver.ForSlice(
            config,
            options.Module,
            moduleDirectory,
            featuresDirectory,
            options.Name);

        var repository = ModuleTypeLocator.Find(
            moduleDirectory,
            moduleNamespace,
            $"I{entity}Repository.cs",
            config);

        var response = ModuleTypeLocator.Find(
            moduleDirectory,
            moduleNamespace,
            $"{entity}Response.cs",
            config);

        var context = new SliceContext(
            options.Kind,
            operation,
            options.Name,
            entity,
            options.Module,
            sliceNamespace,
            route,
            mapMethod,
            repository,
            response);

        var files = BuildFiles(context);
        var relativeSlice = Path.GetRelativePath(root, sliceDirectory);

        if (options.DryRun)
        {
            Console.WriteLine($"Dry run: se crearia '{relativeSlice}'");
            foreach (var fileName in files.Keys.OrderBy(x => x, StringComparer.Ordinal))
                Console.WriteLine($"  - {fileName}");

            PrintWarnings(context);
            return;
        }

        var stagingDirectory = Path.Combine(
            featuresDirectory,
            $".{options.Name}.tmp-{Guid.NewGuid():N}");

        try
        {
            Directory.CreateDirectory(stagingDirectory);

            foreach (var (fileName, content) in files)
            {
                File.WriteAllText(
                    Path.Combine(stagingDirectory, fileName),
                    content,
                    new UTF8Encoding(encoderShouldEmitUTF8Identifier: false));
            }

            Directory.Move(stagingDirectory, sliceDirectory);
        }
        catch
        {
            if (Directory.Exists(stagingDirectory))
                Directory.Delete(stagingDirectory, recursive: true);

            throw;
        }

        Console.WriteLine($"Slice creado en '{relativeSlice}':");
        foreach (var fileName in files.Keys.OrderBy(x => x, StringComparer.Ordinal))
            Console.WriteLine($"  - {fileName}");

        Console.WriteLine();
        Console.WriteLine("Registra el endpoint manualmente:");
        Console.WriteLine($"  group.Map{context.Name}();");

        PrintWarnings(context);
    }

    private static Dictionary<string, string> BuildFiles(SliceContext context)
    {
        var tokens = BuildTokens(context);

        return context.Kind switch
        {
            FeatureKind.Command => new Dictionary<string, string>(StringComparer.Ordinal)
            {
                [$"{context.Name}Command.cs"] = TemplateRenderer.Render(
                    "command",
                    "Command.cs.tpl",
                    tokens),
                [$"{context.Name}Handler.cs"] = TemplateRenderer.Render(
                    "command",
                    "Handler.cs.tpl",
                    tokens),
                [$"{context.Name}Endpoint.cs"] = TemplateRenderer.Render(
                    "command",
                    "Endpoint.cs.tpl",
                    tokens)
            },
            FeatureKind.Query => new Dictionary<string, string>(StringComparer.Ordinal)
            {
                [$"{context.Name}Query.cs"] = TemplateRenderer.Render(
                    "query",
                    "Query.cs.tpl",
                    tokens),
                [$"{context.Name}Handler.cs"] = TemplateRenderer.Render(
                    "query",
                    "Handler.cs.tpl",
                    tokens),
                [$"{context.Name}Endpoint.cs"] = TemplateRenderer.Render(
                    "query",
                    "Endpoint.cs.tpl",
                    tokens)
            },
            _ => throw new ArgumentOutOfRangeException(nameof(context))
        };
    }

    private static Dictionary<string, string> BuildTokens(SliceContext context)
    {
        var endpoint = context.Kind == FeatureKind.Command
            ? CommandEndpoint(context)
            : QueryEndpoint(context);

        return new Dictionary<string, string>(StringComparer.Ordinal)
        {
            ["Namespace"] = context.Namespace,
            ["Name"] = context.Name,
            ["Entity"] = context.Entity,
            ["Module"] = context.Module,
            ["Route"] = context.Route,
            ["MapMethod"] = context.MapMethod,

            ["RequestDeclaration"] = RequestDeclaration(context),
            ["CommandDeclaration"] = CommandDeclaration(context),
            ["ValidatorDeclaration"] = ValidatorDeclaration(context),
            ["HandlerParameters"] = HandlerParameters(context),
            ["HandlerUsings"] = HandlerUsings(context),
            ["HandlerPrologue"] = HandlerPrologue(context),

            ["QueryUsings"] = QueryUsings(context),
            ["ResponseDeclaration"] = ResponseDeclaration(context),
            ["QueryDeclaration"] = QueryDeclaration(context),
            ["ResponseType"] = context.ResponseType,
            ["HandlerResponseType"] = HandlerResponseType(context),

            ["EndpointParameters"] = endpoint.Parameters,
            ["EndpointBody"] = endpoint.Body
        };
    }

    private static string RequestDeclaration(SliceContext context)
        => context.Operation == FeatureOperation.Delete
            ? string.Empty
            : $"public record {context.Name}Request();";

    private static string CommandDeclaration(SliceContext context)
    {
        return context.Operation switch
        {
            FeatureOperation.Delete =>
                $"public record {context.Name}Command(Guid Id) : IRequest<Guid>;",

            FeatureOperation.Update =>
                $"""
                public record {context.Name}Command(
                    Guid Id,
                    {context.Name}Request Request
                ) : IRequest<Guid>;
                """,

            _ =>
                $"""
                public record {context.Name}Command(
                    {context.Name}Request Request
                ) : IRequest<Guid>;
                """
        };
    }

    private static string ValidatorDeclaration(SliceContext context)
    {
        if (context.Operation == FeatureOperation.Delete)
            return string.Empty;

        if (context.Operation == FeatureOperation.Update)
        {
            return $$"""
                public class {{context.Name}}Validator : AbstractValidator<{{context.Name}}Command>
                {
                    public {{context.Name}}Validator()
                    {
                        RuleFor(x => x.Id)
                            .NotEmpty();

                        RuleFor(x => x.Request)
                            .NotNull();

                        // TODO: agregar reglas de validacion.
                    }
                }
                """;
        }

        return $$"""
            public class {{context.Name}}Validator : AbstractValidator<{{context.Name}}Command>
            {
                public {{context.Name}}Validator()
                {
                    RuleFor(x => x.Request)
                        .NotNull();

                    // TODO: agregar reglas de validacion.
                }
            }
            """;
    }

    private static string HandlerParameters(SliceContext context)
    {
        if (context.Repository is null)
            return string.Empty;

        return $"({NewLine}    {context.Repository.TypeName} repository{NewLine})";
    }

    private static string HandlerPrologue(SliceContext context)
        => context.Repository is null
            ? string.Empty
            : $"        ArgumentNullException.ThrowIfNull(repository);{NewLine}";

    private static string HandlerUsings(SliceContext context)
    {
        var namespaces = new SortedSet<string>(StringComparer.Ordinal);

        if (context.Repository is not null
            && !string.Equals(context.Repository.Namespace, context.Namespace, StringComparison.Ordinal))
        {
            namespaces.Add(context.Repository.Namespace);
        }

        if (context.Kind == FeatureKind.Query
            && context.Response is not null
            && !string.Equals(context.Response.Namespace, context.Namespace, StringComparison.Ordinal))
        {
            namespaces.Add(context.Response.Namespace);
        }

        return FormatUsings(namespaces);
    }

    private static string QueryUsings(SliceContext context)
    {
        var namespaces = new SortedSet<string>(StringComparer.Ordinal);

        if (context.Response is not null
            && !string.Equals(context.Response.Namespace, context.Namespace, StringComparison.Ordinal))
        {
            namespaces.Add(context.Response.Namespace);
        }

        return FormatUsings(namespaces);
    }

    private static string FormatUsings(IEnumerable<string> namespaces)
    {
        var lines = namespaces
            .Select(ns => $"using {ns};")
            .ToArray();

        return lines.Length == 0
            ? string.Empty
            : string.Join(NewLine, lines) + NewLine;
    }

    private static string ResponseDeclaration(SliceContext context)
        => context.Response is null
            ? $"public record {context.ResponseType};"
            : string.Empty;

    private static string HandlerResponseType(SliceContext context)
        => context.Kind == FeatureKind.Query
           && context.Operation != FeatureOperation.GetById
            ? $"{context.ResponseType}[]"
            : context.ResponseType;

    private static string QueryDeclaration(SliceContext context)
        => context.Operation == FeatureOperation.GetById
            ? $"public record {context.Name}Query(Guid Id) : IRequest<{context.ResponseType}>;"
            : $"public record {context.Name}Query() : IRequest<{context.ResponseType}[]>;";

    private static EndpointSnippet CommandEndpoint(SliceContext context)
    {
        return context.Operation switch
        {
            FeatureOperation.Update => new EndpointSnippet(
                Parameters($"Guid id,", $"{context.Name}Request request,"),
                Block(
                    $"var command = new {context.Name}Command(id, request);",
                    "var result = await sender.Send(command, ct);",
                    "return Results.Ok(new { id = result });")),

            FeatureOperation.Delete => new EndpointSnippet(
                Parameters("Guid id,"),
                Block(
                    $"var command = new {context.Name}Command(id);",
                    "var result = await sender.Send(command, ct);",
                    "return Results.Ok(new { id = result });")),

            _ => new EndpointSnippet(
                Parameters($"{context.Name}Request request,"),
                Block(
                    $"var command = new {context.Name}Command(request);",
                    "var result = await sender.Send(command, ct);",
                    "return Results.Ok(new { id = result });"))
        };
    }

    private static EndpointSnippet QueryEndpoint(SliceContext context)
    {
        return context.Operation == FeatureOperation.GetById
            ? new EndpointSnippet(
                Parameters("Guid id,"),
                Block(
                    $"var query = new {context.Name}Query(id);",
                    "var result = await sender.Send(query, ct);",
                    "return Results.Ok(result);"))
            : new EndpointSnippet(
                string.Empty,
                Block(
                    $"var query = new {context.Name}Query();",
                    "var result = await sender.Send(query, ct);",
                    "return Results.Ok(result);"));
    }

    private static string Parameters(params string[] lines)
        => string.Concat(lines.Select(line => $"        {line}{NewLine}"));

    private static string Block(params string[] lines)
        => string.Join(NewLine, lines.Select(line => $"        {line}"));

    private static void PrintWarnings(SliceContext context)
    {
        if (context.Repository is null)
        {
            Console.WriteLine();
            Console.WriteLine(
                $"Advertencia: no se encontro I{context.Entity}Repository; el handler se genero sin inyeccion.");
        }

        if (context.Kind == FeatureKind.Query && context.Response is null)
        {
            Console.WriteLine();
            Console.WriteLine(
                $"Advertencia: no se encontro {context.Entity}Response; se genero un response local en el query.");
        }
    }

    private sealed record EndpointSnippet(string Parameters, string Body);
}
