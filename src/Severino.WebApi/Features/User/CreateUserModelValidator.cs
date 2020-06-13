namespace Severino.WebApi.Features.User
{
    using FluentValidation;

    internal sealed class CreateUserModelValidator : AbstractValidator<CreateUserModel>
    {
        internal CreateUserModelValidator()
        {
            this.RuleFor(model => model.Email)
                .NotEmpty()
                .WithMessage("User e-mail is required.");

            this.RuleFor(model => model.Password)
                .NotEmpty()
                .WithMessage("User password is required.");

            this.RuleFor(model => model.ConfirmPassword)
                .Equal(model => model.Password)
                .WithMessage("User confirm password must match user password.");
        }
    }
}
