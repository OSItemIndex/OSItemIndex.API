namespace OSItemIndex.API.Data
{
    public class DatabaseOptions
    {
        public string DbConnectionString { get; set; }
        public int PoolSize { get; set; } = 128;
    }
}
