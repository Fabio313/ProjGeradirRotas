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
        public static string[,] ReadExcel(IFormFile file)
        {
             
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (ExcelPackage package = new(file.OpenReadStream()))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                int colCount = worksheet.Dimension.End.Column;
                int rowCount = worksheet.Dimension.End.Row;
                var rotas = new string[rowCount, colCount];

                for (int row = 1; row <= rowCount; row++)
                {
                    for (int col = 1; col <= colCount;col++)
                    {
                        rotas[row - 1,col - 1] = worksheet.Cells[row, col].Value.ToString();
                    }
                }
                return rotas;
            }
        }
    }
}
