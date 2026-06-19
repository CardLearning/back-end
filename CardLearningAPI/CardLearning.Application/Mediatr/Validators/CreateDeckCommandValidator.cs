using CardLearning.Application.Mediatr.Commands;
using FluentValidation;

namespace CardLearning.Application.Mediatr.Validators;

public class CreateDeckCommandValidator : AbstractValidator<CreateDeckCommand>
{
    public CreateDeckCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty();

    }
}