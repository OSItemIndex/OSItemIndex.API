﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace OSItemIndex.API.Models
{
    public class WeirdGloopPrice : PriceIdentity
    {
        /// <summary>
        /// The price of the item.
        /// </summary>
        [Required]
        [JsonPropertyName("price")]
        public long? Price { get; set; }

        /// <summary>
        /// The timestamp (UTC) the item was updated (in ISO8601 date format).
        /// </summary>
        [Required]
        [JsonPropertyName("timestamp")]
        public DateTime? Timestamp { get; set; }
    }
}
