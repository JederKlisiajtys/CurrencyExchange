using CurrencyExchange.Services.Interfaces;
using FluentValidation;

namespace CurrencyExchange.Services.Validators
{
    public class QueryValidator : AbstractValidator<Query>
    {
        public QueryValidator(INBPClient client)
        {
            RuleFor(x => x.Amount)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("You must provide amount")
                .Must(x => x > 0)
                .WithMessage("Amount must be greater than 0");

            RuleFor(x => x.SourceCurrency)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("You must provide source currency")
                .MustAsync(async (SourceCurrency, cancellation) => SourceCurrency == "PLN" || await client.CheckIfCurrencyExists(SourceCurrency))
                .WithMessage("Provided source currency code does not exist")
                .Must((query, sourceCurrency) => sourceCurrency != query.TargetCurrency)
                .WithMessage("Source currency must be different than target currency");

            RuleFor(x => x.TargetCurrency)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("You must provide target currency")
                .MustAsync(async (TargetCurrency, cancellation) => TargetCurrency == "PLN" || await client.CheckIfCurrencyExists(TargetCurrency))
                .WithMessage("Provided target currency code does not exist");
        }
    }
}
