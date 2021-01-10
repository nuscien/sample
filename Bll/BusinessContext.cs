using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using NuScien.Data;
using NuScien.Security;
using Trivial.Data;
using Trivial.Net;
using Trivial.Reflection;
using Trivial.Security;
using Trivial.Text;

namespace NuScien.Sample
{
    /// <summary>
    /// The resource accessing context on-premises.
    /// </summary>
    public class OnPremisesBusinessContext
    {
        /// <summary>
        /// Internal business database context.
        /// Please do NOT use this class out of this library.
        /// </summary>
        public class InternalDbContext : DbContext
        {
            #region Constructors

            /// <summary>
            /// Initializes a new instance of the InternalDbContext class.
            /// The Microsoft.EntityFrameworkCore.DbContext.OnConfiguring(Microsoft.EntityFrameworkCore.DbContextOptionsBuilder)
            /// method will be called to configure the database (and other options) to be used for this context.
            /// </summary>
            protected InternalDbContext()
            {
            }

            /// <summary>
            /// Initializes a new instance of the InternalDbContext class.
            /// It can use a specified options.
            /// The Microsoft.EntityFrameworkCore.DbContext.OnConfiguring(Microsoft.EntityFrameworkCore.DbContextOptionsBuilder)
            /// method will still be called to allow further configuration of the options.
            /// </summary>
            /// <param name="options">The options for this context.</param>
            public InternalDbContext(DbContextOptions options)
                : base(options)
            {
            }

            #endregion

            #region Database sets

            /// <summary>
            /// Gets the settings database set.
            /// </summary>
            public DbSet<CustomerEntity> Customers { get; }

            #endregion

            #region Other members

            #endregion

            #region Helpers

            #endregion
        }

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the OnPremisesBusinessContext class.
        /// </summary>
        /// <param name="client">The resource access client.</param>
        /// <param name="dbContext">The database context.</param>
        public OnPremisesBusinessContext(OnPremisesResourceAccessClient client, InternalDbContext dbContext)
        {
            CoreResources = client;
            
            // Initialize all entity providers.
            Customers = new CustomerEntityProvider(client, dbContext.Customers, dbContext.SaveChangesAsync);
        }

        /// <summary>
        /// Initializes a new instance of the OnPremisesBusinessContext class.
        /// </summary>
        /// <param name="client">The resource access client.</param>
        /// <param name="options">The options for this context.</param>
        public OnPremisesBusinessContext(OnPremisesResourceAccessClient client, DbContextOptions options)
            : this(client, new InternalDbContext(options))
        {
        }

        /// <summary>
        /// Initializes a new instance of the OnPremisesBusinessContext class.
        /// </summary>
        /// <param name="client">The resource access client.</param>
        /// <param name="configureConnection">The method to configure context options with connection string.</param>
        /// <param name="connection">The database connection.</param>
        public OnPremisesBusinessContext(OnPremisesResourceAccessClient client, Func<DbContextOptionsBuilder, DbConnection, DbContextOptionsBuilder> configureConnection, DbConnection connection)
            : this(client, new InternalDbContext(DbResourceEntityExtensions.CreateDbContextOptions<InternalDbContext>(configureConnection, connection)))
        {
        }

        /// <summary>
        /// Initializes a new instance of the OnPremisesBusinessContext class.
        /// </summary>
        /// <param name="client">The resource access client.</param>
        /// <param name="configureConnection">The method to configure context options with connection string.</param>
        /// <param name="connection">The connection string.</param>
        public OnPremisesBusinessContext(OnPremisesResourceAccessClient client, Func<DbContextOptionsBuilder, string, DbContextOptionsBuilder> configureConnection, string connection)
            : this(client, new InternalDbContext(DbResourceEntityExtensions.CreateDbContextOptions<InternalDbContext>(configureConnection, connection)))
        {
        }

        /// <summary>
        /// Initializes a new instance of the OnPremisesBusinessContext class.
        /// </summary>
        /// <param name="client">The resource access client.</param>
        /// <param name="configureConnection">The method to configure context options with connection string.</param>
        /// <param name="connection">The database connection.</param>
        /// <param name="optionsAction">The additional options action.</param>
        public OnPremisesBusinessContext(OnPremisesResourceAccessClient client, Func<DbContextOptionsBuilder<InternalDbContext>, DbConnection, Action<DbContextOptionsBuilder<InternalDbContext>>, DbContextOptionsBuilder<InternalDbContext>> configureConnection, DbConnection connection, Action<DbContextOptionsBuilder<InternalDbContext>> optionsAction)
            : this(client, new InternalDbContext(DbResourceEntityExtensions.CreateDbContextOptions(configureConnection, connection, optionsAction)))
        {
        }

        /// <summary>
        /// Initializes a new instance of the OnPremisesBusinessContext class.
        /// </summary>
        /// <param name="client">The resource access client.</param>
        /// <param name="configureConnection">The method to configure context options with connection string.</param>
        /// <param name="connection">The connection string.</param>
        /// <param name="optionsAction">The additional options action.</param>
        public OnPremisesBusinessContext(OnPremisesResourceAccessClient client, Func<DbContextOptionsBuilder<InternalDbContext>, string, Action<DbContextOptionsBuilder<InternalDbContext>>, DbContextOptionsBuilder<InternalDbContext>> configureConnection, string connection, Action<DbContextOptionsBuilder<InternalDbContext>> optionsAction)
            : this(client, new InternalDbContext(DbResourceEntityExtensions.CreateDbContextOptions(configureConnection, connection, optionsAction)))
        {
        }

        #endregion

        #region Data providers

        /// <summary>
        /// Gets the resources access client.
        /// </summary>
        public OnPremisesResourceAccessClient CoreResources { get; }

        /// <summary>
        /// Gets the customer data provider.
        /// </summary>
        public CustomerEntityProvider Customers { get; }

        #endregion

        #region Other members

        #endregion

        #region Helpers

        public static Func<OnPremisesResourceAccessClient, bool, OnPremisesBusinessContext> Factory { internal get; set; }

        /// <summary>
        /// Creates a new one.
        /// </summary>
        /// <returns>The database context instance.</returns>
        public static OnPremisesBusinessContext Create(OnPremisesResourceAccessClient client, bool isReadOnly)
        {
            var h = Factory;
            return h != null ? h(client, isReadOnly) : null;
        }


        #endregion
    }

    /// <summary>
    /// The resource accessing context for HTTP client.
    /// </summary>
    public class HttpBusinessContext
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the HttpBusinessContext class.
        /// </summary>
        /// <param name="client">The resource access client.</param>
        public HttpBusinessContext(HttpResourceAccessClient client)
        {
            CoreResources = client;

            // Initialize all entity providers.
            Customers = new CustomerEntityClient(client);
        }

        /// <summary>
        /// Initializes a new instance of the HttpBusinessContext class.
        /// </summary>
        /// <param name="appKey">The app secret key for accessing API.</param>
        /// <param name="host">The host URI.</param>
        public HttpBusinessContext(AppAccessingKey appKey, Uri host)
            : this(new HttpResourceAccessClient(appKey, host))
        {
        }

        #endregion

        #region Data providers

        /// <summary>
        /// Gets the resources access client.
        /// </summary>
        public HttpResourceAccessClient CoreResources { get; }

        /// <summary>
        /// Gets the customer data client.
        /// </summary>
        public CustomerEntityClient Customers { get; }

        #endregion

        #region Other members

        #endregion
    }
}
