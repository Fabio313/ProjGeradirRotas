namespace APIPessoa.Utils
{
    public interface IMongoDBSettings
    {
        string PersonCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
