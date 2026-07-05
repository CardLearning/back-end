using CardLearning.Application.Exceptions;
using CardLearning.Application.Interfaces;
using CardLearning.Application.Mediatr.Commands;
using CardLearning.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CardLearning.Application.Mediatr.CommandHandlers;

public class CreateCardCommandHandler(IDbContext cardLearningDbContext) : IRequestHandler<CreateCardCommand, int>
{
    public async Task<int> Handle(CreateCardCommand request, CancellationToken cancellationToken)
    {
        var duplicateName = await cardLearningDbContext.Cards.FirstOrDefaultAsync(x => x.Name == request.Name, cancellationToken: cancellationToken);
        if (duplicateName != null)
        {
            throw new CardAlreadyExistsException($"Card {request.Name} already exists.");
        }
        
        var deck = await cardLearningDbContext.Decks.FirstOrDefaultAsync(x => x.Id == request.DeckId, cancellationToken);
        if (deck == null)
        {
            throw new InvalidDeckException($"Invalid deck id = {request.DeckId}.");
        }

        var newCard = new Card
        {
            Name = request.Name,
            Front = request.Front,
            Back = request.Back,
            DeckId = request.DeckId
        };
        cardLearningDbContext.Cards.Add(newCard);
        await cardLearningDbContext.SaveChangesAsync(cancellationToken);
        
        return newCard.Id;
    }
}