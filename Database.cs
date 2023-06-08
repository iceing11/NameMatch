using System.Text.RegularExpressions;

namespace NameMatch;

public class Database
{
    private List<Person> Record = new List<Person>();

    public Database(string recordPath)
    {
        Record = LoadRecords(recordPath);
    }

    private List<Person> LoadRecords(string path)
    {
        var persons = new List<Person>();
        
        using (var reader = new StreamReader(path))  
        {
            while (!reader.EndOfStream)
            {
                var values = new string[2];
                var line = reader.ReadLine();
                if (line != null)
                {
                    values = line.Split(',');
                }

                var p = new Person(values[0], values[1]);
                persons.Add(p);
            }
        }

        return persons;
    }

    public NameComparison GetClosestName(string name)
    {
        var cleanInput = Regex.Replace(name, @"\s+", "");
        var inputSoundex = Soundex.GetSoundex(name.Split(' ')[0]) + Soundex.GetSoundex(name.Split(' ')[1]);

        LevenshteinComparator quickenshteinComparator = new LevenshteinComparator();
        CosineComparator cosineComparator = new CosineComparator();

        var result = new NameComparison();

        foreach (var person in Record)
        {
            var cleanName = person.GetFullName();
            var soundex = Soundex.GetSoundex(person.GetFirstName()) + Soundex.GetSoundex(person.GetLastName());
            
            // Levenshtein Closeness
            IComparator comparator = quickenshteinComparator;
            var levenshteinCloseness = comparator.CompareSimilarity(cleanInput, cleanName);
            // Soundex Levenshtein Closeness
            var soundexLevenshteinCloseness = comparator.CompareSimilarity(inputSoundex, soundex);
            // Cosine Similarity
            comparator = cosineComparator;
            var cosineSimilarity = comparator.CompareSimilarity(cleanInput, cleanName);
            
            // Similarity Score
            var similarityScore = levenshteinCloseness * soundexLevenshteinCloseness * cosineSimilarity;

            if (similarityScore > result.GetSimilarityScore()) result = new NameComparison(person.GetFullName(" "), levenshteinCloseness, cosineSimilarity, soundexLevenshteinCloseness, soundex, similarityScore);
        }

        return result;
    }

    public Person? GetPersonByIndex(int index)
    {
        Person? result = null;
        
        try
        {
            result = Record[index];
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

        return result;
    }
}