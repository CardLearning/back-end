using CardLearning.Application.Exceptions;
using CardLearning.Application.Interfaces;
using CardLearning.Application.Mediatr.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CardLearning.Application.Mediatr.CommandHandlers;

public class EditCardCommandHandler(IDbContext cardLearningDbContext) : IRequestHandler<EditCardCommand>
{
    public async Task Handle(EditCardCommand request, CancellationToken cancellationToken)
    {
        var card = await cardLearningDbContext.Cards.FirstOrDefaultAsync(x => x.Id == request.CardId, cancellationToken);
        if (card == null)
        {
            throw new InvalidCardException($"Card with id = {request.CardId} does not exist.");
        }
        
        cardLearningDbContext.Cards.Update(card);
        await cardLearningDbContext.SaveChangesAsync(cancellationToken);
    }
}