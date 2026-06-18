using CardLearning.Application.DTO.Output;
using MediatR;

namespace CardLearning.Application.Mediatr.Queries;

public class GetDecksQuery : IRequest<IEnumerable<DeckDTO>>
{
    
}