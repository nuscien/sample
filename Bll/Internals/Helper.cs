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

        internal static DbContextOptions<T> CreateDbContextOptions<T>(Func<DbContextOptionsBuilder, DbConnection, DbContextOptionsBuilder> configureConnection, DbConnection connection)
     where T : DbContext
        {
            var b = new DbContextOptionsBuilder<T>();
            configureConnection(b, connection);
            return b.Options;
        }

        internal static DbContextOptions<T> CreateDbContextOptions<T>(Func<DbContextOptionsBuilder, string, DbContextOptionsBuilder> configureConnection, string connection)
             where T : DbContext
        {
            var b = new DbContextOptionsBuilder<T>();
            configureConnection(b, connection);
            return b.Options;
        }

        internal static DbContextOptions<T> CreateDbContextOptions<T>(Func<DbContextOptionsBuilder<T>, DbConnection, Action<DbContextOptionsBuilder<T>>, DbContextOptionsBuilder<T>> configureConnection, DbConnection connection, Action<DbContextOptionsBuilder<T>> optionsAction)
             where T : DbContext
        {
            var b = new DbContextOptionsBuilder<T>();
            configureConnection(b, connection, optionsAction);
            return b.Options;
        }

        internal static DbContextOptions<T> CreateDbContextOptions<T>(Func<DbContextOptionsBuilder<T>, string, Action<DbContextOptionsBuilder<T>>, DbContextOptionsBuilder<T>> configureConnection, string connection, Action<DbContextOptionsBuilder<T>> optionsAction)
             where T : DbContext
        {
            var b = new DbContextOptionsBuilder<T>();
            configureConnection(b, connection, optionsAction);
            return b.Options;
        }
    }
}
