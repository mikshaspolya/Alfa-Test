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

namespace TestWpfApp.Utils.Exports
{
    internal class WordDataExporter : IDataExporter
    {
        public string FileExtension => ".docx";

        public async Task ExportDataAsync(IEnumerable<Item> data, string filePath)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            using (var document = WordprocessingDocument.Create(filePath + FileExtension, WordprocessingDocumentType.Document))
            {
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
            }
        }
    }
}
