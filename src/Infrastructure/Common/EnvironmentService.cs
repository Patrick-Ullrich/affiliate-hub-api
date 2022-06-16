using AffiliateHub.Application.Common.Interfaces;
namespace AffiliateHub.Infrastructure.Common;

public class EnvironmentService : IEnvironment
{
    public bool IsDevelopment()
    {
        return Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") != null && Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")!.Equals("Development");
    }
}

