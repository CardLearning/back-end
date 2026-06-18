using CardLearning.Application.Mediatr.Commands;
using FluentValidation;

namespace CardLearning.Application.Mediatr.Validators;

public class EditDeckDataCommandValidator : AbstractValidator<EditDeckDataCommand>
{
    public EditDeckDataCommandValidator()
    {
        RuleFor(x => x.DeckId)
            .GreaterThan(0);
        
        RuleFor(x => x.NewDeckData)
            .NotNull();

        When(x => x.NewDeckData != null, () =>
        {
            RuleFor(x => x.NewDeckData.Name)
                .NotEmpty();
            
            RuleFor(x => x.NewDeckData.Description)
                .MaximumLength(255)
                .When(x => x.NewDeckData.Description != null);
        });

    }
}