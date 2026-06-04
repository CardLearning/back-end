using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace CardLearning.Infrastructure.Database;

public class CardLearningDbContextFactory : IDesignTimeDbContextFactory<CardLearningDbContext>
{
    public CardLearningDbContext CreateDbContext(string[] args)
    {
        var currentDirectory = Directory.GetCurrentDirectory();
        var candidateBasePaths = new[]
        {
            currentDirectory,
            Path.Combine(currentDirectory, "CardLearning.API"),
            Path.Combine(currentDirectory, "..", "CardLearning.API"),
            Path.Combine(currentDirectory, "..", "..", "CardLearning.API"),
            Path.Combine(currentDirectory, "..", "..", "..", "CardLearning.API")
        }
        .Select(Path.GetFullPath)
        .Distinct()
        .ToArray();

        var basePath = candidateBasePaths.FirstOrDefault(ContainsAppSettingsFiles);

        if (basePath is null)
        {
            throw new DirectoryNotFoundException(
                $"Unable to locate the CardLearning.API configuration directory. " +
                $"Current directory: '{currentDirectory}'. " +
                $"Checked paths: {string.Join(", ", candidateBasePaths.Select(path => $"'{path}'"))}.");
        }

        var configuration = new ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddJsonFile("appsettings.json", optional: true)
            .AddJsonFile("appsettings.Development.json", optional: true)
            .AddEnvironmentVariables()
            .Build();

        var connectionString = configuration.GetConnectionString("CardLearning");

        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException(
                $"Connection string 'CardLearning' was not found. " +
                $"Base path: '{basePath}'. " +
                $"Current directory: '{currentDirectory}'.");
        }

        var optionBuilder = new DbContextOptionsBuilder<CardLearningDbContext>();
        optionBuilder.UseSqlServer(connectionString);

        return new CardLearningDbContext(optionBuilder.Options);

        static bool ContainsAppSettingsFiles(string path) =>
            File.Exists(Path.Combine(path, "appsettings.json")) ||
            File.Exists(Path.Combine(path, "appsettings.Development.json"));
    }
}
