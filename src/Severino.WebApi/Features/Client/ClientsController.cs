namespace Severino.WebApi.Features.Client
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using Severino.Domain.Client;
    using Severino.Domain.Client.Commands;
    using Severino.Domain.Client.Queries;
    using Severino.Infrastructure.ErrorHandling.Exceptions;
    using Severino.Infrastructure.Monad;
    using Severino.Infrastructure.Pagination;
    using Severino.WebApi.Features.Shared;

    using static Severino.Domain.Shared.Commands.DeleteParam<string>;
    using static Severino.Domain.Shared.Commands.UpsertParam<string, Severino.Domain.Client.Client>;
    using static Severino.Domain.Shared.Queries.GetAllParam;
    using static Severino.Domain.Shared.Queries.GetByIdParam<string>;
    using static Severino.Infrastructure.Monad.Utils.Util;
    using static Severino.WebApi.Features.Client.ClientModel;

    [ApiController]
    [ApiVersion("1")]
    [Route("clients")]
    public class ClientsController : BaseController
    {
        private readonly IGetClients getClients;
        private readonly IGetClientById getClientById;
        private readonly IUpsertClient upsertClient;
        private readonly IDeleteClient deleteClient;
        private readonly CreateClientModelValidator createValidator;
        private readonly UpdateClientModelValidator updateValidator;

        public ClientsController(
            IGetClients getClients,
            IGetClientById getClientById,
            IUpsertClient upsertClient,
            IDeleteClient deleteClient)
        {
            this.getClients = getClients;
            this.getClientById = getClientById;
            this.upsertClient = upsertClient;
            this.deleteClient = deleteClient;
            this.createValidator = new CreateClientModelValidator();
            this.updateValidator = new UpdateClientModelValidator();
        }

        /// <summary>
        /// Get.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(Try<Page<Try<ClientModel>>>), 200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetClients()
        {
            var entities = await this.getClients.GetResult(NewGetByAllParam(this.Request.QueryString.Value));

            return entities.Match(
                this.Error<Page<Try<ClientModel>>>,
                page => this.Ok(page.ToPage(NewClientModel)));
        }

        /// <summary>
        /// Get by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Try<ClientModel>), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetClientById([FromRoute] string id)
        {
            var entity = await this.getClientById.GetResult(NewGetByIdParam(id));

            return entity.Match(
                this.Error<ClientModel>,
                client => this.Ok(Success(NewClientModel(client))));
        }

        /// <summary>
        /// Create.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(Try<ClientModel>), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(409)]
        [ProducesResponseType(422)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateClient([FromBody] CreateClientModel request)
        {
            var validated = await this.createValidator.ValidateAsync(request);
            if (!validated.IsValid)
            {
                return this.Error<ClientModel>(new InvalidObjectException("Invalid client.", validated));
            }

            if (await this.getClientById.GetResult(NewGetByIdParam(request.Id)))
            {
                return this.Error<ClientModel>(new AlreadyExistsException("Client already exists."));
            }

            Option<Client> entity = request;
            var @try = await this.upsertClient.Execute(NewUpsertParam(entity));

            return @try.Match(
                this.Error<ClientModel>,
                _ => this.Created(entity.Get().Id, Success(NewClientModel(entity.Get()))));
        }

        /// <summary>
        /// Update.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Try<ClientModel>), 201)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(422)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> UpdateClient(
            [FromRoute] string id,
            [FromBody] UpdateClientModel request)
        {
            var validated = await this.updateValidator.ValidateAsync(request);
            if (!validated.IsValid)
            {
                return this.Error<ClientModel>(new InvalidObjectException("Invalid client.", validated));
            }

            var entity = await this.getClientById.GetResult(NewGetByIdParam(id));

            var newEntity = request.ToEntity(id);
            var @try = await this.upsertClient.Execute(NewUpsertParam(id, newEntity));

            return @try.Match(
                this.Error<ClientModel>,
                _ => entity
                    ? this.NoContent()
                    : this.Created(newEntity.Get().Id, Success(NewClientModel(newEntity.Get()))));
        }

        /// <summary>
        /// Exclude.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeleteClient([FromRoute] string id)
        {
            var deleted = await this.deleteClient.Execute(NewDeleteParam(id));

            return deleted.Match(
                this.Error<ClientModel>,
                _ => this.NoContent());
        }
    }
}
