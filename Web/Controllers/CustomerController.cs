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
    [Route("api/customers")]
    public class CustomerController : Controller
    {
        /// <summary>
        /// Searches customers.
        /// </summary>
        /// <returns>The customers.</returns>
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var context = await this.GetBusinessContextAsync(true);
            var entity = await context.Customers.GetAsync(id);
            return this.ResourceEntityResult(entity);
        }

        /// <summary>
        /// Searches customers.
        /// </summary>
        /// <returns>The customers.</returns>
        [HttpGet]
        public async Task<IActionResult> Search()
        {
            var context = await this.GetBusinessContextAsync(true);
            var q = Request.Query.GetQueryArgs();
            var col = await context.Customers.SearchAsync(q);
            return this.ResourceEntityResult(col.Value, col.Offset, col.Count);
        }

        /// <summary>
        /// Searches customers.
        /// </summary>
        /// <returns>The customers.</returns>
        [HttpPut]
        public async Task<ChangeMethodResult> Save([FromBody] CustomerEntity entity)
        {
            var context = await this.GetBusinessContextAsync(false);
            var result = await context.Customers.SaveAsync(entity);
            return result;
        }
    }
}
