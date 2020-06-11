namespace Severino.WebApi.Features.IdentityResource
{
    using FluentValidation;

    internal sealed class CreateIdentityResourceModelValidator : AbstractValidator<CreateIdentityResourceModel>
    {
        internal CreateIdentityResourceModelValidator()
        {
            this.RuleFor(model => model.Name)
                .NotEmpty()
                .WithMessage("Identity resource name is required.");

            this.RuleFor(model => model.DisplayName)
                .NotEmpty()
                .WithMessage("Identity resource display name is required.");
        }
    }
}
