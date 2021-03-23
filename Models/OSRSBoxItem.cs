using System;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace OSItemIndex.API.Models
{
    public class OSRSBoxItem
    {
        /// <summary>
        /// Unique OSRS item ID number.
        /// </summary>
        [JsonProperty("id", Required = Required.Always)]
        public int ID { get; set; }

        /// <summary>
        /// The name of the item.
        /// </summary>
        [JsonProperty("name", Required = Required.Always)]
        public string Name { get; set; }

        /// <summary>
        /// If the item is a duplicate.
        /// </summary>
        [JsonProperty("duplicate", Required = Required.Always)]
        public bool Duplicate { get; set; }

        /// <summary>
        /// If the item is noted.
        /// </summary>
        [JsonProperty("noted", Required = Required.Always)]
        public bool Noted { get; set; }

        /// <summary>
        /// If the item is a placeholder.
        /// </summary>
        [JsonProperty("placeholder", Required = Required.Always)]
        public bool Placeholder { get; set; }

        /// <summary>
        /// If the item is stackable (in inventory).
        /// </summary>
        [JsonProperty("stackable", Required = Required.Always)]
        public bool Stackable { get; set; }

        /// <summary>
        /// If the item is tradeable (only on GE).
        /// </summary>
        [JsonProperty("tradeable_on_ge", Required = Required.Always)]
        public bool TradeableOnGe { get; set; }

        /// <summary>
        /// The last time (UTC) the item was updated (in ISO8601 date format).
        /// </summary>
        [JsonProperty("last_updated", Required = Required.Always)]
        public DateTime LastUpdated { get; set; }

        /// <summary>
        /// OSRSBox item document.
        /// </summary>
        [Column(TypeName = "json")]
        public string Document { get; set; }
    }
}
