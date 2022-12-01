using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using WordFinder.SortingStrategies;

namespace WordFinder.FileSortingTest
{
    [TestClass]
    public class FileSortingTest
    {
        [TestMethod]
        public void FileOrderer_OrderByDescending()
        {
            List<KeyValuePair<string, int>> inputFiles = new()
            {
                new KeyValuePair<string, int>("file1", 2),
                new KeyValuePair<string, int>("file2", 3),
                new KeyValuePair<string, int>("file3", 1),
                new KeyValuePair<string, int>("file4", 5),
                new KeyValuePair<string, int>("file5", 4),
            };

            var fileSorting = new FileSorting(new OrderByDescending());
            var sortedFiles = fileSorting.OrderFiles(inputFiles);

            var expectedResult = new List<KeyValuePair<string, int>>()
            {
                new KeyValuePair<string, int>("file4", 5),
                new KeyValuePair<string, int>("file5", 4),
                new KeyValuePair<string, int>("file2", 3),
                new KeyValuePair<string, int>("file1", 2),
                new KeyValuePair<string, int>("file3", 1),
            };

            Assert.IsTrue(expectedResult.SequenceEqual(sortedFiles));
        }

        [TestMethod]
        public void FileOrderer_OrderByDescending_SameCount_InputOrder()
        {
            List<KeyValuePair<string, int>> inputFiles = new()
            {
                new KeyValuePair<string, int>("file4", 2),
                new KeyValuePair<string, int>("file1", 2),
                new KeyValuePair<string, int>("file2", 2),
                new KeyValuePair<string, int>("file3", 1),
                new KeyValuePair<string, int>("file5", 4),
            };

            var fileSorting = new FileSorting(new OrderByDescending());
            var sortedFiles = fileSorting.OrderFiles(inputFiles);

            var expectedResult = new List<KeyValuePair<string, int>>()
            {
                new KeyValuePair<string, int>("file5", 4),
                new KeyValuePair<string, int>("file4", 2),
                new KeyValuePair<string, int>("file1", 2),
                new KeyValuePair<string, int>("file2", 2),
                new KeyValuePair<string, int>("file3", 1),
            };

            Assert.IsTrue(expectedResult.SequenceEqual(sortedFiles));
        }
    }
}