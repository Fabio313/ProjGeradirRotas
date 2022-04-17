using System.Collections.Generic;
using APICidade.Utils;
using Model;
using MongoDB.Driver;

namespace APICidade.Services
{
    public class CidadeService
    {
        private readonly IMongoCollection<Cidade> _equipe;

        public CidadeService(IMongoDBSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _equipe = database.GetCollection<Cidade>(settings.PersonCollectionName);
        }

        public List<Cidade> Get() =>
            _equipe.Find(person => true).ToList();

        public Cidade Get(string id) =>
            _equipe.Find<Cidade>(cliente => cliente.Id == id).FirstOrDefault();

        public Cidade Create(Cidade cliente)
        {
            _equipe.InsertOne(cliente);
            return cliente;
        }

        public void Update(string id, Cidade clienteIn) =>
            _equipe.ReplaceOne(cliente => cliente.Id == id, clienteIn);

        public void Remove(Pessoa clienteIn) =>
            _equipe.DeleteOne(cliente => cliente.Id == clienteIn.Id);

        public void Remove(string id) =>
            _equipe.DeleteOne(cliente => cliente.Id == id);
    }
}
