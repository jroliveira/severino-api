namespace Severino.WebApi.Features.ApiResource
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
    using static Severino.Domain.Shared.Commands.UpsertParam<string, Severino.Domain.Resource.ApiResource>;
    using static Severino.Domain.Shared.Queries.GetAllParam;
    using static Severino.Domain.Shared.Queries.GetByIdParam<string>;
    using static Severino.Infrastructure.Monad.Utils.Util;
    using static Severino.WebApi.Features.ApiResource.ApiResourceModel;

    [ApiController]
    [ApiVersion("1")]
    [Route("api-resources")]
    public class ApiResourcesController : BaseController
    {
        private readonly IGetApiResources getApiResources;
        private readonly IGetApiResourceByName getApiResourceByName;
        private readonly IUpsertApiResource upsertApiResource;
        private readonly IDeleteResource deleteResource;
        private readonly CreateApiResourceModelValidator createValidator;
        private readonly UpdateApiResourceModelValidator updateValidator;

        public ApiResourcesController(
            IGetApiResources getApiResources,
            IGetApiResourceByName getApiResourceByName,
            IUpsertApiResource upsertApiResource,
            IDeleteResource deleteResource)
        {
            this.getApiResources = getApiResources;
            this.getApiResourceByName = getApiResourceByName;
            this.upsertApiResource = upsertApiResource;
            this.deleteResource = deleteResource;
            this.createValidator = new CreateApiResourceModelValidator();
            this.updateValidator = new UpdateApiResourceModelValidator();
        }

        /// <summary>
        /// Get.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(Try<Page<Try<ApiResourceModel>>>), 200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetApiResources()
        {
            var entities = await this.getApiResources.GetResult(NewGetByAllParam(this.Request.QueryString.Value));

            return entities.Match(
                this.Error<Page<Try<ApiResourceModel>>>,
                page => this.Ok(page.ToPage(NewApiResourceModel)));
        }

        /// <summary>
        /// Get by name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("{name}")]
        [ProducesResponseType(typeof(Try<ApiResourceModel>), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetApiResourceById([FromRoute] string name)
        {
            var entity = await this.getApiResourceByName.GetResult(NewGetByIdParam(name));

            return entity.Match(
                this.Error<ApiResourceModel>,
                apiResource => this.Ok(Success(NewApiResourceModel(apiResource))));
        }

        /// <summary>
        /// Create.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(Try<ApiResourceModel>), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(409)]
        [ProducesResponseType(422)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateApiResource([FromBody] CreateApiResourceModel request)
        {
            var validated = await this.createValidator.ValidateAsync(request);
            if (!validated.IsValid)
            {
                return this.Error<ApiResourceModel>(new InvalidObjectException("Invalid api resource.", validated));
            }

            if (await this.getApiResourceByName.GetResult(NewGetByIdParam(request.Name)))
            {
                return this.Error<ApiResourceModel>(new AlreadyExistsException("Api resource already exists."));
            }

            Option<ApiResource> entity = request;
            var @try = await this.upsertApiResource.Execute(NewUpsertParam(entity));

            return @try.Match(
                this.Error<ApiResourceModel>,
                _ => this.Created(entity.Get().Id, Success(NewApiResourceModel(entity.Get()))));
        }

        /// <summary>
        /// Update.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("{name}")]
        [ProducesResponseType(typeof(Try<ApiResourceModel>), 201)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(422)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> UpdateApiResource(
            [FromRoute] string name,
            [FromBody] UpdateApiResourceModel request)
        {
            var validated = await this.updateValidator.ValidateAsync(request);
            if (!validated.IsValid)
            {
                return this.Error<ApiResourceModel>(new InvalidObjectException("Invalid api resource.", validated));
            }

            var entity = await this.getApiResourceByName.GetResult(NewGetByIdParam(name));

            var newEntity = request.ToEntity(name);
            var @try = await this.upsertApiResource.Execute(NewUpsertParam(name, newEntity));

            return @try.Match(
                this.Error<ApiResourceModel>,
                _ => entity
                    ? this.NoContent()
                    : this.Created(newEntity.Get().Id, Success(NewApiResourceModel(newEntity.Get()))));
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
        public async Task<IActionResult> DeleteApiResource([FromRoute] string name)
        {
            var deleted = await this.deleteResource.Execute(NewDeleteParam(name));

            return deleted.Match(
                this.Error<ApiResourceModel>,
                _ => this.NoContent());
        }
    }
}
