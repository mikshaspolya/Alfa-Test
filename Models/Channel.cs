using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TestWpfApp.Models
{
    [XmlRoot("channel")]
    public class Channel
    {
        [XmlElement("item")]
        public ObservableCollection<Item> Items { get; set; }

        public Channel()
        {
            Items = new ObservableCollection<Item>();
        }

        public override string ToString()
        {
            string res = "";
            foreach (Item item in Items) { res += item; }
            return res;
        }
    }
}
