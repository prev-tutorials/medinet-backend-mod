using Medinet.Feature.Cli.Cli;

namespace Medinet.Feature.Cli.Configuration;

public static class ProjectRoot
{
    public static string Find(string? explicitRoot)
    {
        if (!string.IsNullOrWhiteSpace(explicitRoot))
        {
            var fullPath = Path.GetFullPath(explicitRoot);
            if (!Directory.Exists(fullPath))
                throw new CliException($"No existe la raiz indicada: '{explicitRoot}'.");

            return fullPath;
        }

        var current = new DirectoryInfo(Directory.GetCurrentDirectory());

        while (current is not null)
        {
            if (File.Exists(Path.Combine(current.FullName, "feature-cli.json"))
                || current.EnumerateFiles("*.slnx").Any()
                || current.EnumerateFiles("*.sln").Any())
            {
                return current.FullName;
            }

            current = current.Parent;
        }

        throw new CliException(
            "No se encontro la raiz del proyecto. Indica --root o crea feature-cli.json.");
    }
}
