﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace OSItemIndex.API.Models
{
    public class WikiRealtimePrice : PriceIdentity
    {
        /// <summary>
        /// 
        /// </summary>
        [Required]
        [Column(TypeName = "json")]
        [JsonPropertyName("latest")]
        public LatestPrice Latest { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        [Column(TypeName = "json")]
        [JsonPropertyName("5m")]
        public AveragePrice FiveMinute { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        [Column(TypeName = "json")]
        [JsonPropertyName("1h")]
        public AveragePrice OneHour { get; set; }

        //[NotMapped]
        public class LatestPrice : IWikiRealtimePricingModel
        {
            /// <summary>
            /// Unique OSRS item ID number.
            /// </summary>
            [NotMapped] // ignored by efcore
            [JsonPropertyName("id")]
            public int? Id { get; set; }

            /// <summary>
            /// 
            /// </summary>
            [Required]
            [JsonPropertyName("high")]
            public int? High { get; set; }

            /// <summary>
            /// 
            /// </summary>
            [Required]
            [JsonPropertyName("highTime")]
            public long? HighTime { get; set; }

            /// <summary>
            /// 
            /// </summary>
            [Required]
            [JsonPropertyName("low")]
            public int? Low { get; set; }

            /// <summary>
            /// 
            /// </summary>
            [Required]
            [JsonPropertyName("lowTime")]
            public long? LowTime { get; set; }
        }

        //[NotMapped]
        public class AveragePrice : IWikiRealtimePricingModel
        {
            /// <summary>
            /// Unique OSRS item ID number.
            /// </summary>
            [NotMapped] // ignored by efcore
            [JsonPropertyName("id")]
            public int? Id { get; set; }

            /// <summary>
            /// 
            /// </summary>
            [Required]
            [JsonPropertyName("avgHighPrice")]
            public int? AverageHighPrice { get; set; }

            /// <summary>
            /// 
            /// </summary>
            [Required]
            [JsonPropertyName("highPriceVolume")]
            public int? HighPriceVolume { get; set; }

            /// <summary>
            /// 
            /// </summary>
            [Required]
            [JsonPropertyName("avgLowPrice")]
            public int? AverageLowPrice { get; set; }

            /// <summary>
            /// 
            /// </summary>
            [Required]
            [JsonPropertyName("lowPriceVolume")]
            public int? LowPriceVolume { get; set; }

            /// <summary>
            /// 
            /// </summary>
            [Required]
            [JsonPropertyName("timestamp")]
            public long? Timestamp { get; set; }
        }
    }
}
