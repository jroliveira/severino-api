namespace Severino.WebApi.Infrastructure.Versioning
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Versioning;

    using Severino.Infrastructure.ErrorHandling.Exceptions;

    using static Severino.WebApi.Infrastructure.ErrorHandling.ErrorHandler;

    internal sealed class VersioningErrorResponseProvider : DefaultErrorResponseProvider
    {
        public override IActionResult CreateResponse(ErrorResponseContext context) => ErrorResult(new InvalidRequestException(context.Message));
    }
}
