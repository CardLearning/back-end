using CardLearning.Application.Mediatr.Queries;
using FluentValidation;

namespace CardLearning.Application.Mediatr.Validators;

public class GetDeckByIdQueryValidator : AbstractValidator<GetDeckByIdQuery>
{
    public GetDeckByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0);
    }
}