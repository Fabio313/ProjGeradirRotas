using System.Collections.Generic;
using APIUsuario.Utils;
using Model;
using MongoDB.Driver;

namespace APIUsuario.Services
{
    public class UsuarioService
    {
        private readonly IMongoCollection<Usuario> _usuario;

        public UsuarioService(IMongoDBSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _usuario = database.GetCollection<Usuario>(settings.PersonCollectionName);
        }

        public List<Usuario> Get() =>
            _usuario.Find(person => true).ToList();

        public Usuario GetLogin(string login, string senha) =>
            _usuario.Find(usuario => usuario.Login == login && usuario.Senha == senha).FirstOrDefault();

        public Usuario ForgetPassword(string login) =>
            _usuario.Find(usuario=> usuario.Login == login).FirstOrDefault();

        public Usuario Get(string id) =>
            _usuario.Find<Usuario>(cliente => cliente.Id == id).FirstOrDefault();

        public Usuario Create(Usuario cliente)
        {
            _usuario.InsertOne(cliente);
            return cliente;
        }

        public void Update(string id, Usuario clienteIn) =>
            _usuario.ReplaceOne(cliente => cliente.Id == id, clienteIn);

        public void Remove(Pessoa clienteIn) =>
            _usuario.DeleteOne(cliente => cliente.Id == clienteIn.Id);

        public void Remove(string id) =>
            _usuario.DeleteOne(cliente => cliente.Id == id);
    }
}
