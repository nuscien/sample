using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NuScien.Data;
using NuScien.Web;
using Trivial.Data;
using Trivial.Security;
using Trivial.Text;

namespace NuScien.Sample.Web.Controllers
{
    /// <summary>
    /// The customer controller.
    /// </summary>
    [ApiController]
    [Route("api/goods")]
    public class GoodController : ResourceEntityControllerBase<GoodEntityProvider, GoodEntity>
    {
        /// <summary>
        /// Initializes a new instance of the GoodController class.
        /// </summary>
        public GoodController()
        {
        }

        /// <summary>
        /// Initializes a new instance of the GoodController class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        public GoodController(ILogger<GoodController> logger) : base(logger)
        {
        }
        /// <inheritdoc />
        protected override async Task<GoodEntityProvider> GetProviderAsync()
        {
            var context = await this.GetBusinessContextAsync();
            return context.Goods;
        }
    }
}
