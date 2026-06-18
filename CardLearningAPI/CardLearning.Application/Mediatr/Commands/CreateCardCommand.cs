using MediatR;

namespace CardLearning.Application.Mediatr.Commands;

public class CreateCardCommand : IRequest<int>
{
    public required string Name { get; set; }
    public string? Front { get; set; }
    public string? Back { get; set; }
    public int DeckId { get; set; }
}