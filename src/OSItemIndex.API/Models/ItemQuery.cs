using System.Text.Json.Serialization;

namespace OSItemIndex.API.Models
{
    public class ItemQuery
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("looseCompare")]
        public bool? LooseCompare { get; set; }

        [JsonPropertyName("isDuplicate")]
        public bool? IsDuplicate { get; set; }

        [JsonPropertyName("isNoted")]
        public bool? IsNoted { get; set; }

        [JsonPropertyName("isPlaceholder")]
        public bool? IsPlaceholder { get; set; }

        [JsonPropertyName("isStackable")]
        public bool? IsStackable { get; set; }

        [JsonPropertyName("isTradeableOnGe")]
        public bool? IsTradeableOnGe { get; set; }
    }
}
