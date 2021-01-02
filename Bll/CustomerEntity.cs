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

using Trivial.Data;
using Trivial.Reflection;
using Trivial.Text;
using Trivial.Net;
using NuScien.Data;
using NuScien.Security;

namespace NuScien.Sample
{
    public class CustomerEntity : BaseResourceEntity
    {
        #region Constructors

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        [JsonPropertyName("address")]
        [Column("address")]
        public string Address
        {
            get => GetCurrentProperty<string>();
            set => SetCurrentProperty(value);
        }

        /// <summary>
        /// Gets or sets the phone number.
        /// </summary>
        [JsonPropertyName("phone")]
        [Column("phone")]
        public string PhoneNumber
        {
            get => GetCurrentProperty<string>();
            set => SetCurrentProperty(value);
        }

        /// <summary>
        /// Gets or sets the identifier of the owner site.
        /// </summary>
        [JsonPropertyName("site")]
        [Column("site")]
        public string SiteId
        {
            get => GetCurrentProperty<string>();
            set => SetCurrentProperty(value);
        }

        #endregion

        #region Member methods

        #endregion

        #region Static methods

        /// <summary>
        /// Searches.
        /// </summary>
        /// <param name="name">The name to search.</param>
        /// <param name="like">true if search by like; otherwise, false, to equals to.</param>
        /// <param name="state">The state.</param>
        /// <returns>A collection.</returns>
        public static IEnumerable<CustomerEntity> Search(string name, bool like = false, ResourceEntityStates state = ResourceEntityStates.Normal)
        {
            var context = Internals.BusinessDbContext.Create(true);
            return context.Customers.ListEntities(name, like, state);
        }

        /// <summary>
        /// Searches.
        /// </summary>
        /// <param name="name">The name to search.</param>
        /// <returns>A collection.</returns>
        public static IEnumerable<CustomerEntity> Search(QueryArgs q)
        {
            var context = Internals.BusinessDbContext.Create(true);
            return context.Customers.ListEntities(q);
        }

        /// <summary>
        /// Gets by identifier.
        /// </summary>
        /// <param name="id">The identifier of the entity to get.</param>
        /// <returns>The entity.</returns>
        public static Task<CustomerEntity> GetById(string id, bool includeAllStates = false)
        {
            var context = Internals.BusinessDbContext.Create(true);
            return context.Customers.GetByIdAsync(id, includeAllStates);
        }

        /// <summary>
        /// Saves.
        /// </summary>
        /// <param name="value">The entity to add or update.</param>
        /// <param name="cancellationToken">The optional token to monitor for cancellation requests.</param>
        /// <returns>The change method.</returns>
        /// <example>
        /// var client = ResourceAccessClients.CreateAsync();
        /// // client.LoginAsync(...);
        /// var entity = CustomerEntity.GetById("abcdefg") ?? new CustomerEntity
        /// {
        ///     Name = "abcdefg",
        ///     State = ResourceEntityStates.Normal
        /// };
        /// await CustomerEntity.SaveAsync(client, entity);
        /// </example>
        public static async Task<ChangeMethods> SaveAsync(BaseResourceAccessClient client, CustomerEntity value, CancellationToken cancellationToken = default)
        {
            if (value.IsNew
                ? string.IsNullOrWhiteSpace(value.SiteId)   // Property check for new entity.
                : !await client.HasPermissionAsync(value.SiteId, "sample-customer-management")) // Permission check.
                return ChangeMethods.Invalid;
            var context = Internals.BusinessDbContext.Create(false);
            return await DbResourceEntityExtensions.SaveAsync(context.Customers, context.SaveChangesAsync, value, cancellationToken);
        }

        #endregion
    }
}
