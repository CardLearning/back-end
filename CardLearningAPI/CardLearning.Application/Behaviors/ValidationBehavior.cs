using FluentValidation;
using MediatR;

namespace CardLearning.Application.Behaviors;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : class
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;
    
    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (_validators.Any())
        {
            var context = new ValidationContext<TRequest>(request);

            var errors = _validators.Select(x => x.Validate(context))
                .Where(x => x.Errors.Count > 0)
                .SelectMany(x => x.Errors)
                .ToList();

            if (errors.Any())
            {
                throw new ValidationException(errors);
            }
        }

        return await next(cancellationToken);
    }
}