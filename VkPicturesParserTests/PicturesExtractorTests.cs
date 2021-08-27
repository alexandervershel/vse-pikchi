using Microsoft.VisualStudio.TestTools.UnitTesting;
using VkPicturesParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VkPicturesParser.Tests
{
    [TestClass()]
    public class PicturesExtractorTests
    {
        [TestMethod()]
        public void ExtractTest()
        {
            PicturesExtractor picturesExtractor = new PicturesExtractor();
            picturesExtractor.Extract();
        }
    }
}