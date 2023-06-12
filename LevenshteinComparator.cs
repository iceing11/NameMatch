using Quickenshtein;

namespace NameMatch;

public class LevenshteinComparator : IComparator
{
    public double CompareSimilarity(string word1, string word2)
    {
        var maxLength = Math.Max(word1.Length, word2.Length);
        var distance = Quickenshtein.Levenshtein.GetDistance(word1, word2);

        return (double)(maxLength - distance) / maxLength;
    }
    
    public string GetName()
    {
        return "Quickenshtein Distance";
    }
}