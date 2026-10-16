# Comandos

## SDK

```console
# Versión de .NET
dotnet --version

# Ver lista de SDKs disponibles
dotnet --list-sdks

# Install sdk .net 10
sudo apt update
sudo apt install -y dotnet-sdk-10.0

#

# Fijar el proyecto en una versión específica
dotnet new globaljson --sdk-version 10.0.401
```

## Solución y proyectos

```console
# Crear una solución
dotnet new sln -n Medinet

# Crear un proyecto API
dotnet new webapi -n Api

# Agregar un proyecto a la solución
dotnet sln add Api/Api.csproj
```

## Dependencias

```console
# SwaggerUI
dotnet add package Swashbuckle.AspNetCore.SwaggerUI

# EF Core
dotnet add package Microsoft.EntityFrameworkCore --version 10.0.13
dotnet add package Microsoft.EntityFrameworkCore.Design --version 10.0.13
dotnet add package Microsoft.EntityFrameworkCore.SqlServer --version 10.0.13

# FluentValidation
dotnet add package FluentValidation --version 12.2.0
dotnet add package FluentValidation.DependencyInjectionExtensions --version 12.2.0

# MediatR
dotnet add package MediatR --version 14.2.0
```
