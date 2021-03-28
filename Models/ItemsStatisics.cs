using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace OSItemIndex.API.Models
{
    public class ItemsStatisics
    {
        /// <summary>
        /// Total item records in the dbSet
        /// </summary>
        [Required]
        [JsonPropertyName("total_item_records")]
        public int? TotalItemRecords { get; set; }
    }
}
