namespace Severino.Domain.User.Data.Mongo.Validators
{
    using System.Linq;
    using System.Threading.Tasks;

    using IdentityServer4.Validation;

    using Severino.Domain.User.Data.Mongo.Documents;
    using Severino.Infrastructure.Data.Mongo;

    using static IdentityServer4.Models.TokenRequestErrors;

    public sealed class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly MongoConnection connection;

        public ResourceOwnerPasswordValidator(MongoConnection connection) => this.connection = connection;

        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            var document = await this.connection
                .SingleOrDefault<UserDocument>(item => item.Email == context.UserName);

            if (!document)
            {
                context.Result = new GrantValidationResult(InvalidGrant, "Invalid username or password");
                return;
            }

            var user = document
                .Select(item => item.ToEntity())
                .Get();

            if (!user.ValidatePassword(context.Password))
            {
                context.Result = new GrantValidationResult(InvalidGrant, "Invalid username or password");
                return;
            }

            context.Result = new GrantValidationResult(
                subject: user.Email,
                authenticationMethod: "custom",
                claims: user.Claims);
        }
    }
}
