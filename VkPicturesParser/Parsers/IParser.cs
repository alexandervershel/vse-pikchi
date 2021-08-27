using System.Collections.Generic;
using System.Threading.Tasks;

namespace VkPicturesParser.Parsers
{
    public interface IParser
    {
        public string MainUrl { get; }

        public string AdditionalUrl { get; }

        public List<string> Parse();

        public Task<List<string>> ParseAsync();
    }
}
