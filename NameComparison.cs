using System.Text.RegularExpressions;

namespace NameMatch;

public class NameComparison
{
    private string _fullName;
    private string _firstNameSoundex;
    private string _lastNameSoundex;
    private double _firstNameSimilarityScore;
    private double _lastNameSimilarityScore;
    private double _similarityScore;

    public NameComparison()
    { 
        _fullName = "N/A";
        _firstNameSoundex = "";
        _lastNameSoundex = "";
        _similarityScore = 0;
    }
    public NameComparison(string firstName, string lastName, string firstNameSoundex, string lastNameSoundex, double firstNameSimilarityScore, double lastNameSimilarityScore)
    { 
        _fullName = firstName + " " + lastName;
        _firstNameSoundex = firstNameSoundex;
        _lastNameSoundex = lastNameSoundex;
        _firstNameSimilarityScore = firstNameSimilarityScore;
        _lastNameSimilarityScore = lastNameSimilarityScore;
        _similarityScore = (firstNameSimilarityScore + lastNameSimilarityScore) / 2;
    }

    public double GetSimilarityScore()
    {
        return _similarityScore;
    }

    public override string ToString()
    {
        return _fullName + " ( Similarity Score: " + Math.Round(_similarityScore, 6) + " ( " + _firstNameSimilarityScore + " / " + _lastNameSimilarityScore  +" ) " + "/ Soundex Code: " + _firstNameSoundex + "/" + _lastNameSoundex + " )";
    }
}