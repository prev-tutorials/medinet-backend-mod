using Medinet.Feature.Cli.Generation;

namespace Medinet.Feature.Cli.Cli;

public sealed class FeatureCliOptions
{
    public FeatureKind Kind { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Module { get; init; } = string.Empty;
    public string? Entity { get; init; }
    public string? Route { get; init; }
    public string? Root { get; init; }
    public bool DryRun { get; init; }
    public bool ShowHelp { get; init; }
}
