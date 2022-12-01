using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using WordFinder.WordCounter;

namespace WordFinder.WordCounterTest
{
    [TestClass]
    public class WordCounterTest
    {
        private readonly static string _directoryPath = Path.Combine(Directory.GetCurrentDirectory(), @"tmp");

        [TestInitialize]
        public void TestInitialize()
        {
            Directory.CreateDirectory(_directoryPath);

            var files = Directory.GetFiles(_directoryPath, "*.txt", SearchOption.AllDirectories);
            Assert.AreEqual(0, files.Length);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            Directory.Delete(_directoryPath, true);
        }

        //one line one word
        [TestMethod]
        public void CountWordOccurrence_OneLine_OneWord()
        {
            string fileName = Path.Combine(_directoryPath, $"test.txt");

            using (FileStream fs = File.Create(fileName))
            {
                byte[] text = Encoding.UTF8.GetBytes("coffee coffee coffee");
                fs.Write(text, 0, text.Length);
            }

            var files = Directory.GetFiles(_directoryPath, "*.txt", SearchOption.AllDirectories);
            var result = WordOccurrenceCounter.GetWordOccurrenceAllFiles(files);

            Assert.AreEqual(1, result.Keys.Count);
            Assert.AreEqual(1, result["test.txt"].wordsOccurrence.Values.Count);

            var expectedResult = new Dictionary<string, int>()
            {
                {"coffee", 3},
            };

            Assert.IsTrue(expectedResult.SequenceEqual(result["test.txt"].wordsOccurrence));
        }

        //multiple lines and words
        [TestMethod]
        public void CountWordOccurrence_MultipleLines_Words()
        {
            string fileName = Path.Combine(_directoryPath, $"test.txt");

            using (FileStream fs = File.Create(fileName))
            {
                // Add some text to file    
                byte[] text = Encoding.UTF8.GetBytes("coffee \n coffee coffee \n more coffee");
                fs.Write(text, 0, text.Length);
            }

            var files = Directory.GetFiles(_directoryPath, "*.txt", SearchOption.AllDirectories);
            var result = WordOccurrenceCounter.GetWordOccurrenceAllFiles(files);
            Assert.AreEqual(1, result.Keys.Count);
            Assert.AreEqual(2, result["test.txt"].wordsOccurrence.Values.Count);

            var expectedResult = new Dictionary<string, int>()
            {
                {"coffee", 4},
                {"more", 1 }
            };

            Assert.IsTrue(expectedResult.SequenceEqual(result["test.txt"].wordsOccurrence));
        }

        //punctuation
        [TestMethod]
        public void CountWordOccurrence_Punctuation()
        {
            string fileName = Path.Combine(_directoryPath, @"test.txt");

            using (FileStream fs = File.Create(fileName))
            {
                byte[] text = Encoding.UTF8.GetBytes("how to make coffee: grind coffee, brew coffee and enjoy coffee!");
                fs.Write(text, 0, text.Length);
            }

            var files = Directory.GetFiles(_directoryPath, "*.txt", SearchOption.AllDirectories);
            var result = WordOccurrenceCounter.GetWordOccurrenceAllFiles(files);

            var expectedResult = new Dictionary<string, int>()
            {
                {"how", 1},
                {"to", 1 },
                {"make", 1 },
                {"coffee", 4 },
                {"grind", 1 },
                {"brew", 1 },
                {"and", 1 },
                {"enjoy", 1 }
            };

            Assert.IsTrue(expectedResult.SequenceEqual(result["test.txt"].wordsOccurrence));
        }

        //lower/upper case
        [TestMethod]
        public void CountWordOccurrence_LowerUpperCase()
        {
            string fileName = Path.Combine(_directoryPath, @"test.txt");

            using (FileStream fs = File.Create(fileName))
            {
                byte[] text = Encoding.UTF8.GetBytes("Home Is where THE CoFfEe iS.");
                fs.Write(text, 0, text.Length);
            }

            var files = Directory.GetFiles(_directoryPath, "*.txt", SearchOption.AllDirectories);
            var result = WordOccurrenceCounter.GetWordOccurrenceAllFiles(files);

            var expectedResult = new Dictionary<string, int>()
            {
                {"home", 1},
                {"is", 2 },
                {"where", 1 },
                {"the", 1 },
                {"coffee", 1 }
            };

            Assert.IsTrue(expectedResult.SequenceEqual(result["test.txt"].wordsOccurrence));
        }

        //apostrophe
        [TestMethod]
        public void CountWordOccurrence_Apostrophe()
        {
            string fileName = Path.Combine(_directoryPath, @"test.txt");

            using (FileStream fs = File.Create(fileName))
            {
                byte[] text = Encoding.UTF8.GetBytes("Coffee's the word");
                fs.Write(text, 0, text.Length);
            }

            var files = Directory.GetFiles(_directoryPath, "*.txt", SearchOption.AllDirectories);
            var result = WordOccurrenceCounter.GetWordOccurrenceAllFiles(files);

            var expectedResult = new Dictionary<string, int>()
            {
                {"coffee", 1},
                {"s", 1 },
                {"the", 1 },
                {"word", 1 }
            };

            Assert.IsTrue(expectedResult.SequenceEqual(result["test.txt"].wordsOccurrence));
        }

        //math and numbers
        [TestMethod]
        public void CountWordOccurrence_Numbers()
        {
            string fileName = Path.Combine(_directoryPath, @"test.txt");

            using (FileStream fs = File.Create(fileName))
            {
                byte[] text = Encoding.UTF8.GetBytes("1 coffee + 2 coffees = 3 coffees\n ");
                fs.Write(text, 0, text.Length);
            }

            var files = Directory.GetFiles(_directoryPath, "*.txt", SearchOption.AllDirectories);
            var result = WordOccurrenceCounter.GetWordOccurrenceAllFiles(files);

            var expectedResult = new Dictionary<string, int>()
            {
                {"1", 1},
                {"coffee", 1 },
                {"+", 1 },
                {"2", 1 },
                {"coffees", 2 },
                {"=", 1 },
                {"3", 1 }
            };

            Assert.IsTrue(expectedResult.SequenceEqual(result["test.txt"].wordsOccurrence));
        }

        //empty document
        [TestMethod]
        public void CountWordOccurrence_EmptyDocument()
        {
            string fileName = Path.Combine(_directoryPath, $"test.txt");

            using (FileStream fs = File.Create(fileName)) { };

            var files = Directory.GetFiles(_directoryPath, "*.txt", SearchOption.AllDirectories);
            var result = WordOccurrenceCounter.GetWordOccurrenceAllFiles(files);
            Assert.AreEqual(1, result.Keys.Count);
            Assert.AreEqual(0, result["test.txt"].wordsOccurrence.Values.Count);
        }

        //multiple docs, one word
        [TestMethod]
        public void CountWordOccurrence_MultipleDocuments_OneWord()
        {
            for (int i = 0; i < 3; i++)
            {
                string fileName = Path.Combine(_directoryPath, $"test{i}.txt");
                using (FileStream fs = File.Create(fileName))
                {
                    byte[] text = Encoding.UTF8.GetBytes("coffee\n");
                    for (int j = 0; j < i + 1; j++)
                        fs.Write(text, 0, text.Length);
                }
            }

            var files = Directory.GetFiles(_directoryPath, "*.txt", SearchOption.AllDirectories);
            Assert.AreEqual(3, files.Length);

            var result = WordOccurrenceCounter.GetWordOccurrenceAllFiles(files);
            Assert.AreEqual(3, result.Keys.Count);

            for (var i = 0; i < result.Keys.Count; i++)
            {
                Assert.AreEqual(1, result[$"test{i}.txt"].wordsOccurrence.Values.Count);
                Assert.AreEqual("coffee", result[$"test{i}.txt"].wordsOccurrence.Keys.First());
                Assert.AreEqual(i + 1, result[$"test{i}.txt"].wordsOccurrence["coffee"]);
            }
        }

        //multiple docs, multiple words
        [TestMethod]
        public void CountWordOccurrence_MultipleDocuments_MultipleWords()
        {
            string file1 = Path.Combine(_directoryPath, $"test1.txt");
            string file2 = Path.Combine(_directoryPath, $"test2.txt");
            string file3 = Path.Combine(_directoryPath, $"test3.txt");

            using (FileStream fs = File.Create(file1))
            {
                byte[] text = Encoding.UTF8.GetBytes("1) Grind the coffee beans");
                fs.Write(text, 0, text.Length);
            }

            using (FileStream fs = File.Create(file2))
            {
                byte[] text = Encoding.UTF8.GetBytes("1) Grind the coffee beans\n2) Brew the coffee in a moka");
                fs.Write(text, 0, text.Length);
            }

            using (FileStream fs = File.Create(file3))
            {
                byte[] text = Encoding.UTF8.GetBytes("1) Grind the coffee beans\n2) Brew the coffee in a moka\n3) Pour it and enjoy the coffee!");
                fs.Write(text, 0, text.Length);
            }

            var files = Directory.GetFiles(_directoryPath, "*.txt", SearchOption.AllDirectories);
            Assert.AreEqual(3, files.Length);

            var result = WordOccurrenceCounter.GetWordOccurrenceAllFiles(files);
            Assert.AreEqual(3, result.Keys.Count);
            for (int i = 0; i < result.Keys.Count; i++)
            {
                Assert.AreEqual($"test{i + 1}.txt", result.Keys.ElementAt(i));
            }

            //test file1
            var file1Dictionary = result["test1.txt"];
            var expectedFile1 = new Dictionary<string, int>
            {
                {"1", 1 },
                {"grind", 1 },
                {"the", 1 },
                {"coffee", 1 },
                {"beans", 1 },
            };
            Assert.IsTrue(expectedFile1.SequenceEqual(file1Dictionary.wordsOccurrence));

            //test file2
            var file2Dictionary = result["test2.txt"];
            var expectedFile2 = new Dictionary<string, int>
            {
                {"1", 1 },
                {"grind", 1 },
                {"the", 2 },
                {"coffee", 2 },
                {"beans", 1 },
                {"2", 1 },
                {"brew", 1 },
                {"in", 1 },
                {"a", 1 },
                {"moka", 1 }
            };
            Assert.IsTrue(expectedFile2.SequenceEqual(file2Dictionary.wordsOccurrence));

            //test file3
            var file3Dictionary = result["test3.txt"];
            var expectedFile3 = new Dictionary<string, int>
            {
                {"1", 1 },
                {"grind", 1 },
                {"the", 3 },
                {"coffee", 3 },
                {"beans", 1 },
                {"2", 1 },
                {"brew", 1 },
                {"in", 1 },
                {"a", 1 },
                {"moka", 1 },
                {"3", 1 },
                {"pour", 1 },
                {"it", 1 },
                {"and", 1 },
                {"enjoy", 1 }
            };
            Assert.IsTrue(expectedFile3.SequenceEqual(file3Dictionary.wordsOccurrence));
        }
    }
}