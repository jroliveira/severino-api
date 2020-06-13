namespace Severino.Domain.User
{
    using System.Collections.Generic;
    using System.Security.Claims;

    using IdentityModel;

    using Severino.Domain.Shared;
    using Severino.Infrastructure.ErrorHandling.Exceptions;
    using Severino.Infrastructure.Monad;

    using static Severino.Infrastructure.Monad.Utils.Util;

    public sealed class User : Entity<Email>
    {
        private User(Email email, string password)
            : base(email)
        {
            this.Email = email;
            this.Password = password;
        }

        public Email Email { get; }

        public string Password { get; }

        public IReadOnlyCollection<Claim> Claims => new[]
        {
            new Claim(JwtClaimTypes.Email, this.Email),
        };

        public static Try<User> NewUser(
            in Option<Email> email,
            in Option<string> password) =>
                email
                && password
                    ? new User(email.Get(), password.Get())
                    : Failure<User>(new InvalidObjectException("Invalid user."));
    }
}
