using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;

namespace Model.Services
{
    public class LeitorArquivos
    {
        public static List<List<string>> ReadExcel(IFormFile file)
        {
            List<List<string>> exel = new();
            
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (ExcelPackage package = new(file.OpenReadStream()))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                int colCount = worksheet.Dimension.End.Column;
                int rowCount = worksheet.Dimension.End.Row;

                for (int row = 1; row <= rowCount; row++)
                {
                    List<string> linha = new();
                    for (int col = 1; col <= colCount; col++)
                    {
                        var valor = worksheet.Cells[row, col].Value;
                        if (valor == null)
                            valor = " ";
                        linha.Add(valor.ToString());
                    }
                    exel.Add(linha);
                }
            }
            return exel;
            
        }
    }
}
