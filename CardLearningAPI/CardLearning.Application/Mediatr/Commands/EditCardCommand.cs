using CardLearning.Application.DTO.Input;
using MediatR;

namespace CardLearning.Application.Mediatr.Commands;

public class EditCardCommand : IRequest
{
    public int CardId { get; set; }
    public required EditCardDTO NewCardData { get; set; }
}