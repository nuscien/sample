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

        #endregion

        #region Member methods

        #endregion

        #region Static methods

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
        public static Task<ChangeMethods> SaveAsync(CustomerEntity value, CancellationToken cancellationToken = default)
        {
            var context = Internals.BusinessDbContext.Create(false);
            return DbResourceEntityExtensions.SaveAsync(context.Customers, context.SaveChangesAsync, value, cancellationToken);
        }

        #endregion
    }
}
