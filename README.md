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
