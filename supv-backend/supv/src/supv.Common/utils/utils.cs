public static class Utils
{
    public static string UppercaseFirstWord(string word)
    {
        if (string.IsNullOrEmpty(word))
        {
            return word;
        }

        return char.ToUpperInvariant(word[0]) + word.Substring(1);
    }
}
