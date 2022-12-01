namespace WordFinder.WordCounter
{
    public static class WordOccurrenceCounter
    {
        public static Dictionary<string, FileContent> GetWordOccurrenceAllFiles(string[] files)
        {
            var fileDatabase = new Dictionary<string, FileContent>();
            foreach (var file in files)
            {
                var fileContent = ComputeWordOccurrenceInFile(file);
                FileInfo fileInfo = new FileInfo(file);
                fileDatabase[fileInfo.Name] = fileContent;
            }
            return fileDatabase;
        }

        private static FileContent ComputeWordOccurrenceInFile(string file)
        {
            FileContent wordsOccurrence = new();

            using (StreamReader streamReader = new StreamReader(file))
            {
                var wordsDictionary = wordsOccurrence.wordsOccurrence;
                while (!streamReader.EndOfStream)
                {
                    var line = streamReader.ReadLine();

                    if (!string.IsNullOrEmpty(line))
                    {
                        var groups = line.Split(new char[] { ' ', '\'' }, StringSplitOptions.RemoveEmptyEntries)
                                                              .RemovePunctuation()
                                                              .ToLower()
                                                              .GroupBy(word => word);

                        foreach (var group in groups)
                        {
                            wordsDictionary[group.Key] = wordsDictionary.ContainsKey(group.Key) ? wordsDictionary[group.Key] + group.Count() : group.Count();
                        }
                    }
                }
            }

            return wordsOccurrence;
        }
    }
}

