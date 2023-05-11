using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWpfApp.Models;
using OfficeOpenXml;
using NLog;

namespace TestWpfApp.Utils.Exports
{
    internal class ExcelDataExporter : IDataExporter
    {
        private static readonly Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public string FileExtension => ".xlsx";

        public async Task ExportDataAsync(IEnumerable<Item> data, string fileName)
        {
            if (data == null)
            {
                logger.Error("there is no data for excel export");
                throw new ArgumentNullException(nameof(data));
            }

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var excelPackage = new ExcelPackage())
            {
                try
                {
                    logger.Info("excel export started");

                    var worksheet = excelPackage.Workbook.Worksheets.Add("Sheet1");
                    var properties = typeof(Item).GetProperties();

                    for (int i = 0; i < properties.Length; i++)
                    {
                        var property = properties[i];
                        worksheet.Cells[1, i + 1].Value = property.Name;
                    }

                    var rowIndex = 2;
                    foreach (var item in data)
                    {
                        for (int i = 0; i < properties.Length; i++)
                        {
                            var property = properties[i];
                            var value = property.GetValue(item);
                            worksheet.Cells[rowIndex, i + 1].Value = value;
                            worksheet.Cells[rowIndex, i + 1].Style.Numberformat.Format = "yyyy-MM-dd HH:mm:ss";
                        }
                        rowIndex++;
                    }

                    using (var fileStream = new FileStream(fileName + FileExtension, FileMode.Create))
                    {
                        await excelPackage.SaveAsAsync(fileStream);
                    }

                    logger.Info("excel export success");
                }
                catch (Exception ex) 
                {
                    logger.Error("excel export failed with error: " + ex.Message);
                }               
            }
        }
    }
}
