using CardLearning.Application.Mediatr.Commands;
using FluentValidation;

namespace CardLearning.Application.Mediatr.Validators;

public class EditCardCommandValidator : AbstractValidator<EditCardCommand>
{
    public EditCardCommandValidator()
    {
        RuleFor(x => x.CardId)
            .GreaterThan(0);
        
        RuleFor(x=> x.NewCardData)
            .NotNull();

        When(x => x.NewCardData != null, () =>
        {
            RuleFor(x => x.NewCardData.Name)
                .NotEmpty();

            RuleFor(x => x.NewCardData.Back)
                .MaximumLength(255)
                .When(x => x.NewCardData.Back != null);
            
            RuleFor(x => x.NewCardData.Front)
                .MaximumLength(255)
                .When(x => x.NewCardData.Front != null);
        });

    }
}