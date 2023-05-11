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
        public string Title { get; set; }
        public string Link { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public DateTime PubDate { get; set; }

        public override string ToString()
        {
            string formattedDate = PubDate.ToString("ddd, dd MMM yyyy HH:mm:ss");
            return $"Заголовок: {Title}\n Ссылка: {Link}\n Описание: {Description}\n Категория: {Category}\n Дата публикации: {formattedDate}\n";
        }
    }
}
