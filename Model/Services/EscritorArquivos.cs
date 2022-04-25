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
        public static string EscreveDocx(List<Equipe> equipesRota, List<List<string>> rotas, Cidade cidade, string servico, List<string> colunas, string path)
        {
            string PathFile = path + "//file//Rota-"+DateTime.Now.ToString("dd - MM - yyyy")+"["+cidade.Nome+","+servico+"].docx";

            //pegar qual coluna possui o nome definido
            int colnumero = rotas[0].FindIndex(coluna => coluna == "NUMERO");
            int colcomplemento = rotas[0].FindIndex(coluna => coluna == "COMPLEMENTO");
            int colbairro = rotas[0].FindIndex(coluna => coluna == "BAIRRO");
            int colendereco = rotas[0].FindIndex(coluna => coluna == "ENDEREÇO");
            int colservico = rotas[0].FindIndex(coluna => coluna == "SERVIÇO");
            int colcidade = rotas[0].FindIndex(coluna => coluna == "CIDADE");
            int colcep = rotas[0].FindIndex(coluna => coluna == "CEP");

            bool getend = false;
            string aux="";
            foreach(string indice in colunas)
            {
                if (int.Parse(indice) == colendereco)
                {
                    getend = true;
                    aux = indice;
                    
                }
            }
            if(getend==true)
            {
                colunas.Remove(aux);
                colunas.Add(aux);
                colunas.Add(colnumero.ToString());
                colunas.Add(colbairro.ToString());
                colunas.Add(colcomplemento.ToString());
                colunas.Add(colcep.ToString());
            }

            //deixando apenas o que foi filtrado
            var numcount = rotas.Count;
            var allColumns = rotas[0];
            for (int i = 0; i < numcount; i++)
            {
                rotas.Remove(rotas.Find(rota => rota[colcidade].ToLower() != cidade.Nome.ToLower()));
                rotas.Remove(rotas.Find(rota => rota[colservico].ToLower()
                                                                .Replace("ç", "c")
                                                                .Replace("ã", "a") != servico.ToLower()
                                                                                             .Replace("ç", "c")
                                                                                             .Replace("ã", "a")));
            }

            //ordena por cep
            var rotasordenadas = rotas.OrderBy(x => x[colcep]).ToList();

            int indicegeral = 0;

            var divisao = rotasordenadas.Count / equipesRota.Count;
            var divisaoresto = rotasordenadas.Count % equipesRota.Count;
            using (StreamWriter sw = new(PathFile))
            {
                sw.WriteLine($"{servico} - {DateTime.Now.ToString("dd/MM/yyyy")}\n{cidade.Nome}\n\n");//Titulo
                foreach (Equipe equipe in equipesRota)
                {
                    sw.WriteLine("Equipe: " + equipe.Nome + "\nRotas:\n");
                    //Listar as rotas de cada equipe
                    for (int i = 0; i < divisao; i++)
                    {
                        if (i == 0 && divisaoresto > 0)
                        {
                            divisao++;
                        }
                        if(i == 0)
                        {
                            divisaoresto--;
                        }
                        //Listar as colunas escolhidas
                        foreach (var index in colunas)
                        {
                            sw.WriteLine($"{allColumns[int.Parse(index)]}: {rotasordenadas[i+indicegeral][int.Parse(index)]}");
                        }
                        if ((i + 1) >= divisao)
                            indicegeral = i+indicegeral+1;
                        sw.WriteLine("\n");
                    }

                    if(divisaoresto >= 0)
                        divisao--;
                    sw.WriteLine("--------------------------------------------------------------");
                }
            }
            return PathFile;
        }
    }
}
