namespace WordFinder
{
    public interface IQueryFinder
    {
        public Dictionary<string, int> FindWordInFiles(string query, Dictionary<string, FileContent> wordsDatabase);
    }
}
