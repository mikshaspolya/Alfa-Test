using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWpfApp.Models;

namespace TestWpfApp.Utils.Exports
{
    public interface IDataExporter
    {
        string FileExtension { get; } 
        Task ExportDataAsync(IEnumerable<Item> data, string filePath); 
    }
}
