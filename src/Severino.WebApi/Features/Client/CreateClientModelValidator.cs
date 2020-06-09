namespace Severino.WebApi.Features.Client
{
    using FluentValidation;

    internal sealed class CreateClientModelValidator : AbstractValidator<CreateClientModel>
    {
        internal CreateClientModelValidator()
        {
            this.RuleFor(model => model.Id)
                .NotEmpty()
                .WithMessage("Client id is required.");

            this.RuleFor(model => model.Name)
                .NotEmpty()
                .WithMessage("Client name is required.");
        }
    }
}
