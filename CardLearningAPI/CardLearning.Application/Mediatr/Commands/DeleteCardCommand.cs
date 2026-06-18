using MediatR;

namespace CardLearning.Application.Mediatr.Commands;

public class DeleteCardCommand : IRequest<int>
{
    public int CardId { get; set; }
}