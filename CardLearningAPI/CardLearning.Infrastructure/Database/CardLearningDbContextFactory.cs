using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace CardLearning.Infrastructure.Database;

public class CardLearningDbContextFactory : IDesignTimeDbContextFactory<CardLearningDbContext>
{
    public CardLearningDbContext CreateDbContext(string[] args)
    {
        var basePath = Path.Combine(Directory.GetCurrentDirectory(), "../CardLearning.API");
        
        var configuration = new ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddJsonFile("appsettings.json", optional: true)
            .AddJsonFile("appsettings.Development.json", optional: true)
            .Build();
        
        var connectionString = configuration.GetConnectionString("CardLearning");
        var optionBuilder = new DbContextOptionsBuilder<CardLearningDbContext>();
        optionBuilder.UseSqlServer(connectionString);
        
        return new CardLearningDbContext(optionBuilder.Options);
            
    }
}