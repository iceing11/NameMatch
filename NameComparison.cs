using System.Text.RegularExpressions;

namespace NameMatch;

public class NameComparison
{
    private string _fullName;
    private string _soundex;
    private double _similarityScore;

    public NameComparison()
    { 
        _fullName = "N/A";
        _soundex = "";
        _similarityScore = 0;
    }
    public NameComparison(string fullName, string soundex, double similarityScore)
    { 
        _fullName = fullName;
        _soundex = soundex;
        _similarityScore = similarityScore;
    }

    public double GetSimilarityScore()
    {
        return _similarityScore;
    }

    public override string ToString()
    {
        return _fullName + " ( Similarity Score: " + Math.Round(_similarityScore, 6) + " / Soundex Code: " + _soundex + " )";
    }
}