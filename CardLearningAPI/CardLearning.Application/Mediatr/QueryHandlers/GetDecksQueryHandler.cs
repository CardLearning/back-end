using CardLearning.Application.DTO.Output;
using CardLearning.Application.Interfaces;
using CardLearning.Application.Mediatr.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CardLearning.Application.Mediatr.QueryHandlers;

public class GetDecksQueryHandler(IDbContext cardLearningDbContext) : IRequestHandler<GetDecksQuery, IEnumerable<DeckDTO>>
{
    
    public async Task<IEnumerable<DeckDTO>> Handle(GetDecksQuery request, CancellationToken cancellationToken)
    {
        var decks = await cardLearningDbContext.Decks.ToListAsync(cancellationToken: cancellationToken);

        var result = decks.Select(x => new DeckDTO
        {
            Id = x.Id,
            Name = x.Name,
            Description = x.Description
        });
        
        return result;
    }
}