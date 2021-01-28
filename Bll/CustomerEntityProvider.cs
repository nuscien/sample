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
    /// The data provider for customers.
    /// </summary>
    public class CustomerEntityProvider : OnPremisesResourceEntityProvider<CustomerEntity>
    {
        /// <summary>
        /// Initializes a new instance of the CustomerEntityProvider class.
        /// </summary>
        /// <param name="client">The resource access client.</param>
        /// <param name="set">The database set.</param>
        /// <param name="save">The entity save handler.</param>
        public CustomerEntityProvider(OnPremisesResourceAccessClient client, DbSet<CustomerEntity> set, Func<CancellationToken, Task<int>> save)
            : base(client, set, save) // Please make sure the entity provider contains a constructor with these parameters.
        {
        }

        /// <summary>
        /// Initializes a new instance of the CustomerEntityProvider class.
        /// </summary>
        /// <param name="dataProvider">The resource data provider.</param>
        /// <param name="set">The database set.</param>
        /// <param name="save">The entity save handler.</param>
        public CustomerEntityProvider(IAccountDataProvider dataProvider, DbSet<CustomerEntity> set, Func<CancellationToken, Task<int>> save)
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
        public Task<CollectionResult<CustomerEntity>> SearchAsync(QueryArgs q, string siteId, CancellationToken cancellationToken)
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
        protected override void MapQuery(QueryPredication<CustomerEntity> predication)
        {
            // This method is used to map the properties from query data to entity.
            // You can bind the query key and expression to filter the data source.
            // The query key is from the query data
            // and the expression is just to filter the source collection.

            predication.AddForString("site", info => info.Source.Where(ele => ele.OwnerSiteId == info.Value));
            predication.AddForString("phone", info => info.Source.Where(ele => ele.PhoneNumber == info.Value));
            predication.AddForString("addr", info => info.Source.Where(ele => ele.Address != null && ele.Address.Contains(info.Value)));
        }
    }

    /// <summary>
    /// The HTTP client for customers.
    /// </summary>
    public class CustomerEntityClient : HttpResourceEntityProvider<CustomerEntity>
    {
        /// <summary>
        /// The relative path of the resource.
        /// </summary>
        public const string RELATIVE_PATH = "api/customers";

        /// <summary>
        /// Initializes a new instance of the CustomerEntityClient class.
        /// </summary>
        /// <param name="client">The resource access client.</param>
        /// <param name="relativePath">The relative path.</param>
        public CustomerEntityClient(HttpResourceAccessClient client)
            : base(client, RELATIVE_PATH)   // Please make sure the entity provider contains a constructor with these parameters.
        {
        }

        /// <summary>
        /// Initializes a new instance of the CustomerEntityClient class.
        /// </summary>
        /// <param name="appKey">The app secret key for accessing API.</param>
        /// <param name="host">The host URI.</param>
        /// <param name="relativePath">The relative path.</param>
        public CustomerEntityClient(AppAccessingKey appKey, Uri host)
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
        public Task<CollectionResult<CustomerEntity>> SearchAsync(QueryArgs q, string siteId, CancellationToken cancellationToken)
        {
            var query = q != null ? (QueryData)q : new QueryData();
            if (string.IsNullOrWhiteSpace(siteId)) query["site"] = siteId;
            return SearchAsync(query, cancellationToken);
        }
    }
}
