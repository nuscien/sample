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
    /// The good entity.
    /// </summary>
    [Table("testgoods")]
    public class GoodEntity : SiteOwnedResourceEntity
    {
        #region Constructors

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the image.
        /// </summary>
        [JsonPropertyName("image")]
        [Column("image")]
        public string Image
        {
            get => GetCurrentProperty<string>();
            set => SetCurrentProperty(value);
        }

        #endregion

        #region Member methods

        #endregion

        #region Static methods

        #endregion
    }
}
