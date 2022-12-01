namespace WordFinder.SortingStrategies
{
    public class OrderByDescending : ISortingStategy
    {
        public IEnumerable<KeyValuePair<string, int>> OrderFiles(IEnumerable<KeyValuePair<string, int>> files)
        {
            return files.OrderByDescending(x => x.Value);
        }
    }
}
