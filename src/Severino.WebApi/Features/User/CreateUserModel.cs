namespace Severino.WebApi.Features.User
{
    using System.ComponentModel.DataAnnotations;

    using Severino.Domain.User;
    using Severino.Infrastructure.Monad;

    using static Severino.Domain.Shared.Email;
    using static Severino.Domain.User.User;

    public class CreateUserModel
    {
        public CreateUserModel(
            string email,
            string password,
            string confirmPassword)
        {
            this.Email = email;
            this.Password = password;
            this.ConfirmPassword = confirmPassword;
        }

        [Required]
        public string Email { get; }

        [Required]
        public string Password { get; }

        [Required]
        public string ConfirmPassword { get; }

        public static implicit operator Option<User>(CreateUserModel model) => NewUser(NewEmail(model.Email), model.Password);
    }
}
