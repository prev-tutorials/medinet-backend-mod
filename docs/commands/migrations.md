
## Migraciones


```
# Crear las migraciones
dotnet ef migrations add InitialMigration \
  -p Api \                     
  -o Modules/Shared/Infrastructure/Persistence/Migrations

# Actualizar db con migraciones
dotnet ef database update -p Api
```