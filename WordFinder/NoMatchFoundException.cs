namespace WordFinder
{
    public class NoMatchFoundException : Exception
    {
        public NoMatchFoundException() { }

        public NoMatchFoundException(string word) : base($"No match found for word \"{word}\"")
        {
        }
    }
}
