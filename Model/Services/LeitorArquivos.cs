using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClosedXML.Excel;
using OfficeOpenXml;

namespace Model.Services
{
    public class LeitorArquivos
    {
        public static List<Rotas> ReadExcel()
        {
            var rotas = new List<Rotas>();      
            //COLOCAR O DIRETORIO DO ARQUIVO AQUI
            FileInfo arquivoExiste = new(@"C:\Users\Fabio Z Ferrenha\Desktop\Atividades\GeradorDeRotas\Rotas.xlsx");
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (ExcelPackage package = new(arquivoExiste))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                int colCount = worksheet.Dimension.End.Column;
                int rowCount = worksheet.Dimension.End.Row;
                for (int row = 1; row <= rowCount; row++)
                {
                    var rota = new Rotas();

                    rota.Data          = worksheet.Cells[row, 1].Value.ToString();
                    rota.Stats         = worksheet.Cells[row, 2].Value.ToString();
                    rota.Auditado      = worksheet.Cells[row, 3].Value.ToString();
                    rota.CopReverteu   = worksheet.Cells[row, 4].Value.ToString();
                    rota.Log           = worksheet.Cells[row, 5].Value.ToString();
                    rota.Pdf           = worksheet.Cells[row, 6].Value.ToString();
                    rota.Foto          = worksheet.Cells[row, 7].Value.ToString();
                    rota.Contrato      = worksheet.Cells[row, 8].Value.ToString();
                    rota.Wo            = worksheet.Cells[row, 9].Value.ToString();
                    rota.Os            = worksheet.Cells[row, 10].Value.ToString();
                    rota.Assinante     = worksheet.Cells[row, 11].Value.ToString();
                    rota.Tecnicos      = worksheet.Cells[row, 12].Value.ToString();
                    rota.Login         = worksheet.Cells[row, 13].Value.ToString();
                    rota.Matricula     = worksheet.Cells[row, 14].Value.ToString();
                    rota.Cop           = worksheet.Cells[row, 15].Value.ToString();
                    rota.UltimoAlterar = worksheet.Cells[row, 16].Value.ToString();
                    rota.Local         = worksheet.Cells[row, 17].Value.ToString();
                    rota.PontoCasaApt  = worksheet.Cells[row, 18].Value.ToString();
                    rota.Cidade        = worksheet.Cells[row, 19].Value.ToString();
                    rota.Base          = worksheet.Cells[row, 20].Value.ToString();
                    rota.Horario       = worksheet.Cells[row, 21].Value.ToString();
                    rota.Segmento      = worksheet.Cells[row, 22].Value.ToString();
                    rota.Servico       = worksheet.Cells[row, 23].Value.ToString();
                    rota.TipoServico   = worksheet.Cells[row, 24].Value.ToString();
                    rota.TipoOs        = worksheet.Cells[row, 25].Value.ToString();
                    rota.GrupoServico  = worksheet.Cells[row, 26].Value.ToString();
                    rota.Endereco      = worksheet.Cells[row, 27].Value.ToString();
                    rota.Numero        = worksheet.Cells[row, 28].Value.ToString();
                    rota.Complemento   = worksheet.Cells[row, 29].Value.ToString();
                    rota.Cep           = worksheet.Cells[row, 30].Value.ToString();
                    rota.Node          = worksheet.Cells[row, 31].Value.ToString();
                    rota.Bairro        = worksheet.Cells[row, 32].Value.ToString();
                    rota.Pacote        = worksheet.Cells[row, 33].Value.ToString();
                    rota.Cod           = worksheet.Cells[row, 34].Value.ToString();
                    rota.Telefone1     = worksheet.Cells[row, 35].Value.ToString();
                    rota.Telefone2     = worksheet.Cells[row, 36].Value.ToString();
                    rota.Obs           = worksheet.Cells[row, 37].Value.ToString();
                    rota.ObsTecnico    = worksheet.Cells[row, 38].Value.ToString();
                    rota.Equipamento   = worksheet.Cells[row, 39].Value.ToString();
                    rotas.Add(rota);
                }
            }
            return rotas;
        }
    }
}
