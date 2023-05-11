using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TestWpfApp.Models;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using NLog;

namespace TestWpfApp.Utils.Exports
{
    internal class WordDataExporter : IDataExporter
    {
        private static readonly Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public string FileExtension => ".docx";

        public async Task ExportDataAsync(IEnumerable<Item> data, string filePath)
        {
            if (data == null)
            {
                logger.Error("there is no data for word export");
                throw new ArgumentNullException(nameof(data));
            }

            using (var document = WordprocessingDocument.Create(filePath + FileExtension, WordprocessingDocumentType.Document))
            {
                try
                {
                    logger.Info("word export started");

                    var mainPart = document.AddMainDocumentPart();
                    mainPart.Document = new Document();
                    var body = mainPart.Document.AppendChild(new Body());

                    foreach (var item in data)
                    {
                        var paragraph = body.AppendChild(new Paragraph());
                        paragraph.AppendChild(new Run(new Text(item.ToString())));
                    }

                    await Task.Run(() => document.SaveAs(filePath));
                    document.Close();

                    logger.Info("word export success");
                }
                catch (Exception ex)
                {
                    logger.Error("word export failed with error: " + ex.Message);
                }
                
            }
        }
    }
}
