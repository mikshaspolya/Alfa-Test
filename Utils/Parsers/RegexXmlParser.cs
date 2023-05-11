using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using TestWpfApp.Models;
using NLog;

namespace TestWpfApp.Utils
{
    public class RegexXmlParser : IParser
    {
        private static readonly Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private const string ItemPattern = @"<item>[\s\S]*?<title>(?<title>[\s\S]*?)<\/title>[\s\S]*?<link>(?<link>[\s\S]*?)<\/link>[\s\S]*?<description>(?<description>[\s\S]*?)<\/description>[\s\S]*?<category>(?<category>[\s\S]*?)<\/category>[\s\S]*?<pubDate>(?<pubDate>[\s\S]*?)<\/pubDate>[\s\S]*?<\/item>";
        private static readonly Regex ItemRegex = new Regex(ItemPattern, RegexOptions.Compiled);

        public string Name => "Regex";

        public async Task<ObservableCollection<Item>> ParseAsync(string filePath)
        {
            try
            {
                logger.Info("regex parsing from file " + filePath);

                ObservableCollection<Item> items = new ObservableCollection<Item>();
                var xmlContent = "";
                await Task.Run(() => xmlContent = File.ReadAllText(filePath));
                var matches = ItemRegex.Matches(xmlContent);

                foreach (Match match in matches)
                {
                    items.Add(
                        new Item
                        {
                            Title = match.Groups["title"].Value.Trim(),
                            Link = match.Groups["link"].Value.Trim(),
                            Description = match.Groups["description"].Value.Trim(),
                            Category = match.Groups["category"].Value.Trim(),
                            PubDate = DateTime.Parse(match.Groups["pubDate"].Value.Trim()),
                        }
                    );
                }

                logger.Info("regex parsing success");
                return items;
            }
            catch (Exception ex)
            {
                logger.Error($"Error regex parsing XML from {filePath}: {ex.Message}");
                return new ObservableCollection<Item>();
            }

        }
    }
}
