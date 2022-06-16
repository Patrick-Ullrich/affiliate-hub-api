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

### Undoing Migration

From the `/src` folder:
`dotnet-ef database update 0 --project Infrastructure --startup-project WebUI`

### Removing Migration

From the `/src` folder:
`dotnet-ef migrations remove --project Infrastructure --startup-project WebUI`

## Generate Client

Install Java

```
brew tap AdoptOpenJDK/openjdk
brew install --cask adoptopenjdk12
export JAVA_HOME=/Library/Java/JavaVirtualMachines/jdk-12.0.2.jdk/Contents/Home/
```

Install generator

```
npm install -g @openapitools/openapi-generator-cli
```

Run generator

```
openapi-generator-cli generate -g typescript-fetch -i http://localhost:5090/swagger/v1/swagger.json -o affiliate-hub-client --additional-properties=supportsES6=true
```

## Docker

```
# Build
docker build . -t affiliate-hub-api

# Run
docker run --name affiliate-hub-api -p 8081:80 affiliate-hub-api
```

## Hosting on DigitalOcean

Environemtn Variables:

```
ConnectionStrings__DefaultConnection:
Trust Server Certificate=true;SSL Mode=Require;Host=<HOST>;Port=<PORT>;Database=<DATABASE>;Username=<USERNAME>;Password=<PASSWORD>

HTTP Port:
80

```
