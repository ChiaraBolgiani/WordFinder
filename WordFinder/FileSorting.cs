using WordFinder.SortingStrategies;

namespace WordFinder
{
    public class FileSorting : IFileSorting
    {
        private ISortingStategy _strategy;
        public FileSorting(ISortingStategy strategy)
        {
            _strategy = strategy;
        }
        public void SetStrategy(ISortingStategy strategy)
        {
            _strategy = strategy;
        }
        public IEnumerable<KeyValuePair<string, int>> OrderFiles(IEnumerable<KeyValuePair<string, int>> files)
        {
            return _strategy.OrderFiles(files);
        }
    }
}
