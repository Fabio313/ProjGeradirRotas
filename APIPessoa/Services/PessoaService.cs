using System.Collections.Generic;
using APIPessoa.Utils;
using Model;
using MongoDB.Driver;

namespace APIPessoa.Services
{
    public class PessoaService
    {
        private readonly IMongoCollection<Pessoa> _pessoa;

        public PessoaService(IMongoDBSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _pessoa = database.GetCollection<Pessoa>(settings.PersonCollectionName);
        }

        public List<Pessoa> Get() =>
            _pessoa.Find(person => true).ToList();

        public List<Pessoa> GetDisponiveis()=>
            _pessoa.Find(person => person.Equipe == null).ToList();

        public List<Pessoa> GetPessoasTime(string idtime) =>
            _pessoa.Find(person => person.Equipe.Id == idtime).ToList();

        public Pessoa Get(string id) =>
            _pessoa.Find<Pessoa>(cliente => cliente.Id == id).FirstOrDefault();

        public Pessoa Create(Pessoa cliente)
        {
            _pessoa.InsertOne(cliente);
            return cliente;
        }

        public void Update(string id, Pessoa clienteIn) =>
            _pessoa.ReplaceOne(cliente => cliente.Id == id, clienteIn);

        public void Remove(Pessoa clienteIn) =>
            _pessoa.DeleteOne(cliente => cliente.Id == clienteIn.Id);

        public void Remove(string id) =>
            _pessoa.DeleteOne(cliente => cliente.Id == id);
    }
}
