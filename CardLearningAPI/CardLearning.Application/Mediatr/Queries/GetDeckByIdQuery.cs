using CardLearning.Application.DTO;
using CardLearning.Application.DTO.Output;
using MediatR;

namespace CardLearning.Application.Mediatr.Queries;

public class GetDeckByIdQuery : IRequest<DeckDTO>
{
    public int Id { get; set; }
}