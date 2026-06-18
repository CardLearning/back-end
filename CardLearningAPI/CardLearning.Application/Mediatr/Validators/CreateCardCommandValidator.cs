using CardLearning.Application.Mediatr.Commands;
using FluentValidation;

namespace CardLearning.Application.Mediatr.Validators;

public class CreateCardCommandValidator : AbstractValidator<CreateCardCommand>
{
    public CreateCardCommandValidator() 
    {
        RuleFor(x => x.Name)
            .NotEmpty();
        
        RuleFor(x => x.DeckId)
            .NotEmpty()
            .GreaterThan(0);
    }
}