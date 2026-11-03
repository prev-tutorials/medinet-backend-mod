using Medinet.Feature.Cli.Configuration;

namespace Medinet.Feature.Cli.Generation;

public sealed record LocatedType(string TypeName, string Namespace, string RelativePath);

public static class ModuleTypeLocator
{
    public static LocatedType? Find(
        string moduleDirectory,
        string moduleNamespace,
        string fileName,
        FeatureCliConfig config
    )
    {
        var file = FileSystemScanner
            .EnumerateFiles(moduleDirectory, fileName, config.IgnoredDirectories)
            .FirstOrDefault();

        if (file is null)
            return null;

        var directory = Path.GetDirectoryName(file)
            ?? throw new InvalidOperationException($"No se pudo obtener la carpeta de '{file}'.");

        return new LocatedType(
            Path.GetFileNameWithoutExtension(file),
            NamespaceResolver.ForDirectory(moduleNamespace, moduleDirectory, directory),
            Path.GetRelativePath(moduleDirectory, file)
        );
    }
}
