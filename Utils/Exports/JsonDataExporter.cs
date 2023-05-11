using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWpfApp.Models;
using Newtonsoft.Json;

namespace TestWpfApp.Utils.Exports
{
    internal class JsonDataExporter : IDataExporter
    {
        public string FileExtension => throw new NotImplementedException();

        public async Task ExportDataAsync(IEnumerable<Item> data, string fileName)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            var json = JsonConvert.SerializeObject(data, Formatting.Indented);

            using (var streamWriter = new StreamWriter(fileName + FileExtension))
            {
                await streamWriter.WriteAsync(json);
            }
        }
    }
}
