## Database

### Install dotnet-ef tools

For m1 Macbook:
`dotnet tool install -g -a arm64 dotnet-ef`

Non m1:
`dotnet tool update --global dotnet-ef`

### Add Migration

From the `/src` folder:
`dotnet-ef migrations add "MIGRATION_NAME" --project Infrastructure --startup-project WebUI --output-dir Persistence/Migrations`

### Manually apply migration to Database

From the `/src` folder:
`dotnet-ef database update --startup-project WebUI`