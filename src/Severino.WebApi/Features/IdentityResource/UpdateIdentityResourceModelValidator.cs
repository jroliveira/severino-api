namespace Severino.WebApi.Features.IdentityResource
{
    using FluentValidation;

    internal sealed class UpdateIdentityResourceModelValidator : AbstractValidator<UpdateIdentityResourceModel>
    {
        internal UpdateIdentityResourceModelValidator() => this.RuleFor(model => model.DisplayName)
            .NotEmpty()
            .WithMessage("Identity resource display name is required.");
    }
}
