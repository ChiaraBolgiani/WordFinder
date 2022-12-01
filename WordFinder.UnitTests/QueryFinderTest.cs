using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace WordFinder.QueryFinderTest
{
    [TestClass]
    public class QueryFinderTest
    {
        private Dictionary<string, FileContent> _wordsDatabase = new()
        {
            { "file1", new FileContent() { wordsOccurrence = { { "coffee", 1 }, { "moka", 2 }, { "filter", 2 } } } },
            { "file2", new FileContent() { wordsOccurrence = { { "coffee", 2 }, { "moka", 2 }, { "filter", 5 } } } },
            { "file3", new FileContent() { wordsOccurrence = { { "coffee", 3 }, { "moka", 2 } } } },
            { "file4", new FileContent() { wordsOccurrence = { { "coffee", 4 }, { "moka", 1 } } } },
            { "file5", new FileContent() { wordsOccurrence = { { "coffee", 5 }, { "moka", 1 } } } },
        };

        [TestMethod]
        public void FindQuery_AllFilesWithQuery()
        {
            string query = "coffee";
            var queryFinder = new QueryFinder();
            var result = queryFinder.FindWordInFiles(query, _wordsDatabase);

            var expectedResult = new Dictionary<string, int>()
            {
                {"file1", 1 },
                {"file2", 2 },
                {"file3", 3 },
                {"file4", 4 },
                {"file5", 5 },
            };

            Assert.IsTrue(expectedResult.SequenceEqual(result));
        }

        [TestMethod]
        public void FindQuery_SomFilesWithQuery()
        {
            string query = "filter";
            var queryFinder = new QueryFinder();
            var result = queryFinder.FindWordInFiles(query, _wordsDatabase);

            var expectedResult = new Dictionary<string, int>()
            {
                {"file1", 2 },
                {"file2", 5 }
            };

            Assert.IsTrue(expectedResult.SequenceEqual(result));
        }

        [TestMethod]
        public void FindQuery_NoFilesWithQuery()
        {
            string query = "beans";
            var queryFinder = new QueryFinder();
            var result = queryFinder.FindWordInFiles(query, _wordsDatabase);

            Assert.AreEqual(0, result.Count);
        }
    }
}