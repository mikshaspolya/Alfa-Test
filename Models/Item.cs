using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TestWpfApp.Models
{
    public class Item
    {
        [XmlElement("title")]
        public string Title { get; set; }
        [XmlElement("link")]
        public string Link { get; set; }
        [XmlElement("description")]
        public string Description { get; set; }
        [XmlElement("category")]
        public string Category { get; set; }
        [XmlElement("pubDate")]
        public DateTime PubDate { get; set; }

        public override string ToString()
        {
            string formattedDate = PubDate.ToString("ddd, dd MMM yyyy HH:mm:ss");
            return $"title: {Title}\nlink: {Link}\ndescription: {Description}\ncategory: {Category}\npubdate: {formattedDate}\n";
        }
    }
}
