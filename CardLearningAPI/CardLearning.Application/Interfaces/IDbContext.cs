using Microsoft.EntityFrameworkCore.;
namespace CardLearning.Application.Interfaces;


public interface IDbContext
{
    Dbset<T> GetDbset<T>() where T : class;
}