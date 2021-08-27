using HtmlAgilityPack;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace VkPicturesParser.Parsers
{
    public class VkCommentImagesParser : IParser
    {
        public VkCommentImagesParser()
        {
        }

        public VkCommentImagesParser(string additionalUrl, bool commentsOnly = false)
        {
            AdditionalUrl = additionalUrl;
            CommentsOnly = commentsOnly;
        }

        public string MainUrl { get; } = @"https://vk.com";

        public string AdditionalUrl { get; private set; }

        public bool CommentsOnly { get; private set; }

        public List<string> Parse()
        {
            string fullUrl = $"{MainUrl}/{AdditionalUrl}";
            var web = new HtmlWeb();
            var doc = web.Load(fullUrl);
            HtmlNodeCollection unfilteredNodes = doc.DocumentNode.SelectNodes("//a[contains(@class, 'page_post_thumb_wrap image_cover  page_post_thumb_last_column page_post_thumb_last_row')]");

            if (unfilteredNodes == null)
                return new List<string>();

            List<HtmlNode> nodes;
            if (CommentsOnly)
            {
                nodes = new List<HtmlNode>();
                foreach (var n in unfilteredNodes)
                {
                    string node = n.ParentNode?.ParentNode?.Attributes["class"]?.Value;
                    if (node == null)
                    {
                        nodes.Add(n);
                    }
                }
            }
            else
            {
                nodes = unfilteredNodes.ToList();
            }

            List<string> result = new List<string>();
            string attributeValue;
            foreach (var node in nodes)
            {
                if (node.ParentNode.ParentNode.ChildNodes.Count > 1 && node.ParentNode.ParentNode.ChildNodes[1].Attributes["class"].Value == "Post__copyright")
                {
                    continue;
                }
                attributeValue = node.Attributes["style"].Value;
                result.Add(attributeValue.Substring(attributeValue.IndexOf("(") + 1, attributeValue.IndexOf(")") - attributeValue.IndexOf("(") - 1));
            }

            return result;
        }

        public Task<List<string>> ParseAsync()
        {
            return Task.Run(() => Parse());
        }
    }
}
