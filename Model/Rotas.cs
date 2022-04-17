using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Rotas
    {
        [Key]
        public string Data { get; set; }
        public string Stats { get; set; }
        public string Auditado { get; set; }
        public string CopReverteu { get; set; }
        public string Log { get; set; }
        public string Pdf { get; set; }
        public string Foto { get; set; }
        public string Contrato { get; set; }
        public string Wo { get; set; }
        public string Os { get; set; }
        public string Assinante { get; set; }
        public string Tecnicos { get; set; }
        public string Login { get; set; }
        public string Matricula { get; set; }
        public string Cop { get; set; }
        public string UltimoAlterar { get; set; }
        public string Local { get; set; }
        public string PontoCasaApt { get; set; }
        public string Cidade { get; set; }
        public string Base { get; set; }
        public string Horario { get; set; }
        public string Segmento { get; set; }
        public string Servico { get; set; }
        public string TipoServico { get; set; }
        public string TipoOs { get; set; }
        public string GrupoServico { get; set; }
        public string Endereco { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Cep { get; set; }
        public string Node { get; set; }
        public string Bairro { get; set; }
        public string Pacote { get; set; }
        public string Cod { get; set; }
        public string Telefone1 { get; set; }
        public string Telefone2 { get; set; }
        public string Obs { get; set; }
        public string ObsTecnico { get; set; }
        public string Equipamento { get; set; }
    }
}
