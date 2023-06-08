namespace NameMatch;

public class CosineComparator : IComparator
{
    public double CompareSimilarity(string word1, string word2)
    {
        var ngrams1 = GetNgramsUpToSize(word1, 3);
        var ngrams2 = GetNgramsUpToSize(word2, 3);
        var commonNgrams = ngrams1.Intersect(ngrams2).ToList();
        var word1NgramFrequency = ngrams1.GroupBy(x => x).ToDictionary(x => x.Key, x => x.Count());
        var word2NgramFrequency = ngrams2.GroupBy(x => x).ToDictionary(x => x.Key, x => x.Count());
        var productSummation = 0;
        var word1PowerLength = 0;
        var word2PowerLength = 0;
        double length = 0;
        
        foreach(int frequency in word1NgramFrequency.Values)
        {
            word1PowerLength += frequency * frequency;
        }
        
        foreach(int frequency in word2NgramFrequency.Values)
        {
            word2PowerLength += frequency * frequency;
        }

        length = Math.Sqrt(word1PowerLength) * Math.Sqrt(word2PowerLength);
        
        foreach (string ngram in commonNgrams)
        {
            var word1Frequency = word1NgramFrequency[ngram];
            var word2Frequency = word2NgramFrequency[ngram];
            productSummation += word1Frequency * word2Frequency;
        }

        if (length == 0)
        {
            return 0;
        }
        else
        {
            return Math.Round(productSummation / length, 5);
        }
    }

    private List<string> GetNgramsUpToSize(string word, int size)
    {
        var result = new List<string>();
        
        for (int i = 1; i <= size; i++)
        {
            result = result.Concat(GetNgrams(word, i)).ToList();
        }

        return result;
    }
    
    private List<string> GetNgrams(string word, int ngramSize) 
    {
        List<string> ngrams = new List<string>();

        for (int i = 0; i < word.Length - (ngramSize - 1); i++)
        {
            string ngram = word.Substring(i, ngramSize);
            ngrams.Add(ngram);
        }

        return ngrams;
    }
}