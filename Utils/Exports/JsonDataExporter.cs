using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWpfApp.Models;
using Newtonsoft.Json;
using NLog;

namespace TestWpfApp.Utils.Exports
{
    internal class JsonDataExporter : IDataExporter
    {
        private static readonly Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public string FileExtension => ".json";

        public async Task ExportDataAsync(IEnumerable<Item> data, string fileName)
        {
            if (data == null)
            {
                logger.Error("there is no data for json export");
                throw new ArgumentNullException(nameof(data));
            }

            try
            {
                logger.Info("json export started");

                var json = JsonConvert.SerializeObject(data, Formatting.Indented);

                using (var streamWriter = new StreamWriter(fileName + FileExtension))
                {
                    await streamWriter.WriteAsync(json);
                }

                logger.Info("json export success");
            }
            catch (Exception ex)
            {
                logger.Error("json export failed with error: " + ex.Message);
            }
            
        }
    }
}
