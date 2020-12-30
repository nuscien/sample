using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Common;
using System.Linq;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Principal;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using NuScien.Configurations;
using NuScien.Data;
using NuScien.Users;
using Trivial.Data;
using Trivial.Reflection;
using Trivial.Text;

namespace NuScien.Sample.Internals
{
    /// <summary>
    /// Internal business database context.
    /// Please do NOT use this class out of this library.
    /// </summary>
    public class BusinessDbContext : DbContext
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the BusinessDbContext class.
        /// The Microsoft.EntityFrameworkCore.DbContext.OnConfiguring(Microsoft.EntityFrameworkCore.DbContextOptionsBuilder)
        /// method will be called to configure the database (and other options) to be used for this context.
        /// </summary>
        protected BusinessDbContext()
        {
        }

        /// <summary>
        /// Initializes a new instance of the BusinessDbContext class.
        /// It can use a specified options.
        /// The Microsoft.EntityFrameworkCore.DbContext.OnConfiguring(Microsoft.EntityFrameworkCore.DbContextOptionsBuilder)
        /// method will still be called to allow further configuration of the options.
        /// </summary>
        /// <param name="options">The options for this context.</param>
        public BusinessDbContext(DbContextOptions options)
            : base(options)
        {
        }

        /// <summary>
        /// Initializes a new instance of the BusinessDbContext class.
        /// The Microsoft.EntityFrameworkCore.DbContext.OnConfiguring(Microsoft.EntityFrameworkCore.DbContextOptionsBuilder)
        /// method will still be called to allow further configuration of the options.
        /// </summary>
        /// <param name="configureConnection">The method to configure context options with connection string.</param>
        /// <param name="connection">The database connection.</param>
        public BusinessDbContext(Func<DbContextOptionsBuilder, DbConnection, DbContextOptionsBuilder> configureConnection, DbConnection connection)
            : base(Helper.CreateDbContextOptions<BusinessDbContext>(configureConnection, connection))
        {
        }

        /// <summary>
        /// Initializes a new instance of the BusinessDbContext class.
        /// The Microsoft.EntityFrameworkCore.DbContext.OnConfiguring(Microsoft.EntityFrameworkCore.DbContextOptionsBuilder)
        /// method will still be called to allow further configuration of the options.
        /// </summary>
        /// <param name="configureConnection">The method to configure context options with connection string.</param>
        /// <param name="connection">The connection string.</param>
        public BusinessDbContext(Func<DbContextOptionsBuilder, string, DbContextOptionsBuilder> configureConnection, string connection)
            : base(Helper.CreateDbContextOptions<BusinessDbContext>(configureConnection, connection))
        {
        }

        /// <summary>
        /// Initializes a new instance of the BusinessDbContext class.
        /// The Microsoft.EntityFrameworkCore.DbContext.OnConfiguring(Microsoft.EntityFrameworkCore.DbContextOptionsBuilder)
        /// method will still be called to allow further configuration of the options.
        /// </summary>
        /// <param name="configureConnection">The method to configure context options with connection string.</param>
        /// <param name="connection">The database connection.</param>
        /// <param name="optionsAction">The additional options action.</param>
        public BusinessDbContext(Func<DbContextOptionsBuilder<BusinessDbContext>, DbConnection, Action<DbContextOptionsBuilder<BusinessDbContext>>, DbContextOptionsBuilder<BusinessDbContext>> configureConnection, DbConnection connection, Action<DbContextOptionsBuilder<BusinessDbContext>> optionsAction)
            : base(Helper.CreateDbContextOptions(configureConnection, connection, optionsAction))
        {
        }

        /// <summary>
        /// Initializes a new instance of the BusinessDbContext class.
        /// The Microsoft.EntityFrameworkCore.DbContext.OnConfiguring(Microsoft.EntityFrameworkCore.DbContextOptionsBuilder)
        /// method will still be called to allow further configuration of the options.
        /// </summary>
        /// <param name="configureConnection">The method to configure context options with connection string.</param>
        /// <param name="connection">The connection string.</param>
        /// <param name="optionsAction">The additional options action.</param>
        public BusinessDbContext(Func<DbContextOptionsBuilder<BusinessDbContext>, string, Action<DbContextOptionsBuilder<BusinessDbContext>>, DbContextOptionsBuilder<BusinessDbContext>> configureConnection, string connection, Action<DbContextOptionsBuilder<BusinessDbContext>> optionsAction)
            : base(Helper.CreateDbContextOptions(configureConnection, connection, optionsAction))
        {
        }

        #endregion

        #region database sets

        /// <summary>
        /// Gets the settings database set.
        /// </summary>
        public DbSet<CustomerEntity> Customers { get; }

        #endregion

        #region Other members

        #endregion

        #region Helpers

        public static Func<bool, BusinessDbContext> Factory { internal get; set; }

        /// <summary>
        /// Creates a new one.
        /// </summary>
        /// <returns>The database context instance.</returns>
        public static BusinessDbContext Create(bool isReadOnly)
        {
            var h = Factory;
            return h != null ? h(isReadOnly) : new BusinessDbContext();
        }

        #endregion
    }
}
