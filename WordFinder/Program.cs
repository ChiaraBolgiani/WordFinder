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
        }
    }
}

