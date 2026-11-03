namespace Medinet.Feature.Cli.Generation;

public static class FileSystemScanner
{
    public static IEnumerable<string> EnumerateDirectories(
        string root,
        IReadOnlyCollection<string> ignoredDirectories,
        Func<string, bool>? descendInto = null
    )
    {
        foreach (var directory in SafeEnumerateDirectories(root))
        {
            if (IsIgnored(directory, ignoredDirectories))
                continue;

            yield return directory;

            if (descendInto is not null && !descendInto(directory))
                continue;

            foreach (var child in EnumerateDirectories(directory, ignoredDirectories, descendInto))
                yield return child;
        }
    }

    public static IEnumerable<string> EnumerateFiles(
        string root,
        string searchPattern,
        IReadOnlyCollection<string> ignoredDirectories
    )
    {
        foreach (var file in SafeEnumerateFiles(root, searchPattern))
            yield return file;

        foreach (var directory in SafeEnumerateDirectories(root))
        {
            if (IsIgnored(directory, ignoredDirectories))
                continue;

            foreach (var file in EnumerateFiles(directory, searchPattern, ignoredDirectories))
                yield return file;
        }
    }

    private static IEnumerable<string> SafeEnumerateDirectories(string directory)
    {
        string[] children;
        try
        {
            children = Directory.GetDirectories(directory);
        }
        catch (Exception ex) when (ex is UnauthorizedAccessException or DirectoryNotFoundException)
        {
            yield break;
        }

        foreach (var child in children.OrderBy(x => x, StringComparer.Ordinal))
        {
            if (IsReparsePoint(child))
                continue;

            yield return child;
        }
    }

    private static IEnumerable<string> SafeEnumerateFiles(string directory, string searchPattern)
    {
        string[] files;
        try
        {
            files = Directory.GetFiles(directory, searchPattern);
        }
        catch (Exception ex) when (ex is UnauthorizedAccessException or DirectoryNotFoundException)
        {
            yield break;
        }

        foreach (var file in files.OrderBy(x => x, StringComparer.Ordinal))
            yield return file;
    }

    private static bool IsIgnored(string directory, IReadOnlyCollection<string> ignoredDirectories)
        => ignoredDirectories.Contains(Path.GetFileName(directory), StringComparer.OrdinalIgnoreCase);

    private static bool IsReparsePoint(string directory)
    {
        try
        {
            return (File.GetAttributes(directory) & FileAttributes.ReparsePoint) != 0;
        }
        catch
        {
            return true;
        }
    }
}
