using System;
using Newtonsoft.Json;

namespace OSItemIndex.API.Models
{
    public class RSBuddyPrice : IPriceModel
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
        /// Store price.
        /// </summary>
        [JsonProperty("sp", Required = Required.Always)]
        public int Sp { get; set; }

        /// <summary>
        /// Average buy price.
        /// </summary>
        [JsonProperty("buy_average", Required = Required.Always)]
        public int BuyAverage { get; set; }

        /// <summary>
        /// Average buy quantity.
        /// </summary>
        [JsonProperty("buy_quantity", Required = Required.Always)]
        public int BuyQuantity { get; set; }

        /// <summary>
        /// Average sell price.
        /// </summary>
        [JsonProperty("sell_average", Required = Required.Always)]
        public int SellAverage { get; set; }

        /// <summary>
        /// Average sell quantity.
        /// </summary>
        [JsonProperty("sell_quantity", Required = Required.Always)]
        public int SellQuantity { get; set; }

        /// <summary>
        /// Average overall price.
        /// </summary>
        [JsonProperty("overall_average", Required = Required.Always)]
        public int OverallAverage { get; set; }

        /// <summary>
        /// Average overall quantity.
        /// </summary>
        [JsonProperty("sell_quantity", Required = Required.Always)]
        public int OverallQuantity { get; set; }

        /// <summary>
        /// The timestamp (UTC) the item was updated (in ISO8601 date format).
        /// </summary>
        [JsonProperty("timestamp", Required = Required.Always)]
        public DateTime Timestamp { get; set; }
    }
}
