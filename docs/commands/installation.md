# Instalaciones

## SDK .Net 10

```console
# Versión de .NET
dotnet --version

# Ver lista de SDKs disponibles
dotnet --list-sdks

# Install sdk .net 10
sudo apt update
sudo apt install -y dotnet-sdk-10.0

# Agregar al path (~/.zshrc)
export DOTNET_ROOT="$HOME/.dotnet"
export PATH="$PATH:$DOTNET_ROOT:$DOTNET_ROOT/tools"

# Recargar la terminal
source ~/.bashrc
o
source ~/.zshrc
```

## Instalación de EF Core

```
# Instalar dotnet-ef
dotnet tool install --global dotnet-ef

# Agregar al path
export PATH="$PATH:$HOME/.dotnet/tools" 

# Verificar versión
dotnet ef --version
```