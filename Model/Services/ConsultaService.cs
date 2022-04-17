using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Model.Services
{
    public class ConsultaService
    {
        private static readonly HttpClient APIConnection = new();
        private static HttpResponseMessage GetRestposta = new();

        public static async Task<List<Cidade>> GetCidades()
        {
            GetRestposta = await APIConnection.GetAsync("https://localhost:44398/api/Cidades");
            var passageiro = JsonConvert.DeserializeObject<List<Cidade>>(await GetRestposta.Content.ReadAsStringAsync());
            if (passageiro == null)
                return null;
            return passageiro;
        }
        public static async Task<Cidade> GetIdCidades(string id)
        {
            GetRestposta = await APIConnection.GetAsync("https://localhost:44398/api/Cidades/"+ id);
            var passageiro = JsonConvert.DeserializeObject<Cidade>(await GetRestposta.Content.ReadAsStringAsync());
            if (passageiro == null)
                return null;
            return passageiro;
        }
        public static void UpdateCidades(string id, Cidade cidade)
        {
            APIConnection.PutAsJsonAsync("https://localhost:44398/api/Cidades/" + id, cidade);
        }
        public static void DeleteCidades(string id)
        {
            APIConnection.DeleteAsync("https://localhost:44398/api/Cidades/" + id);
        }
        public static void CreateCidade(Cidade cidade)
        {
            APIConnection.PostAsJsonAsync("https://localhost:44398/api/Cidades", cidade);
        }

        public static async Task<List<Pessoa>> GetPessoas()
        {
            GetRestposta = await APIConnection.GetAsync("https://localhost:44358/api/Pessoas");
            var passageiro = JsonConvert.DeserializeObject<List<Pessoa>>(await GetRestposta.Content.ReadAsStringAsync());
            if (passageiro == null)
                return null;
            return passageiro;
        }
        public static async Task<List<Pessoa>> GetPessoasDisponiveis()
        {
            GetRestposta = await APIConnection.GetAsync("https://localhost:44358/api/Pessoas/Disponiveis");
            var passageiro = JsonConvert.DeserializeObject<List<Pessoa>>(await GetRestposta.Content.ReadAsStringAsync());
            if (passageiro == null)
                return null;
            return passageiro;
        }
        public static async Task<List<Pessoa>> GetPessoasTime(string idtime)
        {
            GetRestposta = await APIConnection.GetAsync("https://localhost:44358/api/Pessoas/PessoasTime?idtime="+idtime);
            var passageiro = JsonConvert.DeserializeObject<List<Pessoa>>(await GetRestposta.Content.ReadAsStringAsync());
            if (passageiro == null)
                return null;
            return passageiro;
        }
        public static async Task<Pessoa> GetIdPessoa(string id)
        {
            GetRestposta = await APIConnection.GetAsync("https://localhost:44358/api/Pessoas/" + id);
            var passageiro = JsonConvert.DeserializeObject<Pessoa>(await GetRestposta.Content.ReadAsStringAsync());
            if (passageiro == null)
                return null;
            return passageiro;
        }
        public static void UpdatePessoas(string id, Pessoa pessoa)
        {
            APIConnection.PutAsJsonAsync("https://localhost:44358/api/Pessoas/" + id, pessoa);
        }
        public static void DeletePessoas(string id)
        {
            APIConnection.DeleteAsync("https://localhost:44358/api/Pessoas/" + id);
        }
        public static void CreatePessoa(Pessoa pessoa)
        {
            APIConnection.PostAsJsonAsync("https://localhost:44358/api/Pessoas", pessoa);
        }

        public static async Task<List<Equipe>> GetEquipes()
        {
            GetRestposta = await APIConnection.GetAsync("https://localhost:44341/api/Equipes");
            var passageiro = JsonConvert.DeserializeObject<List<Equipe>>(await GetRestposta.Content.ReadAsStringAsync());
            if (passageiro == null)
                return null;
            return passageiro;
        }
        public static async Task<Equipe> GetIdEquipe(string id)
        {
            GetRestposta = await APIConnection.GetAsync("https://localhost:44341/api/Equipes/" + id);
            var passageiro = JsonConvert.DeserializeObject<Equipe>(await GetRestposta.Content.ReadAsStringAsync());
            if (passageiro == null)
                return null;
            return passageiro;
        }
        public static async Task<List<Equipe>> GetEquipesCidades(string id)
        {
            GetRestposta = await APIConnection.GetAsync("https://localhost:44341/api/Equipes/EquipesCidade?idcidade=" + id);
            var passageiro = JsonConvert.DeserializeObject<List<Equipe>>(await GetRestposta.Content.ReadAsStringAsync());
            if (passageiro == null)
                return null;
            return passageiro;
        }
        
        public static void UpdateEquipes(string id, Equipe equipe)
        {
            APIConnection.PutAsJsonAsync("https://localhost:44341/api/Equipes/" + id, equipe);
        }
        public static void DeleteEquipes(string id)
        {
            APIConnection.DeleteAsync("https://localhost:44341/api/Equipes/" + id);
        }
        public static void CreateEquipe(Equipe equipe)
        {
            APIConnection.PostAsJsonAsync("https://localhost:44341/api/Equipes", equipe);
        }
    }
}
