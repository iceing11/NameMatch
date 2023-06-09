using System.Text.RegularExpressions;

namespace NameMatch;

public class Database
{
    private List<Person> Record = new List<Person>();
    private List<SoundexPair> SoundexRecord = new List<SoundexPair>();

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
        
        CreateSoundexPairs(persons);

        return persons;
    }

    private void CreateSoundexPairs(List<Person> record)
    {
        var soundexRecord = new List<SoundexPair>();

        foreach (var person in record)
        {
            SoundexPair soundexPair = new SoundexPair(person, Soundex.GetSoundex(person.GetFirstName()) + Soundex.GetSoundex(person.GetLastName()));
            soundexRecord.Add(soundexPair);
            //Console.WriteLine(soundexPair.GetSoundex());
        }

        SoundexRecord = soundexRecord;
    }

    public List<NameComparison> GetClosestNames(string input, IComparator comparator, int maxOutputCount)
    {
        List<NameComparison> results = new List<NameComparison>();

        var cleanInput = Regex.Replace(input, @"\s+", "");

        foreach (var person in Record)
        {
            var cleanName = person.GetFullName();

            var closeness = comparator.CompareSimilarity(cleanInput, cleanName);

            if (results.Count == 0 || results.Count < maxOutputCount)
            {
                results.Add(new NameComparison(person.GetFullName(), Soundex.GetSoundex(person.GetFirstName()) + Soundex.GetSoundex(person.GetLastName()), closeness));
                continue;
            }

            bool addToRanking = false;
            
            foreach (var result in results)
            {
                if (closeness > result.GetSimilarityScore())
                {
                    addToRanking = true;
                }
            }

            if (addToRanking)
            {
                results.Add(new NameComparison(person.GetFullName(), Soundex.GetSoundex(person.GetFirstName()) + Soundex.GetSoundex(person.GetLastName()), closeness));
                results = results.OrderByDescending(r => r.GetSimilarityScore()).ToList();

                if (results.Count > maxOutputCount) results.RemoveAt(results.Count - 1);
            }
        }

        return results;
    }
    
    public List<NameComparison> GetClosestSoundexes(string input, IComparator comparator, int maxOutputCount)
    {
        List<NameComparison> results = new List<NameComparison>();

        var inputSoundex = Soundex.GetSoundex(input.Split(' ')[0]) + Soundex.GetSoundex(input.Split(' ')[1]);

        foreach (var pair in SoundexRecord)
        {
            var closeness = comparator.CompareSimilarity(inputSoundex, pair.GetSoundex());

            if (results.Count == 0 || results.Count < maxOutputCount)
            {
                results.Add(new NameComparison(pair.GetPerson().GetFullName(), pair.GetSoundex(), closeness));
                continue;
            }

            bool addToRanking = false;
            
            foreach (var result in results)
            {
                if (closeness > result.GetSimilarityScore())
                {
                    addToRanking = true;
                }
            }

            if (addToRanking)
            {
                results.Add(new NameComparison(pair.GetPerson().GetFullName(), pair.GetSoundex(), closeness));
                results = results.OrderByDescending(r => r.GetSimilarityScore()).ToList();

                if (results.Count > maxOutputCount) results.RemoveAt(results.Count - 1);
            }
        }

        return results;
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