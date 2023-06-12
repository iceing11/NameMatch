using System.Text.RegularExpressions;

namespace NameMatch;

public class Database
{
    private List<Person> Record = new List<Person>();
    private List<SoundexPair> SoundexRecord = new List<SoundexPair>();
    private Soundex soundex = new Soundex();
    private int _soundexCode;

    public Database(string recordPath, int soundexCode)
    {
        Record = LoadRecords(recordPath, soundexCode);
    }

    private List<Person> LoadRecords(string path, int soundexCode)
    {
        var persons = new List<Person>();
        _soundexCode = soundexCode;
        
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
            SoundexPair soundexPair = new SoundexPair(person, soundex.GetSoundex(person.GetFirstName(), _soundexCode), soundex.GetSoundex(person.GetLastName(), _soundexCode));
            soundexRecord.Add(soundexPair);
            //Console.WriteLine(soundexPair.GetSoundex());
        }

        SoundexRecord = soundexRecord;
    }

    public List<NameComparison> GetClosestNames(string input, IComparator comparator, double outputThreshold)
    {
        List<NameComparison> results = new List<NameComparison>();
        
        var inputFirstName = input.Split(' ')[0];
        var inputLastName = input.Split(' ')[1];

        foreach (var person in Record)
        {
            var firstName = person.GetFirstName();
            var lastName = person.GetLastName();

            var firstNameCloseness = comparator.CompareSimilarity(inputFirstName, firstName);
            var lastNameCloseness = comparator.CompareSimilarity(inputLastName, lastName);
            
            if ((firstNameCloseness + lastNameCloseness) / 2 >= outputThreshold)
            {
                results.Add(new NameComparison(person.GetFirstName(), person.GetLastName(), soundex.GetSoundex(person.GetFirstName(), _soundexCode), soundex.GetSoundex(person.GetLastName(), _soundexCode), firstNameCloseness, lastNameCloseness));
            }
        }

        results = results.OrderByDescending(r => r.GetSimilarityScore()).ToList();
        return results;
    }
    
    public List<NameComparison> GetClosestSoundexes(string input, IComparator comparator, double outputThreshold)
    {
        List<NameComparison> results = new List<NameComparison>();
        
        var inputFirstNameSoundex = soundex.GetSoundex(input.Split(' ')[0], _soundexCode);
        var inputLastNameSoundex = soundex.GetSoundex(input.Split(' ')[1], _soundexCode);

        foreach (var soundexPair in SoundexRecord)
        {
            var firstNameSoundex = soundexPair.GetFirstNameSoundex();
            var lastNameSoundex = soundexPair.GetLastNameSoundex();

            var firstNameCloseness = comparator.CompareSimilarity(inputFirstNameSoundex, firstNameSoundex);
            var lastNameCloseness = comparator.CompareSimilarity(inputLastNameSoundex, lastNameSoundex);
            
            if ((firstNameCloseness + lastNameCloseness) / 2 >= outputThreshold)
            {
                results.Add(new NameComparison(soundexPair.GetPerson().GetFirstName(), soundexPair.GetPerson().GetLastName(), firstNameSoundex, lastNameSoundex, firstNameCloseness, lastNameCloseness));
            }
        }

        results = results.OrderByDescending(r => r.GetSimilarityScore()).ToList();
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