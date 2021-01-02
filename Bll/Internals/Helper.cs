using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using NuScien.Configurations;
using NuScien.Data;
using Trivial.Collection;
using Trivial.Data;
using Trivial.Reflection;
using Trivial.Text;
using Trivial.Security;

namespace NuScien.Sample.Internals
{
    /// <summary>
    /// Helper and extension functions.
    /// </summary>
    internal static class Helper
    {
        /// <summary>
        /// The common page size.
        /// </summary>
        public const int PageSize = 20;
    }
}
