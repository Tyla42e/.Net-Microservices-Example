namespace Play.Common.Settings
{
    public class MongoDBSettings
    {
        public required string Host { get; init; }

        public required string Port { get; init; }

        public string ConnectionString => $"mongodb://{Host}:{Port}";
    }
}