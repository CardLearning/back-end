using CardLearning.Application.Mediatr.Queries;
using FluentValidation;

namespace CardLearning.Application.Mediatr.Validators;

public class GetCardByIdQueryValidator : AbstractValidator<GetCardByIdQuery>
{
    public GetCardByIdQueryValidator()
    {
        RuleFor(x => x.CardId)
            .GreaterThan(0);
    }
}