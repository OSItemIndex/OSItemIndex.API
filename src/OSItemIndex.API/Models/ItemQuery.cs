namespace OSItemIndex.API.Models
{
    public class ItemQuery
    {
        public string Name { get; set; }
        public bool? LooseCompare { get; set; }
        public bool? Duplicate { get; set; }
        public bool? Noted { get; set; }
        public bool? Placeholder { get; set; }
        public bool? Stackable { get; set; }
        public bool? TradeableOnGe { get; set; }
    }
}
