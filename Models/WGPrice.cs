using System;
using Newtonsoft.Json;

namespace OSItemIndex.API.Models
{
    public class WGPrice
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
        /// The price of the item.
        /// </summary>
        [JsonProperty("price", Required = Required.Always)]
        public long Price { get; set; }

        /// <summary>
        /// The timestamp (UTC) the item was updated (in ISO8601 date format).
        /// </summary>
        [JsonProperty("timestamp", Required = Required.Always)]
        public DateTime Timestamp { get; set; }
    }
}
