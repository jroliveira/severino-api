namespace Severino.WebApi.Features.User
{
    using FluentValidation;

    internal sealed class UpdateUserModelValidator : AbstractValidator<UpdateUserModel>
    {
        internal UpdateUserModelValidator()
        {
            this.RuleFor(model => model.Password)
                .NotEmpty()
                .WithMessage("User password is required.");

            this.RuleFor(model => model.ConfirmPassword)
                .Equal(model => model.Password)
                .WithMessage("User confirm password must match user password.");
        }
    }
}
