namespace Severino.WebApi.Features.ApiResource
{
    using FluentValidation;

    internal sealed class CreateApiResourceModelValidator : AbstractValidator<CreateApiResourceModel>
    {
        internal CreateApiResourceModelValidator()
        {
            this.RuleFor(model => model.Name)
                .NotEmpty()
                .WithMessage("Api resource name is required.");

            this.RuleFor(model => model.DisplayName)
                .NotEmpty()
                .WithMessage("Api resource display name is required.");
        }
    }
}
