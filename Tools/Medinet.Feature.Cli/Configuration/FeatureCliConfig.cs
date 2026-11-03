using System.Text.Json;
using Medinet.Feature.Cli.Cli;

namespace Medinet.Feature.Cli.Configuration;

public sealed class FeatureCliConfig
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public string RootNamespace { get; init; } = "Api";
    public string ModulesPath { get; init; } = "Api/Modules";
    public string FeaturesFolderName { get; init; } = "Features";
    public string[] IgnoredDirectories { get; init; } = ["bin", "obj", ".git", ".nupkg"];

    public static FeatureCliConfig Load(string root)
    {
        var path = Path.Combine(root, "feature-cli.json");
        if (!File.Exists(path))
            return new FeatureCliConfig();

        string json;
        try
        {
            json = File.ReadAllText(path);
        }
        catch (Exception ex)
        {
            throw new CliException($"No se pudo leer '{path}': {ex.Message}");
        }

        FeatureCliConfig? config;
        try
        {
            config = JsonSerializer.Deserialize<FeatureCliConfig>(json, JsonOptions);
        }
        catch (JsonException ex)
        {
            throw new CliException($"El archivo '{path}' no es un JSON valido: {ex.Message}");
        }

        config ??= new FeatureCliConfig();

        if (string.IsNullOrWhiteSpace(config.RootNamespace))
            throw new CliException("'rootNamespace' no puede estar vacio en feature-cli.json.");

        if (string.IsNullOrWhiteSpace(config.ModulesPath))
            throw new CliException("'modulesPath' no puede estar vacio en feature-cli.json.");

        if (string.IsNullOrWhiteSpace(config.FeaturesFolderName))
            throw new CliException("'featuresFolderName' no puede estar vacio en feature-cli.json.");

        return new FeatureCliConfig
        {
            RootNamespace = config.RootNamespace.Trim(),
            ModulesPath = config.ModulesPath.Trim(),
            FeaturesFolderName = config.FeaturesFolderName.Trim(),
            IgnoredDirectories = config.IgnoredDirectories is { Length: > 0 }
                ? config.IgnoredDirectories
                : ["bin", "obj", ".git", ".nupkg"]
        };
    }
}
