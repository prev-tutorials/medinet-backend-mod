using System.Reflection;

namespace Medinet.Feature.Cli.Generation;

public static class TemplateRenderer
{
    private static readonly Assembly Assembly = typeof(TemplateRenderer).Assembly;

    public static string Render(
        string kindFolder,
        string fileName,
        IReadOnlyDictionary<string, string> tokens
    )
    {
        var suffix = $".Templates.{kindFolder}.{fileName}";
        var resourceName = Assembly
            .GetManifestResourceNames()
            .SingleOrDefault(name => name.EndsWith(suffix, StringComparison.OrdinalIgnoreCase));

        if (resourceName is null)
        {
            var available = string.Join(", ", Assembly.GetManifestResourceNames());
            throw new InvalidOperationException(
                $"No se encontro el template embebido '{suffix}'. Recursos: {available}");
        }

        using var stream = Assembly.GetManifestResourceStream(resourceName)
            ?? throw new InvalidOperationException($"No se pudo abrir el template '{resourceName}'.");
        using var reader = new StreamReader(stream);
        var content = reader.ReadToEnd();

        foreach (var (key, value) in tokens)
            content = content.Replace($"{{{{{key}}}}}", value, StringComparison.Ordinal);

        if (content.Contains("{{", StringComparison.Ordinal))
            throw new InvalidOperationException($"Quedaron tokens sin reemplazar en '{fileName}'.");

        return content;
    }
}
