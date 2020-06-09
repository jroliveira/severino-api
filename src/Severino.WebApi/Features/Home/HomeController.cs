namespace Severino.WebApi.Features.Home
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;

    using Severino.Infrastructure.Monad;

    using static Severino.Infrastructure.Monad.Utils.Util;

    [ApiController]
    [AllowAnonymous]
    [ApiVersion("1")]
    [Route("")]
    public class HomeController : ControllerBase
    {
        private readonly IWebHostEnvironment environment;

        public HomeController(IWebHostEnvironment environment) => this.environment = environment;

        /// <summary>
        /// Get.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ResponseCache(Duration = 60)]
        [ProducesResponseType(typeof(Try<HomeModel>), 200)]
        public IActionResult Get() => this.Ok(Success(new HomeModel(
            this.environment.ApplicationName,
            this.environment.EnvironmentName,
            "1.0.0")));
    }
}
