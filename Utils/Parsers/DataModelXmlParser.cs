using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TestWpfApp.Models;

namespace TestWpfApp.Utils
{
    public class DataModelXmlParser : IParser
    {
        private static readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public string Name => "DataModel";

        public async Task<ObservableCollection<Item>> ParseAsync(string filePath)
        {
            try
            {
                logger.Info("data model parsing from file " + filePath);
                XmlDocument xmlDocument = new XmlDocument();
                await Task.Run(() => xmlDocument.Load(filePath));

                ObservableCollection<Item> items = new ObservableCollection<Item>();

                XmlNodeList itemList = xmlDocument.GetElementsByTagName("item");
                foreach (XmlNode itemNode in itemList)
                {
                    items.Add(
                        new Item
                        {
                            Title = itemNode.SelectSingleNode("title").InnerText.Trim(),
                            Link = itemNode.SelectSingleNode("link").InnerText.Trim(),
                            Description = itemNode.SelectSingleNode("description").InnerText.Trim(),
                            Category = itemNode.SelectSingleNode("category").InnerText.Trim(),
                            PubDate = DateTime.Parse(itemNode.SelectSingleNode("pubDate").InnerText.Trim()),
                        }
                    );
                }

                logger.Info("data model parsing success");
                return items;
            }
            catch (Exception ex)
            {
                logger.Error($"Error parsing XML from {filePath}: {ex.Message}");
                return new ObservableCollection<Item>();
            }
        }

       
        //public static ObservableCollection<Item> ParseXml(string filePath)
        //{
        //    XmlDocument xmlDocument = new XmlDocument();
        //    xmlDocument.Load(filePath);

        //    ObservableCollection<Item> items = new ObservableCollection<Item>();

        //    XmlNodeList itemList = xmlDocument.GetElementsByTagName("item");
        //    foreach (XmlNode itemNode in itemList)
        //    {
        //        items.Add(
        //            new Item
        //            {
        //                Title = itemNode.SelectSingleNode("title").InnerText,
        //                Link = itemNode.SelectSingleNode("link").InnerText,
        //                Description = itemNode.SelectSingleNode("description").InnerText,
        //                Category = itemNode.SelectSingleNode("category").InnerText,
        //                PubDate = DateTime.Parse(itemNode.SelectSingleNode("pubDate").InnerText),
        //            }
        //        );
        //    }

        //    return items;
        //}
    }
}
