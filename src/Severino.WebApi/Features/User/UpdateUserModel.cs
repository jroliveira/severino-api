namespace Severino.WebApi.Features.User
{
    using System.ComponentModel.DataAnnotations;

    using Severino.Domain.Shared;
    using Severino.Domain.User;
    using Severino.Infrastructure.Monad;

    using static Severino.Domain.User.User;

    public class UpdateUserModel
    {
        public UpdateUserModel(string password, string confirmPassword)
        {
            this.Password = password;
            this.ConfirmPassword = confirmPassword;
        }

        [Required]
        public string Password { get; }

        [Required]
        public string ConfirmPassword { get; }

        public Option<User> ToEntity(in Option<Email> email) => NewUser(email, this.Password);
    }
}
