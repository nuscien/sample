using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using NuScien.Collection;
using NuScien.Data;
using NuScien.Security;
using Trivial.Data;
using Trivial.Reflection;
using Trivial.Text;
using Trivial.Net;
using Trivial.Security;

namespace NuScien.Sample
{
    /// <summary>
    /// The data provider for goods.
    /// </summary>
    public class GoodEntityProvider : OnPremisesResourceEntityProvider<GoodEntity>
    {
        /// <summary>
        /// Initializes a new instance of the GoodEntityProvider class.
        /// </summary>
        /// <param name="client">The resource access client.</param>
        /// <param name="set">The database set.</param>
        /// <param name="save">The entity save handler.</param>
        public GoodEntityProvider(OnPremisesResourceAccessClient client, DbSet<GoodEntity> set, Func<CancellationToken, Task<int>> save)
            : base(client, set, save) // Please make sure the entity provider contains a constructor with these parameters.
        {
        }

        /// <summary>
        /// Initializes a new instance of the GoodEntityProvider class.
        /// </summary>
        /// <param name="dataProvider">The resource data provider.</param>
        /// <param name="set">The database set.</param>
        /// <param name="save">The entity save handler.</param>
        public GoodEntityProvider(IAccountDataProvider dataProvider, DbSet<GoodEntity> set, Func<CancellationToken, Task<int>> save)
            : base(dataProvider, set, save) // Please make sure the entity provider contains a constructor with these parameters.
        {
        }

        /// <summary>
        /// Searches.
        /// </summary>
        /// <param name="q">The query arguments.</param>
        /// <param name="siteId"></param>
        /// <param name="cancellationToken">The optional token to monitor for cancellation requests.</param>
        /// <returns>A collection of entity.</returns>
        public Task<CollectionResult<GoodEntity>> SearchAsync(QueryArgs q, string siteId, CancellationToken cancellationToken)
        {
            // This is an additional method to extend search method.
            // You can add the new method to get the list with further conditions
            // by call SearchAsync(QueryData, CancellationToken)
            // filling a key-value pair query data which will be translated to the expression
            // in MapQuery(QueryPredication<TEntity>) member method.

            var query = q != null ? (QueryData)q : new QueryData();
            if (string.IsNullOrWhiteSpace(siteId)) query["site"] = siteId;
            return SearchAsync(query, cancellationToken);
        }

        /// <inheritdoc />
        public override async Task<ChangeMethodResult> SaveAsync(GoodEntity value, CancellationToken cancellationToken = default)
        {
            // This method is a demo about to override default entity save method.
            // We may validate the permission of the current user to test if it can save the entity.
            // And then call base.SaveAsync(TEntity, CancellationToken) member method if pass;
            // otherwise, return the error result.
            // For the example here, it requires a user logged in to save an entity.

            if (!CoreResources.IsUserSignedIn) return new ChangeMethodResult(ChangeErrorKinds.Unauthorized, "Requires authentication.");
            return await base.SaveAsync(value, cancellationToken);
        }

        /// <inheritdoc />
        protected override void MapQuery(QueryPredication<GoodEntity> predication)
        {
            // This method is used to map the properties from query data to entity.
            // You can bind the query key and expression to filter the data source.
            // The query key is from the query data
            // and the expression is just to filter the source collection.

            predication.AddForString("site", info => info.Source.Where(ele => ele.OwnerSiteId == info.Value));
        }
    }

    /// <summary>
    /// The HTTP client for goods.
    /// </summary>
    public class GoodEntityClient : HttpResourceEntityProvider<GoodEntity>
    {
        /// <summary>
        /// The relative path of the resource.
        /// </summary>
        public const string RELATIVE_PATH = "api/goods";

        /// <summary>
        /// Initializes a new instance of the GoodEntityClient class.
        /// </summary>
        /// <param name="client">The resource access client.</param>
        /// <param name="relativePath">The relative path.</param>
        public GoodEntityClient(HttpResourceAccessClient client)
            : base(client, RELATIVE_PATH)   // Please make sure the entity provider contains a constructor with these parameters.
        {
        }

        /// <summary>
        /// Initializes a new instance of the GoodEntityClient class.
        /// </summary>
        /// <param name="appKey">The app secret key for accessing API.</param>
        /// <param name="host">The host URI.</param>
        /// <param name="relativePath">The relative path.</param>
        public GoodEntityClient(AppAccessingKey appKey, Uri host)
            : base(appKey, host, RELATIVE_PATH)
        {
        }

        /// <summary>
        /// Searches.
        /// </summary>
        /// <param name="q">The query arguments.</param>
        /// <param name="siteId"></param>
        /// <param name="cancellationToken">The optional token to monitor for cancellation requests.</param>
        /// <returns>A collection of entity.</returns>
        public Task<CollectionResult<GoodEntity>> SearchAsync(QueryArgs q, string siteId, CancellationToken cancellationToken)
        {
            // Just map the parameters into a query arguments.
            // And then call base.SearchAsync(QueryData, CancellationToken) to get response asynchronized.

            var query = q != null ? (QueryData)q : new QueryData();
            if (string.IsNullOrWhiteSpace(siteId)) query["site"] = siteId;
            return SearchAsync(query, cancellationToken);
        }
    }
}
