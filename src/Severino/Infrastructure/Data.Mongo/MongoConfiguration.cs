namespace Severino.Infrastructure.Data.Mongo
{
    public sealed class MongoConfiguration
    {
        public string? Database { get; set; }

        public string? Protocol { get; set; }

        public string? Host { get; set; }

        public int? Port { get; set; }

        public string? Username { get; set; }

        public string? Password { get; set; }

        public string Uri => $"{this.Protocol}://{this.Username}:{this.Password}@{this.Host}:{this.Port}";
    }
}
