## Migrations commands

- Create migration
dotnet ef migrations add InitialCreate --project .\src\supv.Data\supv.Data.csproj --startup-project .\src\supv.API\supv.API.csproj

- Apply migrations to database
dotnet ef database update --project .\src\supv.Data\supv.Data.csproj --startup-project .\src\supv.API\supv.API.csproj