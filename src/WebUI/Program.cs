using AffiliateHub.Application;
using AffiliateHub.Application.Common.Interfaces;
using AffiliateHub.Infrastructure;
using AffiliateHub.Infrastructure.Persistence;
using AffiliateHub.WebUI.Filters;
using AffiliateHub.WebUI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

var OriginPolicy = "Application";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: OriginPolicy,
                      policy =>
                      {
                          policy.AllowAnyHeader()
                            .AllowAnyMethod()
                            .AllowAnyOrigin();
                      });
});

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddSingleton<ICurrentUserService, CurrentUserService>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddHealthChecks().AddDbContextCheck<ApplicationDbContext>();

// Disable Model State Validation, instead we use FluentValidation
builder.Services.Configure<ApiBehaviorOptions>(options =>
            options.SuppressModelStateInvalidFilter = true);


builder.Services
    .AddControllers(options =>
        {
            options.Filters.Add<ApiExceptionFilterAttribute>();
        })
    .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.DictionaryKeyPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
            options.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
        });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(o =>
{
    o.DocumentFilter<PathLowercaseDocumentFilter>();
    o.SwaggerDoc("v1", new OpenApiInfo { Title = "AffiliateHub API", Version = "v1" });
    o.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    o.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,
            },
            new List<string>()
        }
    });
});

var app = builder.Build();

app.UseCors(OriginPolicy);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHealthChecks("/health");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
