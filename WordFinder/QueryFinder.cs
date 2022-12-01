namespace WordFinder
{
    public class QueryFinder
    {
        public Dictionary<string, int> FindWordInFiles(string query, Dictionary<string, FileContent> wordsDatabase)
        {
            var occurrenceInFile = wordsDatabase.Select(x => new { Name = x.Key, Occurrence = x.Value.wordsOccurrence.GetValueOrDefault(query.ToLower(), 0) });
            var filesWithQuery = occurrenceInFile.Where(file => file.Occurrence > 0);
            return filesWithQuery.ToDictionary(x => x.Name, x => x.Occurrence);
        }
    }
}
