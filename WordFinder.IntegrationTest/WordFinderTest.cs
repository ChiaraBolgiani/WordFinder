using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WordFinder.SortingStrategies;
using WordFinder.WordCounter;

namespace WordFinder.IntegrationTest
{
    [TestClass]
    public class WordFinderTest
    {
        private static string _assemblyDir = Path.GetDirectoryName(typeof(WordFinderTest).Assembly.Location)!;
        private static string _testDataDir = Path.Combine(_assemblyDir!, @"..\..\..\TestData");
        private static string[] _testFiles = Directory.GetFiles(_testDataDir, "*.txt", SearchOption.AllDirectories);

        //create files-words dictionary
        private Dictionary<string, FileContent> _wordsDictionary = WordOccurrenceCounter.GetWordOccurrenceAllFiles(_testFiles);

        //word found in more than 10 files
        [TestMethod]
        public void WordFound_MoreThanTenFiles()
        {
            var query = "moka";
            var fileSorting = new FileSorting(new OrderByDescending());
            var queryFinder = new QueryFinder();
            var searcher = new DocumentsSearcher(queryFinder, fileSorting);
            var result = searcher.FindTopFilesWithQuery(_wordsDictionary, query, 10);

            Assert.AreEqual(10, result.Count());
            var expectedResult = new List<KeyValuePair<string, int>>
            {
                new KeyValuePair<string, int>("Coffee1.txt",13),
                new KeyValuePair<string, int>("Coffee2.txt",11),
                new KeyValuePair<string, int>("Coffee3.txt",11),
                new KeyValuePair<string, int>("Coffee4.txt",10),
                new KeyValuePair<string, int>("Coffee5.txt",9),
                new KeyValuePair<string, int>("Coffee6.txt",9),
                new KeyValuePair<string, int>("Coffee11.txt",6),
                new KeyValuePair<string, int>("Coffee7.txt",5),
                new KeyValuePair<string, int>("Coffee8.txt",5),
                new KeyValuePair<string, int>("Coffee12.txt",3),
            };

            Assert.IsTrue(result.SequenceEqual(expectedResult));
        }

        //word found in less than 10 files
        [TestMethod]
        public void WordFound_LessThanTenFiles()
        {
            var query = "beans";
            var fileSorting = new FileSorting(new OrderByDescending());
            var queryFinder = new QueryFinder();
            var searcher = new DocumentsSearcher(queryFinder, fileSorting);
            var result = searcher.FindTopFilesWithQuery(_wordsDictionary, query, 10);

            Assert.AreEqual(8, result.Count());
            var expectedResult = new List<KeyValuePair<string, int>>
            {
                new KeyValuePair<string, int>("Coffee1.txt",2),
                new KeyValuePair<string, int>("Coffee2.txt",2),
                new KeyValuePair<string, int>("Coffee3.txt",2),
                new KeyValuePair<string, int>("Coffee4.txt",1),
                new KeyValuePair<string, int>("Coffee5.txt",1),
                new KeyValuePair<string, int>("Coffee6.txt",1),
                new KeyValuePair<string, int>("Coffee7.txt",1),
                new KeyValuePair<string, int>("Coffee8.txt",1)
            };

            Assert.IsTrue(result.SequenceEqual(expectedResult));
        }

        //word not found
        [TestMethod]
        public void WordFound_NoMatch()
        {
            var query = "jelly";
            var fileSorting = new FileSorting(new OrderByDescending());
            var queryFinder = new QueryFinder();
            var searcher = new DocumentsSearcher(queryFinder, fileSorting);
            var excpetion = Assert.ThrowsException<NoMatchFoundException>(() => searcher.FindTopFilesWithQuery(_wordsDictionary, query, 10));
            Assert.AreEqual(excpetion.Message, $"No match found for word \"{query}\"");
        }

        //documents with same count
        [TestMethod]
        public void WordFound_FilesWithSameCount()
        {
            var query = "the";
            var fileSorting = new FileSorting(new OrderByDescending());
            var queryFinder = new QueryFinder();
            var searcher = new DocumentsSearcher(queryFinder, fileSorting);
            var result = searcher.FindTopFilesWithQuery(_wordsDictionary, query, 10);

            Assert.AreEqual(10, result.Count());
            var expectedResult = new List<KeyValuePair<string, int>>
            {
                new KeyValuePair<string, int>("Coffee1.txt",38),
                new KeyValuePair<string, int>("Coffee2.txt",36),
                new KeyValuePair<string, int>("Coffee3.txt",29),
                new KeyValuePair<string, int>("Coffee4.txt",24),
                new KeyValuePair<string, int>("Coffee5.txt",20),
                new KeyValuePair<string, int>("Coffee6.txt",16),
                new KeyValuePair<string, int>("Coffee11.txt",7),
                new KeyValuePair<string, int>("Coffee12.txt",3),
                new KeyValuePair<string, int>("Coffee7.txt",3),
                new KeyValuePair<string, int>("Coffee8.txt",2)
            };

            Assert.IsTrue(result.SequenceEqual(expectedResult));
        }

        //query with capital letters
        [TestMethod]
        public void WordFound_SearchWordWithCapitalLetter()
        {
            var query = "Spain";
            var fileSorting = new FileSorting(new OrderByDescending());
            var queryFinder = new QueryFinder();
            var searcher = new DocumentsSearcher(queryFinder, fileSorting);
            var result = searcher.FindTopFilesWithQuery(_wordsDictionary, query, 10);

            Assert.AreEqual(1, result.Count());
            var expectedResult = new List<KeyValuePair<string, int>>
            {
                new KeyValuePair<string, int>("Coffee12.txt",1),
            };

            Assert.IsTrue(result.SequenceEqual(expectedResult));
        }
    }
}