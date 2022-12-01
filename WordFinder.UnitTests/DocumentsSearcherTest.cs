using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace WordFinder.DocumentsSearcherTest
{
    [TestClass]
    public class DocumentsSearcherTest
    {
        private Mock<IQueryFinder> _mockQueryFinder = new();
        private Mock<IFileSorting> _mockFileSorting = new();
        private Dictionary<string, FileContent> _wordsDatabase = new()
        {
            { "file1", new FileContent() { wordsOccurrence = { { "coffee", 1 }, { "moka", 2 }, { "filter", 2 } } } },
            { "file2", new FileContent() { wordsOccurrence = { { "coffee", 2 }, { "moka", 2 }, { "filter", 5 } } } },
            { "file3", new FileContent() { wordsOccurrence = { { "coffee", 3 }, { "moka", 2 } } } },
            { "file4", new FileContent() { wordsOccurrence = { { "coffee", 4 }, { "moka", 1 } } } },
            { "file5", new FileContent() { wordsOccurrence = { { "coffee", 5 }, { "moka", 1 } } } },
        };


        [TestMethod]
        public void FindTopFilesWithQuery_ReturnTop3Files()
        {
            //Arrange
            var query = "coffee";
            var orderedResult = new Dictionary<string, int>()
            {
                {"file5", 5},
                {"file4", 4 },
                {"file3", 3 },
                {"file2", 2 },
                {"file1", 1 }
            };

            _mockFileSorting.Setup(x => x.OrderFiles(It.IsAny<Dictionary<string, int>>())).Returns(orderedResult);

            //Act
            var searcher = new DocumentsSearcher(_mockQueryFinder.Object, _mockFileSorting.Object);
            var result = searcher.FindTopFilesWithQuery(_wordsDatabase, query, 3);

            //Assert
            _mockQueryFinder.Verify(x => x.FindWordInFiles(query, _wordsDatabase), Times.Once);
            var expectedResult = new Dictionary<string, int>()
            {
                {"file5", 5},
                {"file4", 4 },
                {"file3", 3 }
            };
            Assert.IsTrue(expectedResult.SequenceEqual(result));
        }

        [TestMethod]
        public void FindTopFilesWithQuery_ReturnTop3Files_LessThan3Found()
        {
            var query = "filter";
            var orderedResult = new Dictionary<string, int>()
            {
                {"file2", 5},
                {"file1", 2 }
            };

            _mockFileSorting.Setup(x => x.OrderFiles(It.IsAny<Dictionary<string, int>>())).Returns(orderedResult);

            var searcher = new DocumentsSearcher(_mockQueryFinder.Object, _mockFileSorting.Object);
            var result = searcher.FindTopFilesWithQuery(_wordsDatabase, query, 3);

            _mockQueryFinder.Verify(x => x.FindWordInFiles(query, _wordsDatabase), Times.Once);
            var expectedResult = new Dictionary<string, int>()
            {
                {"file2", 5},
                {"file1", 2 }
            };
            Assert.IsTrue(expectedResult.SequenceEqual(result));
        }

        [TestMethod]
        public void FindTopFilesWithQuery_NoMatchFound()
        {
            var query = "beans";
            var orderedResult = new Dictionary<string, int>();

            _mockFileSorting.Setup(x => x.OrderFiles(It.IsAny<Dictionary<string, int>>())).Returns(orderedResult);

            var searcher = new DocumentsSearcher(_mockQueryFinder.Object, _mockFileSorting.Object);
            var excpetion = Assert.ThrowsException<NoMatchFoundException>(() => searcher.FindTopFilesWithQuery(_wordsDatabase, query, 3));
            Assert.AreEqual(excpetion.Message, $"No match found for word \"{query}\"");
        }
    }
}