using MediatR;

namespace CardLearning.Application.Mediatr.Commands;

public class ModifyDeckCardsCommand : IRequest
{
    public int DeckId { get; set; }
    public IEnumerable<int>? CardIds { get; set; }
}