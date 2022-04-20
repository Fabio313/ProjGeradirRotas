namespace APIUsuario.Utils
{
    public class MongoDBSettings : IMongoDBSettings
    {
        public string PersonCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
