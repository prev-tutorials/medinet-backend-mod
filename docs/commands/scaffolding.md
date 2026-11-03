# Scaffolding de slices

El proyecto incluye una herramienta local, `Medinet.Feature.Cli`, que genera slices
verticales para los modulos de la API.

Genera dos tipos de slice:

- `command`: request, command, validator, handler y endpoint.
- `query`: query, response (si no existe uno en el modulo), handler y endpoint.

Cada slice se crea en una carpeta nueva con exactamente tres archivos.

## Instalacion

```console
./scripts/install-feature-cli.sh
dotnet feature --help
```

El instalador:

- empaqueta `Tools/Medinet.Feature.Cli`,
- crea `.config/dotnet-tools.json` si no existe,
- instala o actualiza la herramienta local.

## Uso

```console
dotnet feature command -n <Nombre> -m <Modulo> [opciones]
dotnet feature query   -n <Nombre> -m <Modulo> [opciones]
```

Opciones:

| Opcion | Descripcion |
| --- | --- |
| `-n`, `--name` | Nombre del slice. Obligatorio. |
| `-m`, `--module` | Nombre de la carpeta del modulo dentro de `modulesPath`. Obligatorio. |
| `-e`, `--entity` | Entidad del dominio. Si no se indica, se infiere del nombre del slice. |
| `--route` | Ruta HTTP. Si no se indica, se usa la ruta por defecto segun la operacion. |
| `--root` | Raiz del repositorio. Por defecto se busca hacia arriba. |
| `--dry-run` | Muestra los archivos que se crearian sin escribir nada. |

Ejemplos:

```console
# Command
dotnet feature command -n CreateDoctor -m Doctors

# Query
dotnet feature query -n GetDoctors -m Doctors

# Sin inferir la entidad
dotnet feature command -n CreateDoctor -m Doctors -e Doctor

# Ruta personalizada
dotnet feature query -n GetDoctorById -m Doctors --route "/{id:guid}"

# Simulacion
dotnet feature command -n CreateDoctor -m Doctors --dry-run
```

## Comportamiento

1. Busca la raiz del repositorio usando `feature-cli.json`, `*.slnx` o `*.sln`.
2. Valida que exista `Api/Modules/{Modulo}`.
3. Busca una carpeta `Features` dentro del modulo, ya sea directa o anidada.
   Si no existe o hay mas de una, la herramienta aborta.
4. Crea una carpeta nueva dentro de `Features`. Si el slice ya existe, aborta.
5. Resuelve la entidad, el repositorio `I{Entidad}Repository` y el DTO
   `{Entidad}Response` si existen en el modulo.
6. Genera los tres archivos del slice.

Si no encuentra el repositorio, el handler se genera sin inyeccion. Si no
encuentra el DTO de respuesta para un query, genera un response local en el
mismo archivo del query.

Las rutas por defecto son:

| Tipo | Operacion | Metodo | Ruta |
| --- | --- | --- | --- |
| command | `Create*` | `MapPost` | `/` |
| command | `Update*` | `MapPatch` | `/{id:guid}` |
| command | `Delete*` | `MapDelete` | `/{id:guid}` |
| query | `Get*` | `MapGet` | `/` |
| query | `Get*ById*` | `MapGet` | `/{id:guid}` |

## Registro del endpoint

La herramienta no modifica `{Modulo}Module.cs`. Al terminar imprime la linea que
debes agregar manualmente:

```csharp
group.MapCreateDoctor();
```

## Configuracion

La configuracion vive en `feature-cli.json` en la raiz del repositorio:

```json
{
  "rootNamespace": "Api",
  "modulesPath": "Api/Modules",
  "featuresFolderName": "Features",
  "ignoredDirectories": ["bin", "obj", ".git", ".nupkg"]
}
```
