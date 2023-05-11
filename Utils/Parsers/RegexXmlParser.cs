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

namespace TestWpfApp.Utils
{
    public class RegexXmlParser : IParser
    {
        private const string ItemPattern = @"<item>[\s\S]*?<title>(?<title>[\s\S]*?)<\/title>[\s\S]*?<link>(?<link>[\s\S]*?)<\/link>[\s\S]*?<description>(?<description>[\s\S]*?)<\/description>[\s\S]*?<category>(?<category>[\s\S]*?)<\/category>[\s\S]*?<pubDate>(?<pubDate>[\s\S]*?)<\/pubDate>[\s\S]*?<\/item>";
        private static readonly Regex ItemRegex = new Regex(ItemPattern, RegexOptions.Compiled);

        public string Name => "Regex";

        public async Task<ObservableCollection<Item>> ParseAsync(string filePath)
        {
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

            return items;
        }



        //public static ObservableCollection<Item> ParseXml(string filePath)
        //{
        //    var xml = File.ReadAllText(filePath);
        //    ObservableCollection<Item> items = new ObservableCollection<Item>();

        //    var itemRegex = new Regex("<item>(.*?)</item>", RegexOptions.Singleline);
        //    var titleRegex = new Regex("<title>(.*?)</title>");
        //    var linkRegex = new Regex("<link>(.*?)</link>");
        //    var descriptionRegex = new Regex("<description>(.*?)</description>");
        //    var categoryRegex = new Regex("<category>(.*?)</category>");
        //    var pubDateRegex = new Regex("<pubDate>(.*?)</pubDate>");

        //    var itemMatches = itemRegex.Matches(xml);
        //    foreach (Match itemMatch in itemMatches)
        //    {
        //        var item = new Item();

        //        var titleMatch = titleRegex.Match(itemMatch.Groups[1].Value);
        //        item.Title = titleMatch.Success ? titleMatch.Groups[1].Value : "";

        //        var linkMatch = linkRegex.Match(itemMatch.Groups[1].Value);
        //        item.Link = linkMatch.Success ? linkMatch.Groups[1].Value : "";

        //        var descriptionMatch = descriptionRegex.Match(itemMatch.Groups[1].Value);
        //        item.Description = descriptionMatch.Success ? descriptionMatch.Groups[1].Value : "";

        //        var categoryMatch = categoryRegex.Match(itemMatch.Groups[1].Value);
        //        item.Category = categoryMatch.Success ? categoryMatch.Groups[1].Value : "";

        //        var pubDateMatch = pubDateRegex.Match(itemMatch.Groups[1].Value);
        //        item.PubDate = pubDateMatch.Success ? DateTime.Parse(pubDateMatch.Groups[1].Value) : DateTime.MinValue;

        //        items.Add(item);
        //    }

        //    return items;
        //}
    }
}
