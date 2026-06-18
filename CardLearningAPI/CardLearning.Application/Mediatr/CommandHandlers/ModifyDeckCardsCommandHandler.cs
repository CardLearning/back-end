using CardLearning.Application.Exceptions;
using CardLearning.Application.Interfaces;
using CardLearning.Application.Mediatr.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CardLearning.Application.Mediatr.CommandHandlers;

public class ModifyDeckCardsCommandHandler(IDbContext cardLearningDbContext) : IRequestHandler<ModifyDeckCardsCommand>
{
    public async Task Handle(ModifyDeckCardsCommand request, CancellationToken cancellationToken)
    {
        var deck = await cardLearningDbContext.Decks
            .Include(x=> x.Cards)
            .FirstOrDefaultAsync(x => x.Id == request.DeckId, cancellationToken);
        
        if (deck == null)
        {
            throw new InvalidDeckException($"Deck id = {request.DeckId} is not found.");
        }

        if (request.CardIds == null || !request.CardIds.Any())
        {
            deck.Cards.Clear();
            await cardLearningDbContext.SaveChangesAsync(cancellationToken);
        }
        else
        {
            var cards = await cardLearningDbContext.Cards
                .Where(x => request.CardIds.Contains(x.Id))
                .ToListAsync(cancellationToken);
            
            var missingCards = request.CardIds.Except(cards.Select(x => x.Id));
            if (missingCards.Any())
            {
                throw new InvalidCardException($"Cards with ids = {string.Join(", ", missingCards)} are not found.");
            }

            deck.Cards = cards;
            await cardLearningDbContext.SaveChangesAsync(cancellationToken);
        }
       
    }
}

