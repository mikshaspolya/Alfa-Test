using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWpfApp.Models;

namespace TestWpfApp.Utils
{
    public interface IParser
    {
        string Name { get; }
        Task<ObservableCollection<Item>> ParseAsync(string filePath);
    }
}
