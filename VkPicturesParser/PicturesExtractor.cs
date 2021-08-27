using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using VkPicturesParser.Parsers;

namespace VkPicturesParser
{
    public class PicturesExtractor
    {
        /// <summary>
        /// groups properties will be filled by default
        /// </summary>
        public PicturesExtractor()
        {
            GroupsToParsePostsAndComments = new List<string>()
            {
                "voiceovers",
                "pichi_trollinga",
                "pickd",
                "trolpikt",
                "zzchan",
                "piktchis",
                "cats_jpg",
                "pic.text",
                "kakiepic"
            };
            GroupsToParseCommentsOnly = new List<string>()
            {
                "reddit",
                "sciencemem"
            };
        }

        /// <summary>
        /// groups are their names in url, 'reddit' for example
        /// </summary>
        /// <param name="groupsToParse"></param>
        public PicturesExtractor(List<string> groupsToParse)
        {
            GroupsToParsePostsAndComments = groupsToParse;
        }

        /// <summary>
        /// groups are their names in url, 'reddit' for example
        /// </summary>
        /// <param name="groupsToParsePostsAndComments"></param>
        /// <param name="groupsToParseCommentsOnly"></param>
        public PicturesExtractor(List<string> groupsToParsePostsAndComments, List<string> groupsToParseCommentsOnly)
        {

        }

        public List<string> GroupsToParsePostsAndComments { get; private set; } = new List<string>();

        public List<string> GroupsToParseCommentsOnly { get; private set; } = new List<string>();

        /// <summary>
        /// Returns a list of picture URLs
        /// </summary>
        /// <returns>List of picture URLs</returns>
        public List<string> Extract()
        {
            List<string> result = new List<string>();

            result.AddRange(ParseByAdditionalUrls(GroupsToParsePostsAndComments, false));
            result.AddRange(ParseByAdditionalUrls(GroupsToParseCommentsOnly, true));

            return result;
        }

        /// <summary>
        /// Returns a list of picture URLs
        /// </summary>
        /// <returns>List of picture URLs</returns>
        public async Task<List<string>> ExtractAsync()
        {
            return await Task.Run(() => Extract());
        }

        private List<string> ParseByAdditionalUrls(List<string> urls, bool commentsOnly)
        {
            List<string> result = new List<string>();

            foreach (string additionalUrl in urls)
            {
                List<string> wallAddresses = GetParsingResult(new VkWallsHrefsParser(additionalUrl));
                foreach (string wallAddress in wallAddresses)
                {
                    List<string> imagesUrls = GetParsingResult(new VkCommentImagesParser(wallAddress, commentsOnly));
                    foreach (var url in imagesUrls)
                    {
                        result.Add(url);
                    }
                }
            }

            return result;
        }

        private List<string> GetParsingResult(IParser parser)
        {
            return parser.Parse();
        }
    }
}
