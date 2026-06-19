using CardLearning.Application.Exceptions;
using CardLearning.Application.Interfaces;
using CardLearning.Application.Mediatr.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CardLearning.Application.Mediatr.CommandHandlers;

public class DeleteCardCommandHandler(IDbContext cardLearningDbContext) : IRequestHandler<DeleteCardCommand, int>
{
    public async Task<int> Handle(DeleteCardCommand request, CancellationToken cancellationToken)
    {
        var card = await cardLearningDbContext.Cards.FirstOrDefaultAsync(x => x.Id == request.CardId, cancellationToken);
        if (card == null)
        {
            throw new InvalidCardException($"Card with id = {request.CardId} does not exist.");
        }
        
        cardLearningDbContext.Cards.Remove(card);
        await cardLearningDbContext.SaveChangesAsync(cancellationToken);
        
        return request.CardId;
    }
}