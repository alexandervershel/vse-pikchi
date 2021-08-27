using System.Collections.Generic;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace VkPicturesParser.Parsers
{
    public class VkWallsHrefsParser : IParser
    {
        public VkWallsHrefsParser(string additionalUrl)
        {
            AdditionalUrl = additionalUrl;
        }

        public string MainUrl { get; } = "https://vk.com";

        public string AdditionalUrl { get; private set; }

        public List<string> Parse()
        {
            string fullUrl = $"{MainUrl}/{AdditionalUrl}";
            var web = new HtmlWeb();
            var doc = web.Load(fullUrl);
            HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes("//a[contains(@class, 'replies_next replies_next_main')]");
            if (nodes == null)
                return new List<string>();

            List<string> result = new List<string>();
            foreach (var node in nodes)
            {
                result.Add(node.Attributes["href"].Value);
            }

            return result;
        }

        public Task<List<string>> ParseAsync()
        {
            return Task.Run(() => Parse());
        }
    }
}
