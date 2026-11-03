using Medinet.Feature.Cli.Generation;

namespace Medinet.Feature.Cli.Cli;

public static class ArgumentParser
{
    public const string HelpText = """
        Uso:
          dotnet feature command -n <Nombre> -m <Modulo> [opciones]
          dotnet feature query   -n <Nombre> -m <Modulo> [opciones]

        Opciones:
          -n, --name <Nombre>       Nombre del slice, por ejemplo CreateDoctor.
          -m, --module <Modulo>     Carpeta del modulo dentro de modulesPath, por ejemplo Doctors.
          -e, --entity <Entidad>    Entidad opcional. Si no se indica, se infiere del dominio.
              --route <Ruta>        Ruta HTTP opcional. Ejemplo: /{id:guid}
              --root <Ruta>         Raiz del repositorio. Por defecto se busca hacia arriba.
              --dry-run             Muestra los archivos sin escribir nada.
          -h, --help                Muestra esta ayuda.

        Ejemplos:
          dotnet feature command -n CreateDoctor -m Doctors
          dotnet feature query   -n GetDoctors -m Doctors
        """;

    public static FeatureCliOptions Parse(string[] args)
    {
        if (args.Length == 0 || args.Any(IsHelp))
            return new FeatureCliOptions { ShowHelp = true };

        FeatureKind? kind = null;
        string? name = null;
        string? module = null;
        string? entity = null;
        string? route = null;
        string? root = null;
        var dryRun = false;

        for (var i = 0; i < args.Length; i++)
        {
            var arg = args[i];

            if (arg is "command" or "query")
            {
                if (kind is not null)
                    throw new CliException("Solo se permite un tipo: command o query.");

                kind = arg == "command" ? FeatureKind.Command : FeatureKind.Query;
                continue;
            }

            switch (arg)
            {
                case "-n":
                case "--name":
                    name = ReadValue(args, ref i, arg);
                    break;

                case "-m":
                case "--module":
                    module = ReadValue(args, ref i, arg);
                    break;

                case "-e":
                case "--entity":
                    entity = ReadValue(args, ref i, arg);
                    break;

                case "--route":
                    route = ReadValue(args, ref i, arg);
                    break;

                case "--root":
                    root = ReadValue(args, ref i, arg);
                    break;

                case "--dry-run":
                    dryRun = true;
                    break;

                default:
                    throw new CliException($"Argumento no reconocido: '{arg}'.");
            }
        }

        if (kind is null)
            throw new CliException("Indica el tipo de slice: command o query.");

        if (string.IsNullOrWhiteSpace(name))
            throw new CliException("El nombre es obligatorio. Usa -n o --name.");

        if (string.IsNullOrWhiteSpace(module))
            throw new CliException("El modulo es obligatorio. Usa -m o --module.");

        name = name.Trim();
        module = module.Trim();
        entity = string.IsNullOrWhiteSpace(entity) ? null : entity.Trim();
        route = string.IsNullOrWhiteSpace(route) ? null : route.Trim();

        if (!IsValidIdentifier(name))
            throw new CliException($"'{name}' no es un identificador C# valido.");

        if (!IsValidIdentifier(module))
            throw new CliException($"'{module}' no es un nombre de modulo valido.");

        if (entity is not null && !IsValidIdentifier(entity))
            throw new CliException($"'{entity}' no es un identificador C# valido.");

        if (route is not null && !route.StartsWith('/'))
            route = "/" + route;

        return new FeatureCliOptions
        {
            Kind = kind.Value,
            Name = name,
            Module = module,
            Entity = entity,
            Route = route,
            Root = root,
            DryRun = dryRun
        };
    }

    private static string ReadValue(string[] args, ref int index, string option)
    {
        if (index + 1 >= args.Length || args[index + 1].StartsWith('-'))
            throw new CliException($"Falta el valor para la opcion '{option}'.");

        index++;
        return args[index];
    }

    private static bool IsHelp(string arg)
        => arg is "-h" or "--help" or "help";

    private static bool IsValidIdentifier(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return false;

        if (!char.IsLetter(value[0]) && value[0] != '_')
            return false;

        return value.Skip(1).All(c => char.IsLetterOrDigit(c) || c == '_');
    }
}
