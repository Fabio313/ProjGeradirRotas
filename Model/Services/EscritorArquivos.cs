using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Services
{
    public class EscritorArquivos
    {
        public static void EscreveDocx(List<Equipe> equipesRota, List<Rotas> rotas)
        {
            int rota = 0;
            var divisao = rotas.Count / equipesRota.Count;
            using (StreamWriter sw = new StreamWriter(@"C:\Users\Fabio Z Ferrenha\Desktop\Atividades\GeradorDeRotas\CDriveDirs.txt"))
            {
               foreach(Equipe equipe in equipesRota)
               {
                    sw.WriteLine("Equipe: "+equipe.Nome);
                    for(int i = 0;i<divisao;i++)
                    {

                    }
               }
            }
        }
    }
}
