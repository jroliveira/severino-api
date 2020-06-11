namespace Severino.WebApi.Features.IdentityResource
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using Severino.Domain.Resource;
    using Severino.Domain.Resource.Commands;
    using Severino.Domain.Resource.Queries;
    using Severino.Infrastructure.ErrorHandling.Exceptions;
    using Severino.Infrastructure.Monad;
    using Severino.Infrastructure.Pagination;
    using Severino.WebApi.Features.Shared;

    using static Severino.Domain.Shared.Commands.DeleteParam<string>;
    using static Severino.Domain.Shared.Commands.UpsertParam<string, Severino.Domain.Resource.IdentityResource>;
    using static Severino.Domain.Shared.Queries.GetAllParam;
    using static Severino.Domain.Shared.Queries.GetByIdParam<string>;
    using static Severino.Infrastructure.Monad.Utils.Util;
    using static Severino.WebApi.Features.IdentityResource.IdentityResourceModel;

    [ApiController]
    [ApiVersion("1")]
    [Route("identity-resources")]
    public class IdentityResourcesController : BaseController
    {
        private readonly IGetIdentityResources getIdentityResources;
        private readonly IGetIdentityResourceByName getIdentityResourceByName;
        private readonly IUpsertIdentityResource upsertIdentityResource;
        private readonly IDeleteResource deleteResource;
        private readonly CreateIdentityResourceModelValidator createValidator;
        private readonly UpdateIdentityResourceModelValidator updateValidator;

        public IdentityResourcesController(
            IGetIdentityResources getIdentityResources,
            IGetIdentityResourceByName getIdentityResourceByName,
            IUpsertIdentityResource upsertIdentityResource,
            IDeleteResource deleteResource)
        {
            this.getIdentityResources = getIdentityResources;
            this.getIdentityResourceByName = getIdentityResourceByName;
            this.upsertIdentityResource = upsertIdentityResource;
            this.deleteResource = deleteResource;
            this.createValidator = new CreateIdentityResourceModelValidator();
            this.updateValidator = new UpdateIdentityResourceModelValidator();
        }

        /// <summary>
        /// Get.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(Try<Page<Try<IdentityResourceModel>>>), 200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetIdentityResources()
        {
            var entities = await this.getIdentityResources.GetResult(NewGetByAllParam(this.Request.QueryString.Value));

            return entities.Match(
                this.Error<Page<Try<IdentityResourceModel>>>,
                page => this.Ok(page.ToPage(NewIdentityResourceModel)));
        }

        /// <summary>
        /// Get by name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("{name}")]
        [ProducesResponseType(typeof(Try<IdentityResourceModel>), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetIdentityResourceById([FromRoute] string name)
        {
            var entity = await this.getIdentityResourceByName.GetResult(NewGetByIdParam(name));

            return entity.Match(
                this.Error<IdentityResourceModel>,
                identityResource => this.Ok(Success(NewIdentityResourceModel(identityResource))));
        }

        /// <summary>
        /// Create.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(Try<IdentityResourceModel>), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(409)]
        [ProducesResponseType(422)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateIdentityResource([FromBody] CreateIdentityResourceModel request)
        {
            var validated = await this.createValidator.ValidateAsync(request);
            if (!validated.IsValid)
            {
                return this.Error<IdentityResourceModel>(new InvalidObjectException("Invalid identity resource.", validated));
            }

            if (await this.getIdentityResourceByName.GetResult(NewGetByIdParam(request.Name)))
            {
                return this.Error<IdentityResourceModel>(new AlreadyExistsException("Identity resource already exists."));
            }

            Option<IdentityResource> entity = request;
            var @try = await this.upsertIdentityResource.Execute(NewUpsertParam(entity));

            return @try.Match(
                this.Error<IdentityResourceModel>,
                _ => this.Created(entity.Get().Id, Success(NewIdentityResourceModel(entity.Get()))));
        }

        /// <summary>
        /// Update.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("{name}")]
        [ProducesResponseType(typeof(Try<IdentityResourceModel>), 201)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(422)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> UpdateIdentityResource(
            [FromRoute] string name,
            [FromBody] UpdateIdentityResourceModel request)
        {
            var validated = await this.updateValidator.ValidateAsync(request);
            if (!validated.IsValid)
            {
                return this.Error<IdentityResourceModel>(new InvalidObjectException("Invalid identity resource.", validated));
            }

            var entity = await this.getIdentityResourceByName.GetResult(NewGetByIdParam(name));

            var newEntity = request.ToEntity(name);
            var @try = await this.upsertIdentityResource.Execute(NewUpsertParam(name, newEntity));

            return @try.Match(
                this.Error<IdentityResourceModel>,
                _ => entity
                    ? this.NoContent()
                    : this.Created(newEntity.Get().Id, Success(NewIdentityResourceModel(newEntity.Get()))));
        }

        /// <summary>
        /// Exclude.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpDelete("{name}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeleteIdentityResource([FromRoute] string name)
        {
            var deleted = await this.deleteResource.Execute(NewDeleteParam(name));

            return deleted.Match(
                this.Error<IdentityResourceModel>,
                _ => this.NoContent());
        }
    }
}
