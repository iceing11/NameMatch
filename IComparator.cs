namespace NameMatch;

public interface IComparator
{
    public double CompareSimilarity(string word1, string word2);
    public string GetName();
}