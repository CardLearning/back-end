using CardLearning.Domain;
using Microsoft.EntityFrameworkCore;
namespace CardLearning.Application.Interfaces;

public interface IDbContext
{
    DbSet<Card> Cards { get;}
    DbSet<Deck> Decks { get;}
    DbSet<LearningSessionProgress> LearningSessionProgresses {get;}
    DbSet<LearningSession> LearningSessions {get;} 
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}