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

            string query = "bean";
            
            var queryFinder = new QueryFinder();
            var fileSorting = new FileSorting(new OrderByDescending());
            var documentsSearcher = new DocumentsSearcher(queryFinder, fileSorting);
            
            Console.WriteLine($"Query: {query}");
            try
            {
                var topFiles = documentsSearcher.FindTopFilesWithQuery(wordsDatabase, query, 10);
                foreach (var file in topFiles)
                    Console.WriteLine($"{file.Key} : {file.Value}");
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }
    }
}

