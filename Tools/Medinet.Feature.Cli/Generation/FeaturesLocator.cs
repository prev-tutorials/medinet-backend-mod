using Medinet.Feature.Cli.Cli;
using Medinet.Feature.Cli.Configuration;

namespace Medinet.Feature.Cli.Generation;

public static class FeaturesLocator
{
    public static string Find(string moduleDirectory, FeatureCliConfig config)
    {
        var matches = FileSystemScanner
            .EnumerateDirectories(
                moduleDirectory,
                config.IgnoredDirectories,
                directory => !string.Equals(
                    Path.GetFileName(directory),
                    config.FeaturesFolderName,
                    StringComparison.OrdinalIgnoreCase))
            .Where(directory => string.Equals(
                Path.GetFileName(directory),
                config.FeaturesFolderName,
                StringComparison.OrdinalIgnoreCase))
            .ToArray();

        return matches.Length switch
        {
            0 => throw new CliException(
                $"El modulo '{moduleDirectory}' no contiene una carpeta '{config.FeaturesFolderName}'."),
            1 => matches[0],
            _ => throw new CliException(
                $"Se encontraron varias carpetas '{config.FeaturesFolderName}' en el modulo:{Environment.NewLine}"
                + string.Join(Environment.NewLine, matches.Select(path => $"  - {path}")))
        };
    }
}
