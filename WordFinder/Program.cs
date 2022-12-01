using WordFinder.SortingStrategies;
using WordFinder.WordCounter;

namespace WordFinder
{
    internal class Program
    {
        private static bool _cancelled = false;

        static void Main()
        {
            string[] arguments = Environment.GetCommandLineArgs();
            if (arguments.Length < 2)
            {
                Console.WriteLine("Provide the path to the directory containing text files");
                Console.WriteLine("Usage: WordFinder.exe <pathToDirectory>");
                return;
            }

            //find all text files in directory
            var directory = arguments[1];
            var files = Directory.GetFiles(directory, "*.txt", SearchOption.AllDirectories);
            Console.WriteLine($"Found {files.Length} text files.");

            //create words database for all files
            var wordsDatabase = WordOccurrenceCounter.GetWordOccurrenceAllFiles(files);

            //setup
            Console.WriteLine("\n----WordFinder----\n");
            var fileSorting = new FileSorting(new OrderByDescending());
            var querFinder = new QueryFinder();
            var searcher = new DocumentsSearcher(querFinder, fileSorting);

            //word search
            while (!_cancelled)
            {
                Console.Write("Search: ");
                var query = Console.ReadLine();
                try
                {
                    if (string.IsNullOrEmpty(query))
                        throw new ArgumentException($"\nEnter a valid query");

                    var results = searcher.FindTopFilesWithQuery(wordsDatabase, query, 10);
                    Console.WriteLine("Word found in the following file(s):");
                    foreach (var file in results)
                        Console.WriteLine($"{file.Key} : {file.Value}");
                }
                catch (NoMatchFoundException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                Console.WriteLine("\nPress Ctrl-C to stop searching.");
            }

            //Ctrl-C to stop application
            Console.CancelKeyPress += new ConsoleCancelEventHandler(Console_CancelKeyPress!);
        }

        static void Console_CancelKeyPress(object sender, ConsoleCancelEventArgs e)
        {
            if (e.SpecialKey == ConsoleSpecialKey.ControlC)
            {
                _cancelled = true;
            }
        }
    }
}

