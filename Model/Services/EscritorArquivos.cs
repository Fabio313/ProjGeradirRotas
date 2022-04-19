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
        public static void EscreveDocx(List<Equipe> equipesRota, string[,] rotas, Cidade cidade, string servico, List<string> colunas)
        {
            int[] indexCol = new int[colunas.Count];
            int k = 0;
            foreach (string coluna in colunas)
            {
                
                for(int j = 0;j<rotas.GetLength(1);j++)
                {
                    if (rotas[0, j] == coluna)
                        indexCol[k] = j;
                }
                k++;
            }
            int indicegeral = 1;

            var divisao = (rotas.GetLength(0) - 1) / equipesRota.Count;
            using (StreamWriter sw = new($@"C:\Users\Fabio Z Ferrenha\Desktop\Atividades\GeradorDeRotas\Rota-{DateTime.Now.ToString("dd-MM-yyyy")}.docx"))
            {
                sw.WriteLine($"{servico} - {DateTime.Now.ToString("dd/MM/yyyy")}\n{cidade.Nome}\n\n");//Titulo
                foreach (Equipe equipe in equipesRota)
                {
                    sw.WriteLine("Equipe: " + equipe.Nome + "\nRotas:");//Listar as rotas de cada equipe
                    for (int i = 0; i < divisao; i++)
                    {
                        foreach(int index in indexCol)
                        {
                            sw.WriteLine($"{rotas[0, index]}: {rotas[i+indicegeral, index]}");
                        }
                        if (i > divisao)
                            indicegeral = i;
                        sw.WriteLine("\n");
                    }
                    sw.WriteLine("--------------------------------------------------------------");
                }
            }
        }
    }
}
