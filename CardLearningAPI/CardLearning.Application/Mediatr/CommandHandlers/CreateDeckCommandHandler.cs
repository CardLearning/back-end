using CardLearning.Application.Interfaces;
using CardLearning.Application.Mediatr.Commands;
using CardLearning.Domain;
using MediatR;

namespace CardLearning.Application.Mediatr.CommandHandlers;

public class CreateDeckCommandHandler(IDbContext cardLearningDbContext) : IRequestHandler<CreateDeckCommand, int>
{
    public async Task<int> Handle(CreateDeckCommand request, CancellationToken cancellationToken)
    {
        var newDeck = new Deck
        {
            Name = request.Name,
            Description = request.Description
        };
        
        cardLearningDbContext.Decks.Add(newDeck);
        await cardLearningDbContext.SaveChangesAsync(cancellationToken);
        
        return newDeck.Id;
    }
}