namespace NameMatch;

public class NaiveLevenshteinComparator : IComparator
{
    public double CompareSimilarity(string word1, string word2)
    {
        var maxLength = Math.Max(word1.Length, word2.Length);
        var word1Length = word1.Length;
        var word2Length = word2.Length;

        var matrix = new int[word1Length + 1, word2Length + 1];

        // First calculation, if one entry is empty return full length
        if (word1Length == 0)
            return word2Length;

        if (word2Length == 0)
            return word1Length;

        // Initialization of matrix with row size word1Length and columns size word2Length
        for (var i = 0; i <= word1Length; matrix[i, 0] = i++) { }
        for (var j = 0; j <= word2Length; matrix[0, j] = j++) { }

        // Calculate rows and columns distances
        for (var i = 1; i <= word1Length; i++)
        {
            for (var j = 1; j <= word2Length; j++)
            {
                var cost = (word2[j - 1] == word1[i - 1]) ? 0 : 1;

                matrix[i, j] = Math.Min(
                    Math.Min(matrix[i - 1, j] + 1, matrix[i, j - 1] + 1),
                    matrix[i - 1, j - 1] + cost);
            }
        }

        return (double)(maxLength - matrix[word1Length, word2Length]) / maxLength;
    }
    
    public string GetName()
    {
        return "Levenshtein Distance";
    }
}