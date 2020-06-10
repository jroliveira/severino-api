namespace Severino.WebApi.Features.ApiResource
{
    using FluentValidation;

    internal sealed class UpdateApiResourceModelValidator : AbstractValidator<UpdateApiResourceModel>
    {
        internal UpdateApiResourceModelValidator() => this.RuleFor(model => model.DisplayName)
            .NotEmpty()
            .WithMessage("Api resource display name is required.");
    }
}
