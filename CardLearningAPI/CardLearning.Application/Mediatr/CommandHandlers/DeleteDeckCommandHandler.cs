using CardLearning.Application.Exceptions;
using CardLearning.Application.Interfaces;
using CardLearning.Application.Mediatr.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CardLearning.Application.Mediatr.CommandHandlers;

public class DeleteDeckCommandHandler(IDbContext cardLearningDbContext) : IRequestHandler<DeleteDeckCommand, int>
{
    public async Task<int> Handle(DeleteDeckCommand request, CancellationToken cancellationToken)
    {
        var deck = await cardLearningDbContext.Decks.FirstOrDefaultAsync(x => x.Id == request.DeckId, cancellationToken);
        if (deck == null)
        {
            throw new InvalidDeckException($"Deck with id = {request.DeckId} does not exist.");
        }
        
        cardLearningDbContext.Decks.Remove(deck);
        await cardLearningDbContext.SaveChangesAsync(cancellationToken);
        
        return request.DeckId;
    }
}