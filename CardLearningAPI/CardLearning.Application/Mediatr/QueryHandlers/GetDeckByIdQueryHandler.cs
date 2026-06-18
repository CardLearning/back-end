using CardLearning.Application.DTO;
using CardLearning.Application.DTO.Output;
using CardLearning.Application.Exceptions;
using CardLearning.Application.Interfaces;
using CardLearning.Application.Mediatr.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CardLearning.Application.Mediatr.QueryHandlers;

public class GetDeckByIdQueryHandler(IDbContext cardLearningDbContext) : IRequestHandler<GetDeckByIdQuery, DeckDTO>
{
    public async Task<DeckDTO> Handle(GetDeckByIdQuery request, CancellationToken cancellationToken)
    {
        var deck = await cardLearningDbContext.Decks.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (deck == null)
        {
            throw new InvalidDeckException($"Deck with id = {request.Id} does not exist.");
        }
        
        var result = new DeckDTO()
        {
            Id = deck.Id,
            Name = deck.Name,
            Description = deck.Description,
        };
        return result;

    }
}