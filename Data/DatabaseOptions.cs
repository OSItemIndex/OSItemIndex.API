namespace OSItemIndex.API
{
    public class DatabaseOptions
    {
        public string DbConnectionString { get; set; }
        public int PoolSize { get; set; } = 128;
    }
}
