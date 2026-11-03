using Medinet.Feature.Cli.Cli;
using Medinet.Feature.Cli.Configuration;
using Medinet.Feature.Cli.Generation;

try
{
    var options = ArgumentParser.Parse(args);

    if (options.ShowHelp)
    {
        Console.WriteLine(ArgumentParser.HelpText);
        return 0;
    }

    var root = ProjectRoot.Find(options.Root);
    var config = FeatureCliConfig.Load(root);
    var generator = new SliceGenerator(root, config);

    generator.Generate(options);
    return 0;
}
catch (CliException ex)
{
    Console.Error.WriteLine($"error: {ex.Message}");
    return 1;
}
catch (Exception ex)
{
    Console.Error.WriteLine($"error inesperado: {ex.Message}");
    return 2;
}
