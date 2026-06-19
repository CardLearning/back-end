using MediatR;

namespace CardLearning.Application.Mediatr.Commands;

public class DeleteDeckCommand : IRequest<int>
{
    public int DeckId { get; set; }
}