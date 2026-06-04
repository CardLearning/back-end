using CardLearning.Application.Interfaces;
using CardLearning.Domain;
using Microsoft.EntityFrameworkCore;

namespace CardLearning.Infrastructure.Database;

public class CardLearningDbContext : DbContext, IDbContext
{
    public CardLearningDbContext(DbContextOptions<CardLearningDbContext> options) : base(options)
    {
        
    }
    public DbSet<Card> Cards => Set<Card>();
    public DbSet<Deck> Decks => Set<Deck>();
    public DbSet<LearningSessionProgress> LearningSessionProgresses => Set<LearningSessionProgress>();
    public DbSet<LearningSession> LearningSessions => Set<LearningSession>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CardLearningDbContext).Assembly);
    }


}