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
    [Route("api")]
    public class CustomerController : Controller
    {
        /// <summary>
        /// Searches customers.
        /// </summary>
        /// <returns>The customers.</returns>
        [HttpGet]
        [Route("customer/{id}")]
        public async Task<IActionResult> Get(string id)
        {
            return this.ResourceEntityResult(await CustomerEntity.GetById(id));
        }

        /// <summary>
        /// Searches customers.
        /// </summary>
        /// <returns>The customers.</returns>
        [HttpGet]
        [Route("customers")]
        public IActionResult Search()
        {
            var q = Request.Query.GetQueryArgs();
            var col = CustomerEntity.Search(q).ToList();
            return new JsonResult(new CollectionResult<CustomerEntity>(col, q?.Offset));
        }
    }
}
