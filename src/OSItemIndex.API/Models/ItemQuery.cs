namespace OSItemIndex.API.Models
{
    public class ItemQuery
    {
        /// <summary>
        ///
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///
        /// </summary>
        public bool? LooseCompare { get; set; }

        /// <summary>
        ///
        /// </summary>
        public bool? Duplicate { get; set; }

        /// <summary>
        ///
        /// </summary>
        public bool? Noted { get; set; }

        /// <summary>
        ///
        /// </summary>
        public bool? Placeholder { get; set; }

        /// <summary>
        ///
        /// </summary>
        public bool? Stackable { get; set; }

        /// <summary>
        ///
        /// </summary>
        public bool? TradeableOnGe { get; set; }
    }
}
