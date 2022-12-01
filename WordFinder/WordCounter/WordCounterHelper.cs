namespace WordFinder.WordCounter
{
    public static class WordCounterHelper
    {
        public static IEnumerable<string> RemovePunctuation(this IEnumerable<string> source)
        {
            foreach (var item in source)
                yield return new string(item.Where(c => !char.IsPunctuation(c)).ToArray());
        }

        public static IEnumerable<string> ToLower(this IEnumerable<string> source)
        {
            foreach (var item in source)
                yield return item.ToLower();
        }
    }
}
