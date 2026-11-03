using System.Text.RegularExpressions;
using Medinet.Feature.Cli.Cli;
using Medinet.Feature.Cli.Configuration;

namespace Medinet.Feature.Cli.Generation;

public static class EntityResolver
{
    private static readonly Regex TypeDeclaration = new(
        @"\b(?:class|record|struct)\s+([A-Za-z_][A-Za-z0-9_]*)",
        RegexOptions.Compiled
    );

    public static string Resolve(
        string moduleDirectory,
        string sliceName,
        string? explicitEntity,
        FeatureCliConfig config
    )
    {
        if (explicitEntity is not null)
            return explicitEntity;

        var domainDirectory = Path.Combine(moduleDirectory, "Domain");
        if (!Directory.Exists(domainDirectory))
            throw new CliException(
                $"No se pudo inferir la entidad: no existe '{domainDirectory}'. Usa --entity.");

        var candidates = new HashSet<string>(StringComparer.Ordinal);

        foreach (var file in FileSystemScanner.EnumerateFiles(
                     domainDirectory,
                     "*.cs",
                     config.IgnoredDirectories))
        {
            var content = File.ReadAllText(file);

            foreach (Match match in TypeDeclaration.Matches(content))
            {
                var candidate = match.Groups[1].Value;
                if (sliceName.Contains(candidate, StringComparison.Ordinal))
                    candidates.Add(candidate);
            }
        }

        if (candidates.Count > 0)
        {
            return candidates
                .OrderByDescending(candidate => candidate.Length)
                .ThenBy(candidate => candidate, StringComparer.Ordinal)
                .First();
        }

        throw new CliException(
            $"No se pudo inferir la entidad para '{sliceName}'. Usa -e o --entity.");
    }
}
