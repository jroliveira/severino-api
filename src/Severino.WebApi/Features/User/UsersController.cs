namespace Severino.WebApi.Features.User
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using Severino.Domain.User;
    using Severino.Domain.User.Commands;
    using Severino.Domain.User.Queries;
    using Severino.Infrastructure.ErrorHandling.Exceptions;
    using Severino.Infrastructure.Monad;
    using Severino.Infrastructure.Pagination;
    using Severino.WebApi.Features.Shared;

    using static Severino.Domain.Shared.Commands.DeleteParam<Domain.Shared.Email>;
    using static Severino.Domain.Shared.Commands.UpsertParam<Domain.Shared.Email, Severino.Domain.User.User>;
    using static Severino.Domain.Shared.Email;
    using static Severino.Domain.Shared.Queries.GetAllParam;
    using static Severino.Domain.Shared.Queries.GetByIdParam<Domain.Shared.Email>;
    using static Severino.Infrastructure.Monad.Utils.Util;
    using static Severino.WebApi.Features.User.UserModel;

    [ApiController]
    [ApiVersion("1")]
    [Route("users")]
    public class UsersController : BaseController
    {
        private readonly IGetUsers getUsers;
        private readonly IGetUserByEmail getUserByEmail;
        private readonly IUpsertUser upsertUser;
        private readonly IDeleteUser deleteUser;
        private readonly CreateUserModelValidator createValidator;
        private readonly UpdateUserModelValidator updateValidator;

        public UsersController(
            IGetUsers getUsers,
            IGetUserByEmail getUserByEmail,
            IUpsertUser upsertUser,
            IDeleteUser deleteUser)
        {
            this.getUsers = getUsers;
            this.getUserByEmail = getUserByEmail;
            this.upsertUser = upsertUser;
            this.deleteUser = deleteUser;
            this.createValidator = new CreateUserModelValidator();
            this.updateValidator = new UpdateUserModelValidator();
        }

        /// <summary>
        /// Get.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(Try<Page<Try<UserModel>>>), 200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetUsers()
        {
            var entities = await this.getUsers.GetResult(NewGetByAllParam(this.Request.QueryString.Value));

            return entities.Match(
                this.Error<Page<Try<UserModel>>>,
                page => this.Ok(page.ToPage(NewUserModel)));
        }

        /// <summary>
        /// Get by id.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpGet("{email}")]
        [ProducesResponseType(typeof(Try<UserModel>), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetUserById([FromRoute] string email)
        {
            var entity = await this.getUserByEmail.GetResult(NewGetByIdParam(NewEmail(email)));

            return entity.Match(
                this.Error<UserModel>,
                user => this.Ok(Success(NewUserModel(user))));
        }

        /// <summary>
        /// Create.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(Try<UserModel>), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(409)]
        [ProducesResponseType(422)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserModel request)
        {
            var validated = await this.createValidator.ValidateAsync(request);
            if (!validated.IsValid)
            {
                return this.Error<UserModel>(new InvalidObjectException("Invalid user.", validated));
            }

            if (await this.getUserByEmail.GetResult(NewGetByIdParam(NewEmail(request.Email))))
            {
                return this.Error<UserModel>(new AlreadyExistsException("User already exists."));
            }

            Option<User> entity = request;
            var @try = await this.upsertUser.Execute(NewUpsertParam(entity));

            return @try.Match(
                this.Error<UserModel>,
                _ => this.Created(entity.Get().Id, Success(NewUserModel(entity.Get()))));
        }

        /// <summary>
        /// Update.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("{email}")]
        [ProducesResponseType(typeof(Try<UserModel>), 201)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(422)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> UpdateUser(
            [FromRoute] string email,
            [FromBody] UpdateUserModel request)
        {
            var validated = await this.updateValidator.ValidateAsync(request);
            if (!validated.IsValid)
            {
                return this.Error<UserModel>(new InvalidObjectException("Invalid user.", validated));
            }

            var entity = await this.getUserByEmail.GetResult(NewGetByIdParam(NewEmail(email)));

            var newEntity = request.ToEntity(NewEmail(email));
            var @try = await this.upsertUser.Execute(NewUpsertParam(NewEmail(email), newEntity));

            return @try.Match(
                this.Error<UserModel>,
                _ => entity
                    ? this.NoContent()
                    : this.Created(newEntity.Get().Id, Success(NewUserModel(newEntity.Get()))));
        }

        /// <summary>
        /// Exclude.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpDelete("{email}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeleteUser([FromRoute] string email)
        {
            var deleted = await this.deleteUser.Execute(NewDeleteParam(NewEmail(email)));

            return deleted.Match(
                this.Error<UserModel>,
                _ => this.NoContent());
        }
    }
}
