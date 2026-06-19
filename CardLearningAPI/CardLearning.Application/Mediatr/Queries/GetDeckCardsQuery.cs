using CardLearning.Application.DTO.Output;
using MediatR;

namespace CardLearning.Application.Mediatr.Queries;

public class GetDeckCardsQuery: IRequest<IEnumerable<CardDTO>>
{
    public int DeckId { get; set; }
}