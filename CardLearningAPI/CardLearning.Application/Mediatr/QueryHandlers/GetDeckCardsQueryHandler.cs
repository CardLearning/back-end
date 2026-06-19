using CardLearning.Application.DTO.Output;
using CardLearning.Application.Exceptions;
using CardLearning.Application.Interfaces;
using CardLearning.Application.Mediatr.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CardLearning.Application.Mediatr.QueryHandlers;

public class GetDeckCardsQueryHandler(IDbContext cardLearningDbContext) : IRequestHandler<GetDeckCardsQuery, IEnumerable<CardDTO>>
{
    public async Task<IEnumerable<CardDTO>> Handle(GetDeckCardsQuery request, CancellationToken cancellationToken)
    {
        var deck = await cardLearningDbContext
            .Decks
            .Include(x => x.Cards)
            .FirstOrDefaultAsync(x => x.Id == request.DeckId, cancellationToken);

        if (deck == null)
        {
            throw new InvalidDeckException($"Deck with id = {request.DeckId} does not exist.");
        }

        return deck.Cards.Select(x => new CardDTO
            {
                Id = x.Id,
                Name = x.Name,
                Front = x.Front,
            }
        ).ToList();
    }
}