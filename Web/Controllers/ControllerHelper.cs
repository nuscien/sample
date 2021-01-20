using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using NuScien.Data;
using NuScien.Security;
using NuScien.Web;
using Trivial.Net;
using Trivial.Reflection;
using Trivial.Security;
using Trivial.Text;

namespace NuScien.Sample.Web.Controllers
{
    /// <summary>
    /// Helpers for web API.
    /// </summary>
    internal static class ControllerHelper
    {
        /// <summary>
        /// Gets the business resource account context.
        /// </summary>
        /// <param name="controller">The MVC controller.</param>
        /// <returns>The business resource account context.</returns>
        public static async Task<OnPremisesBusinessContext> GetBusinessContextAsync(this ControllerBase controller)
        {
            var client = await controller.GetResourceAccessClientAsync() as OnPremisesResourceAccessClient;
            return OnPremisesBusinessContext.Create(client);
        }
    }
}
