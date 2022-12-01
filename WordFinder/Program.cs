using WordFinder.SortingStrategies;
using WordFinder.WordCounter;

namespace WordFinder
{
    internal class Program
    {
        static void Main()
        {
            var filePath = @"C:\Users\Chiara\Desktop\TestData";
            var files = Directory.GetFiles(filePath, "*.txt", SearchOption.AllDirectories);
            Console.WriteLine($"{files.Length} text files found");

            //create words database for all files
            var wordsDatabase = WordOccurrenceCounter.GetWordOccurrenceAllFiles(files);

            Console.WriteLine("\n----WordFinder----\n");

            string query = "coffee";
            var queryFinder = new QueryFinder();
            var filesWithQuery = queryFinder.FindWordInFiles(query, wordsDatabase);

            Console.WriteLine($"Query: {query}");
            foreach(var file in filesWithQuery)
                Console.WriteLine($"{file.Key} : {file.Value}");

            var fileSorting = new FileSorting(new OrderByDescending());
            var sortedFiles = fileSorting.OrderFiles(filesWithQuery);

            Console.WriteLine();
            foreach (var file in sortedFiles)
                Console.WriteLine($"{file.Key} : {file.Value}");
        }
    }
}

