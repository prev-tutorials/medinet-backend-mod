#!/usr/bin/env bash
set -euo pipefail

repo_root="$(cd "$(dirname "${BASH_SOURCE[0]}")/.." && pwd)"
package_dir="$repo_root/.nupkg"
tool_project="$repo_root/Tools/Medinet.Feature.Cli/Medinet.Feature.Cli.csproj"

mkdir -p "$package_dir"
mkdir -p "$repo_root/.config"

if [ ! -f "$repo_root/.config/dotnet-tools.json" ]; then
  dotnet new tool-manifest --output "$repo_root/.config" >/dev/null
fi

cd "$repo_root"

dotnet pack "$tool_project" -c Release -o "$package_dir"

if dotnet tool list --local 2>/dev/null | grep -qi "medinet.feature.cli"; then
  dotnet tool uninstall Medinet.Feature.Cli --local
fi

dotnet tool install Medinet.Feature.Cli \
  --local \
  --add-source "$package_dir" \
  --version 1.0.0

dotnet tool restore

echo
echo "Instalado. Prueba:"
echo "  dotnet feature --help"
