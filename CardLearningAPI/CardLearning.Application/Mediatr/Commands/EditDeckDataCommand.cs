using CardLearning.Application.DTO.Input;
using MediatR;

namespace CardLearning.Application.Mediatr.Commands;

public class EditDeckDataCommand : IRequest
{
    public int DeckId { get; set; }
    public required EditDeckDTO NewDeckData { get; set; }
}