namespace WordFinder
{
    public class DocumentsSearcher
    {
        private IQueryFinder _queryFinder;
        private IFileSorting _fileSorting;

        public DocumentsSearcher(IQueryFinder queryFinder, IFileSorting fileSorting)
        {
            _queryFinder = queryFinder;
            _fileSorting = fileSorting;
        }

        public IEnumerable<KeyValuePair<string, int>> FindTopFilesWithQuery(Dictionary<string, FileContent> wordsDatabase, string query, int topFilesN)
        {
            var queredFiles = _queryFinder.FindWordInFiles(query, wordsDatabase);

            var orderedFiles = _fileSorting.OrderFiles(queredFiles);

            if (orderedFiles.Count() > 0)
            {
                return orderedFiles.Take(topFilesN);
            }
            else
            {
                throw new NoMatchFoundException(query);
            }
        }
    }
}
