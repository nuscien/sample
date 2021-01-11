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
    public class OnPremisesBusinessContext : IDisposable
    {
        #region Fields

        private bool disposedValue;
        private DbContext db;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the OnPremisesBusinessContext class.
        /// </summary>
        /// <param name="client">The resource access client.</param>
        /// <param name="dbContext">The database context.</param>
        public OnPremisesBusinessContext(OnPremisesResourceAccessClient client, DbContext dbContext)
        {
            CoreResources = client;
            db = dbContext;

            // Initialize all entity providers.
            Customers = new CustomerEntityProvider(client, dbContext.Set<CustomerEntity>(), dbContext.SaveChangesAsync);
        }

        /// <summary>
        /// Initializes a new instance of the OnPremisesBusinessContext class.
        /// </summary>
        /// <param name="client">The resource access client.</param>
        /// <param name="options">The options for this context.</param>
        public OnPremisesBusinessContext(OnPremisesResourceAccessClient client, DbContextOptions options)
            : this(client, new DbContext(options))
        {
        }

        /// <summary>
        /// Initializes a new instance of the OnPremisesBusinessContext class.
        /// </summary>
        /// <param name="client">The resource access client.</param>
        /// <param name="configureConnection">The method to configure context options with connection string.</param>
        /// <param name="connection">The database connection.</param>
        public OnPremisesBusinessContext(OnPremisesResourceAccessClient client, Func<DbContextOptionsBuilder, DbConnection, DbContextOptionsBuilder> configureConnection, DbConnection connection)
            : this(client, new DbContext(DbResourceEntityExtensions.CreateDbContextOptions<DbContext>(configureConnection, connection)))
        {
        }

        /// <summary>
        /// Initializes a new instance of the OnPremisesBusinessContext class.
        /// </summary>
        /// <param name="client">The resource access client.</param>
        /// <param name="configureConnection">The method to configure context options with connection string.</param>
        /// <param name="connection">The connection string.</param>
        public OnPremisesBusinessContext(OnPremisesResourceAccessClient client, Func<DbContextOptionsBuilder, string, DbContextOptionsBuilder> configureConnection, string connection)
            : this(client, new DbContext(DbResourceEntityExtensions.CreateDbContextOptions<DbContext>(configureConnection, connection)))
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
            : this(client, new DbContext(DbResourceEntityExtensions.CreateDbContextOptions(configureConnection, connection, optionsAction)))
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
            : this(client, new DbContext(DbResourceEntityExtensions.CreateDbContextOptions(configureConnection, connection, optionsAction)))
        {
        }

        #endregion

        #region Events

        /// <summary>
        /// An event fired at the beginning of a call to SaveChanges or SaveChangesAsync
        /// </summary>
        public event EventHandler<SavingChangesEventArgs> SavingChanges
        {
            add => db.SavingChanges += value;
            remove => db.SavingChanges -= value;
        }

        /// <summary>
        /// An event fired at the end of a call to SaveChanges or SaveChangesAsync
        /// </summary>
        public event EventHandler<SavedChangesEventArgs> SavedChanges
        {
            add => db.SavedChanges += value;
            remove => db.SavedChanges -= value;
        }

        /// <summary>
        /// An event fired if a call to SaveChanges or SaveChangesAsync fails with an exception.
        /// </summary>
        public event EventHandler<SaveChangesFailedEventArgs> SaveChangesFailed
        {
            add => db.SaveChangesFailed += value;
            remove => db.SaveChangesFailed -= value;
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

        #region Entity accessing

        /// <summary>
        /// Gets the database related information and operations for this context.
        /// </summary>
        protected Microsoft.EntityFrameworkCore.Infrastructure.DatabaseFacade Database => db.Database;

        /// <summary>
        /// Creates a DbSet that can be used to query and save instances of TEntity.
        /// </summary>
        /// <typeparam name="TEntity">The type of entity for which a set should be returned.</typeparam>
        /// <returns>A set for the given entity type.</returns>
        protected DbSet<TEntity> Set<TEntity>() where TEntity : class => db.Set<TEntity>();

        /// <summary>
        /// Creates a DbSet that can be used to query and save instances of TEntity.
        /// </summary>
        /// <typeparam name="TEntity">The type of entity for which a set should be returned.</typeparam>
        /// <param name="name">The name.</param>
        /// <returns>A set for the given entity type.</returns>
        protected DbSet<TEntity> Set<TEntity>(string name) where TEntity : class => db.Set<TEntity>(name);

        /// <summary>
        /// Saves all changes made in this context to the database.
        /// This method will automatically call Microsoft.EntityFrameworkCore.ChangeTracking.ChangeTracker.DetectChanges
        /// to discover any changes to entity instances before saving to the underlying database.
        /// This can be disabled via Microsoft.EntityFrameworkCore.ChangeTracking.ChangeTracker.AutoDetectChangesEnabled.
        /// Multiple active operations on the same context instance are not supported. Use
        /// 'await' to ensure that any asynchronous operations have completed before calling
        /// another method on this context.
        /// </summary>
        /// <param name="cancellationToken">An optional cancellation token to observe while waiting for the task to complete.</param>
        /// <returns>The number of state entries written to the database.</returns>
        protected Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) => db.SaveChangesAsync(cancellationToken);

        /// <summary>
        /// Asynchronously ensures that the database for the context exists. If it exists,
        /// no action is taken. If it does not exist then the database and all its schema
        /// are created. If the database exists, then no effort is made to ensure it is compatible
        /// with the model for this context.
        /// Note that this API does not use migrations to create the database. In addition,
        /// the database that is created cannot be later updated using migrations. If you
        /// are targeting a relational database and using migrations, you can use the DbContext.Database.Migrate()
        /// method to ensure the database is created and all migrations are applied.
        /// </summary>
        /// <param name="cancellationToken">An optional cancellation token to observe while waiting for the task to complete.</param>
        /// <returns>true if the database is created, false if it already existed.</returns>
        public Task<bool> EnsureDbCreatedAsync(CancellationToken cancellationToken = default) => db.Database.EnsureCreatedAsync(cancellationToken);

        #endregion

        #region Disposable

        /// <summary>
        /// Disposes.
        /// </summary>
        /// <param name="disposing">true if only for managed resources; otherwise, false.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposedValue) return;
            if (disposing)
            {
                db.Dispose();
            }

            disposedValue = true;
        }

        /// <summary>
        /// Disposes.
        /// </summary>
        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        #endregion

        #region Other members

        #endregion

        #region Helpers

        /// <summary>
        /// Sets the factory of current context.
        /// </summary>
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
