using CardLearning.Application.Mediatr.Commands;
using FluentValidation;

namespace CardLearning.Application.Mediatr.Validators;

public class ModifyDeckCardsCommandValidator : AbstractValidator<ModifyDeckCardsCommand>
{
    public ModifyDeckCardsCommandValidator()
    {
        RuleFor(x => x.DeckId)
            .GreaterThan(0);

        RuleFor(x => x.CardIds)
            .ForEach(x => x.GreaterThan(0))
            .When(x => x.CardIds != null);

    }
}