namespace NameMatch;

public class NameGenerator
{
    private List<string> firstNames = new List<string>();
    private List<string> lastNames = new List<string>();
    private List<string> affixes3 = new List<string>();
    private List<string> affixes4 = new List<string>();
    private List<string> affixes5 = new List<string>();

    private List<string> names = new List<string>();

    private void PopulateList()
    {
        using (var reader = new StreamReader(@"E:\University\Internship\Name Match\Program\NameMatch\NameMatch\FirstNames.csv"))  
        {
            while (!reader.EndOfStream)
            {
                firstNames.Add(reader.ReadLine());
            }
        }
        
        using (var reader = new StreamReader(@"E:\University\Internship\Name Match\Program\NameMatch\NameMatch\LastNames.csv"))  
        {
            while (!reader.EndOfStream)
            {
                lastNames.Add(reader.ReadLine());
            }
        }
        
        foreach (var n in firstNames.Concat(lastNames))
        {
            if (n.Length <= 3)
            {
                affixes3.Add(n);
            }
        }
        
        foreach (var n in firstNames.Concat(lastNames))
        {
            if (n.Length <= 4)
            {
                affixes4.Add(n);
            }
        }

        foreach (var n in firstNames.Concat(lastNames))
        {
            if (n.Length <= 5)
            {
                affixes5.Add(n);
            }
        }

        List<string> prefixedFirstNames = new List<string>();
        List<string> prefixedLastNames = new List<string>();
        List<string> suffixedFirstNames = new List<string>();
        List<string> suffixedLastNames = new List<string>();
        
        for (int i = 0; i < 16000; i++)
        {
            var prefixedFirstName = affixes4.OrderBy(s => Guid.NewGuid()).First() + firstNames.OrderBy(s => Guid.NewGuid()).First();
            prefixedFirstNames.Add(prefixedFirstName);
        }
        
        for (int i = 0; i < 16000; i++)
        {
            var suffixedFirstName = firstNames.OrderBy(s => Guid.NewGuid()).First() + affixes3.OrderBy(s => Guid.NewGuid()).First();
            suffixedFirstNames.Add(suffixedFirstName);
        }

        for (int i = 0; i < 16000; i++)
        {
            var prefixedLastName = affixes5.OrderBy(s => Guid.NewGuid()).First() + lastNames.OrderBy(s => Guid.NewGuid()).First();
            prefixedLastNames.Add(prefixedLastName);
        }
        
        for (int i = 0; i < 16000; i++)
        {
            var suffixedLastName = lastNames.OrderBy(s => Guid.NewGuid()).First() + affixes4.OrderBy(s => Guid.NewGuid()).First();
            suffixedLastNames.Add(suffixedLastName);
        }
        
        firstNames = firstNames.Concat(prefixedFirstNames).Concat(suffixedFirstNames).ToList();
        lastNames = lastNames.Concat(prefixedLastNames).Concat(suffixedLastNames).ToList();
        Random random = new Random();

        using (StreamWriter writer = new StreamWriter(@"E:\University\Internship\Name Match\Program\NameMatch\NameMatch\ExpandedNames.csv"))
        {
            foreach (var n in lastNames)
            {
                double probability = 0.75;

                while (random.Next(0, 1) < probability)
                {
                    var generatedName = firstNames.OrderBy(s => Guid.NewGuid()).First() + "," + n;
                    //names.Add(generatedName);
                    writer.WriteLine(generatedName);
                    Console.WriteLine(generatedName);
                    probability -= 0.15;
                }
            }
        }

        Console.WriteLine(names.Count);
    }

    public void GenerateNames()
    {
        PopulateList();
    }
}