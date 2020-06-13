namespace Severino.WebApi.Features.User
{
    using System.Collections.Generic;
    using System.Linq;

    using Severino.Domain.User;

    public sealed class UserModel
    {
        private UserModel(User entity)
        {
            this.Email = entity.Id;
            this.Claims = entity.Claims.Select(claim => claim.Value);
        }

        public string Email { get; }

        public IEnumerable<string> Claims { get; }

        internal static UserModel NewUserModel(User entity) => new UserModel(entity);
    }
}
