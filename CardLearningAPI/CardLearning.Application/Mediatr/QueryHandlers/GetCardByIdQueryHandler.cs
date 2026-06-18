using CardLearning.Application.DTO.Output;
using CardLearning.Application.Exceptions;
using CardLearning.Application.Interfaces;
using CardLearning.Application.Mediatr.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CardLearning.Application.Mediatr.QueryHandlers;

public class GetCardByIdQueryHandler(IDbContext cardLearningDbContext) : IRequestHandler<GetCardByIdQuery, CardDTO>
{
    public async Task<CardDTO> Handle(GetCardByIdQuery request, CancellationToken cancellationToken)
    {
        var card = await cardLearningDbContext.Cards.FirstOrDefaultAsync(x => x.Id == request.CardId, cancellationToken: cancellationToken);
        if (card == null)
        {
            throw new InvalidCardException($"Card with id ={request.CardId} does not exist.");
        }

        return new CardDTO
        {
            Id = card.Id,
            Name = card.Name,
            Front = card.Front,
            Back = card.Back,
            DeckId = card.DeckId,
        };
    }
}