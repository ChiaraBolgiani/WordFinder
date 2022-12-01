namespace WordFinder
{
    public interface IFileSorting
    {
        public IEnumerable<KeyValuePair<string, int>> OrderFiles(IEnumerable<KeyValuePair<string, int>> files);
    }
}
