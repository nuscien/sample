using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
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
    public class GoodController : BaseResourceEntityController<GoodEntityProvider, GoodEntity>
    {
        /// <inheritdoc />
        protected override async Task<GoodEntityProvider> GetProviderAsync()
        {
            var context = await this.GetBusinessContextAsync();
            return context.Goods;
        }
    }
}
