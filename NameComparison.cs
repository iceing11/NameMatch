using System.Text.RegularExpressions;

namespace NameMatch;

public class NameComparison
{
    private string _fullName;
    private double _levenshteinCloseness;
    private double _cosineSimilarity;
    private double _soundexCloseness;
    private string _soundex;
    private double _similarityScore;

    public NameComparison()
    { 
        _fullName = "N/A";
        _levenshteinCloseness = 0;
        _cosineSimilarity = 0;
        _soundexCloseness = 0;
        _soundex = "";
        _similarityScore = 0;
    }
    public NameComparison(string fullName, double levenshteinCloseness, double cosineSimilarity, double soundexCloseness, string soundex, double similarityScore)
    { 
        _fullName = fullName;
        _levenshteinCloseness = levenshteinCloseness;
        _cosineSimilarity = cosineSimilarity;
        _soundexCloseness = soundexCloseness;
        _soundex = soundex;
        _similarityScore = similarityScore;
    }

    public double GetSimilarityScore()
    {
        return _similarityScore;
    }

    public string ToString()
    {
        return _fullName + " ( Similarity Score: " + Math.Round(_similarityScore, 6) + " / Levenshtein Closeness: " + Math.Round(_levenshteinCloseness, 6) + " / Cosine Similarity: " + Math.Round(_cosineSimilarity, 6)
               + " / Soundex Levenshtein Closeness: " + Math.Round(_soundexCloseness, 6) + " / Soundex Code: " + _soundex + " )";
    }
}