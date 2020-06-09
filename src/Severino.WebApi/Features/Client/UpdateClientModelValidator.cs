namespace Severino.WebApi.Features.Client
{
    using FluentValidation;

    internal sealed class UpdateClientModelValidator : AbstractValidator<UpdateClientModel>
    {
        internal UpdateClientModelValidator() => this.RuleFor(model => model.Name)
            .NotEmpty()
            .WithMessage("Client name is required.");
    }
}
