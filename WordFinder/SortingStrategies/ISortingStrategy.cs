namespace WordFinder.SortingStrategies
{
    public interface ISortingStategy
    {
        IEnumerable<KeyValuePair<string, int>> OrderFiles(IEnumerable<KeyValuePair<string, int>> files);
    }
}
