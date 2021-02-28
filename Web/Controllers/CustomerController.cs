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
    [Route("api/customers")]
    public class CustomerController : ResourceEntityControllerBase<CustomerEntityProvider, CustomerEntity>
    {
        /// <summary>
        /// Initializes a new instance of the CustomerController class.
        /// </summary>
        public CustomerController()
        {
        }

        /// <summary>
        /// Initializes a new instance of the CustomerController class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        public CustomerController(ILogger<CustomerController> logger) : base(logger)
        {
        }

        /// <inheritdoc />
        protected override async Task<CustomerEntityProvider> GetProviderAsync()
        {
            var context = await this.GetBusinessContextAsync();
            return context.Customers;
        }
    }
}
