using BuildingBlocks.CQRS;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace BuildingBlocks.Behaviors;

public class ValidatorBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> Validators)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : ICommand<TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        ValidationContext<TRequest>? context = new(request);
        ValidationResult[]? validationResults =
            await Task.WhenAll(Validators.Select(x => x.ValidateAsync(context, cancellationToken)));

        List<ValidationFailure>? failures =
            validationResults.Where(x => x.Errors.Any()).SelectMany(r => r.Errors).ToList();
        if (failures.Any()) throw new ValidationException(failures);

        return await next();
    }
}