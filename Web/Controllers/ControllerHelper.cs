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
    internal static class ControllerHelper
    {
        public static async Task<OnPremisesBusinessContext> GetBusinessContextAsync(this ControllerBase controller, bool isReadonly = false)
        {
            var client = await controller.GetResourceAccessClientAsync() as OnPremisesResourceAccessClient;
            return OnPremisesBusinessContext.Create(client, isReadonly);
        }
    }
}
