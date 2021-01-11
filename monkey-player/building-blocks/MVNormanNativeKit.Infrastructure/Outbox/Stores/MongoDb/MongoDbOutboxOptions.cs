namespace MVNormanNativeKit.Infrastructure.Outbox.Stores.MongoDb
{
    public class MongoDbOutboxOptions : OutboxOptions
    {
        public string CollectionName { get; } = "Messages";
        public string ConnectionString { get; set; }
        public string DatabaseName { get; } = "OutboxDb";
    }
}
