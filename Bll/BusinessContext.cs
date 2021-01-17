using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading;
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
    public class OnPremisesBusinessContext : OnPremisesResourceAccessContext
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the OnPremisesBusinessContext class.
        /// </summary>
        /// <param name="client">The resource access client.</param>
        /// <param name="dbContext">The database context.</param>
        public OnPremisesBusinessContext(OnPremisesResourceAccessClient client, DbContext dbContext)
            : base(client, dbContext)
        {
        }

        /// <summary>
        /// Initializes a new instance of the OnPremisesBusinessContext class.
        /// </summary>
        /// <param name="client">The resource access client.</param>
        /// <param name="options">The options for this context.</param>
        public OnPremisesBusinessContext(OnPremisesResourceAccessClient client, DbContextOptions options)
            : base(client, options)
        {
        }

        /// <summary>
        /// Initializes a new instance of the OnPremisesBusinessContext class.
        /// </summary>
        /// <param name="client">The resource access client.</param>
        /// <param name="configureConnection">The method to configure context options with connection string.</param>
        /// <param name="connection">The database connection.</param>
        public OnPremisesBusinessContext(OnPremisesResourceAccessClient client, Func<DbContextOptionsBuilder, DbConnection, DbContextOptionsBuilder> configureConnection, DbConnection connection)
            : base(client, configureConnection, connection)
        {
        }

        /// <summary>
        /// Initializes a new instance of the OnPremisesBusinessContext class.
        /// </summary>
        /// <param name="client">The resource access client.</param>
        /// <param name="configureConnection">The method to configure context options with connection string.</param>
        /// <param name="connection">The connection string.</param>
        public OnPremisesBusinessContext(OnPremisesResourceAccessClient client, Func<DbContextOptionsBuilder, string, DbContextOptionsBuilder> configureConnection, string connection)
            : base(client, configureConnection, connection)
        {
        }

        /// <summary>
        /// Initializes a new instance of the OnPremisesBusinessContext class.
        /// </summary>
        /// <param name="client">The resource access client.</param>
        /// <param name="configureConnection">The method to configure context options with connection string.</param>
        /// <param name="connection">The database connection.</param>
        /// <param name="optionsAction">The additional options action.</param>
        public OnPremisesBusinessContext(OnPremisesResourceAccessClient client, Func<DbContextOptionsBuilder<DbContext>, DbConnection, Action<DbContextOptionsBuilder<DbContext>>, DbContextOptionsBuilder<DbContext>> configureConnection, DbConnection connection, Action<DbContextOptionsBuilder<DbContext>> optionsAction)
            : base(client, configureConnection, connection, optionsAction)
        {
        }

        /// <summary>
        /// Initializes a new instance of the OnPremisesBusinessContext class.
        /// </summary>
        /// <param name="client">The resource access client.</param>
        /// <param name="configureConnection">The method to configure context options with connection string.</param>
        /// <param name="connection">The connection string.</param>
        /// <param name="optionsAction">The additional options action.</param>
        public OnPremisesBusinessContext(OnPremisesResourceAccessClient client, Func<DbContextOptionsBuilder<DbContext>, string, Action<DbContextOptionsBuilder<DbContext>>, DbContextOptionsBuilder<DbContext>> configureConnection, string connection, Action<DbContextOptionsBuilder<DbContext>> optionsAction)
            : base(client, configureConnection, connection, optionsAction)
        {
        }

        #endregion

        #region Data providers

        /// <summary>
        /// Gets the customer data provider.
        /// </summary>
        public CustomerEntityProvider Customers { get; }

        /// <summary>
        /// Gets the good data provider.
        /// </summary>
        public GoodEntityProvider Goods { get; }

        #endregion

        #region Other members

        #endregion

        #region Helpers

        /// <summary>
        /// Sets the factory of current context.
        /// </summary>
        public static Func<OnPremisesResourceAccessClient, OnPremisesBusinessContext> Factory { internal get; set; }

        /// <summary>
        /// Creates a new one.
        /// </summary>
        /// <returns>The database context instance.</returns>
        public static OnPremisesBusinessContext Create(OnPremisesResourceAccessClient client)
        {
            var h = Factory;
            return h != null ? h(client) : null;
        }

        #endregion
    }

    /// <summary>
    /// The resource accessing context for HTTP client.
    /// </summary>
    public class HttpBusinessContext : HttpResourceAccessContext
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the HttpBusinessContext class.
        /// </summary>
        /// <param name="client">The resource access client.</param>
        public HttpBusinessContext(HttpResourceAccessClient client) : base(client)
        {
        }

        /// <summary>
        /// Initializes a new instance of the HttpBusinessContext class.
        /// </summary>
        /// <param name="appKey">The app secret key for accessing API.</param>
        /// <param name="host">The host URI.</param>
        public HttpBusinessContext(AppAccessingKey appKey, Uri host) : base(appKey, host)
        {
        }

        #endregion

        #region Data providers

        /// <summary>
        /// Gets the customer data client.
        /// </summary>
        public CustomerEntityClient Customers { get; }

        /// <summary>
        /// Gets the good data client.
        /// </summary>
        public CustomerEntityClient Goods { get; }

        #endregion

        #region Other members

        #endregion
    }
}
