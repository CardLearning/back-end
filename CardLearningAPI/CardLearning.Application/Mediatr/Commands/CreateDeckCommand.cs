using MediatR;

namespace CardLearning.Application.Mediatr.Commands;

public class CreateDeckCommand : IRequest<int>
{
    public required string Name { get; set; }
    public string? Description { get; set; }
}