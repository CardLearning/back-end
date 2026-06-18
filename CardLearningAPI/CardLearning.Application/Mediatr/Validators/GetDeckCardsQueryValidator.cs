using CardLearning.Application.Mediatr.Queries;
using FluentValidation;

namespace CardLearning.Application.Mediatr.Validators;

public class GetDeckCardsQueryValidator : AbstractValidator<GetDeckCardsQuery>
{
    public GetDeckCardsQueryValidator()
    {
        RuleFor(x => x.DeckId)
            .GreaterThan(0);
    }
}