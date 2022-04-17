using System.Collections.Generic;
using APIEquipe.Utils;
using Model;
using MongoDB.Driver;

namespace APIEquipe.Services
{
    public class EquipeService
    {
        private readonly IMongoCollection<Equipe> _equipe;

        public EquipeService(IMongoDBSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _equipe = database.GetCollection<Equipe>(settings.PersonCollectionName);
        }

        public List<Equipe> Get() =>
            _equipe.Find(person => true).ToList();

        public Equipe Get(string id) =>
            _equipe.Find<Equipe>(cliente => cliente.Id == id).FirstOrDefault();

        public List<Equipe> GetEquipesCidade(string idcidade) =>
            _equipe.Find(person => person.Cidade.Id == idcidade).ToList();

        public Equipe Create(Equipe cliente)
        {
            _equipe.InsertOne(cliente);
            return cliente;
        }

        public void Update(string id, Equipe clienteIn) =>
            _equipe.ReplaceOne(cliente => cliente.Id == id, clienteIn);

        public void Remove(Pessoa clienteIn) =>
            _equipe.DeleteOne(cliente => cliente.Id == clienteIn.Id);

        public void Remove(string id) =>
            _equipe.DeleteOne(cliente => cliente.Id == id);
    }
}
