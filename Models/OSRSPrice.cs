using Newtonsoft.Json;

namespace OSItemIndex.API.Models
{
    public class OSRSPrice
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

        public class PriceTrend
        {
            [JsonProperty("trend", Required = Required.Always)]
            public string Trend { get; set; }

            [JsonProperty("name", Required = Required.Always)]
            public string Name { get; set; }
        }
    }
}
