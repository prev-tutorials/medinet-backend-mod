using Medinet.Feature.Cli.Cli;
using Medinet.Feature.Cli.Configuration;

namespace Medinet.Feature.Cli.Generation;

public static class NamespaceResolver
{
    public static string ModuleNamespace(FeatureCliConfig config, string module)
        => $"{config.RootNamespace}.Modules.{module}";

    public static string ForSlice(
        FeatureCliConfig config,
        string module,
        string moduleDirectory,
        string featuresDirectory,
        string sliceName
    )
    {
        var segments = PathSegments(moduleDirectory, featuresDirectory);
        return Combine(
            ModuleNamespace(config, module),
            [.. segments, sliceName]);
    }

    public static string ForDirectory(string moduleNamespace, string moduleDirectory, string directory)
    {
        var segments = PathSegments(moduleDirectory, directory);
        return Combine(moduleNamespace, segments);
    }

    private static string[] PathSegments(string baseDirectory, string directory)
    {
        var relative = Path.GetRelativePath(baseDirectory, directory);
        if (relative == ".")
            return [];

        return relative
            .Split(
                [Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar],
                StringSplitOptions.RemoveEmptyEntries)
            .Select(ValidateSegment)
            .ToArray();
    }

    private static string Combine(string baseNamespace, params string[] segments)
    {
        var all = new List<string> { baseNamespace };
        all.AddRange(segments.Where(segment => !string.IsNullOrWhiteSpace(segment)));
        return string.Join('.', all);
    }

    private static string ValidateSegment(string segment)
    {
        if (!IsValidIdentifier(segment))
            throw new CliException(
                $"La carpeta '{segment}' no forma un namespace C# valido.");

        return segment;
    }

    private static bool IsValidIdentifier(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return false;

        if (!char.IsLetter(value[0]) && value[0] != '_')
            return false;

        return value.Skip(1).All(c => char.IsLetterOrDigit(c) || c == '_');
    }
}
