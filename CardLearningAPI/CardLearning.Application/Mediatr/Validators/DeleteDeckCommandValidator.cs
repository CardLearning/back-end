using CardLearning.Application.Mediatr.Commands;
using FluentValidation;

namespace CardLearning.Application.Mediatr.Validators;

public class DeleteDeckCommandValidator : AbstractValidator<DeleteDeckCommand>
{
    public DeleteDeckCommandValidator()
    {
        RuleFor(x => x.DeckId)
            .GreaterThan(0);
    }
}