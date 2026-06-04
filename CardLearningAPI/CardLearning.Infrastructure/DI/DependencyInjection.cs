using CardLearning.Application.Interfaces;
using CardLearning.Infrastructure.Database;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace CardLearning.Infrastructure.DI;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure
    (
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        var connectionString = configuration.GetConnectionString("CardLearning");
        services.AddDbContext<CardLearningDbContext>(options => options.UseSqlServer(connectionString), ServiceLifetime.Scoped);
        services.AddScoped<IDbContext>(provider =>
            provider.GetRequiredService<CardLearningDbContext>());
        
        return services;
    }
}