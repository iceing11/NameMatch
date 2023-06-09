namespace NameMatch;

public class NameGenerator
{
    private List<string> firstNames = new List<string>();
    private List<string> lastNames = new List<string>();
    private List<string> affixes3 = new List<string>();
    private List<string> affixes4 = new List<string>();
    private List<string> shortFirstNames = new List<string>();
    private List<string> shortLastNames = new List<string>();

    private List<string> names = new List<string>();

    private void PopulateList()
    {
        using (var reader = new StreamReader(@"E:\University\Internship\Name Match\Program\NameMatch\NameMatch\FirstNames.csv"))  
        {
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                if (line != null)
                {
                    firstNames.Add(line);

                    if (line.Length <= 6)
                    {
                        shortFirstNames.Add(line);
                    }
                }
            }
        }
        
        using (var reader = new StreamReader(@"E:\University\Internship\Name Match\Program\NameMatch\NameMatch\LastNames.csv"))  
        {
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                if (line != null)
                {
                    lastNames.Add(line);
                    
                    if (line.Length <= 8)
                    {
                        shortLastNames.Add(line);
                    }
                }
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

        List<string> prefixedFirstNames = new List<string>();
        List<string> prefixedLastNames = new List<string>();
        List<string> suffixedFirstNames = new List<string>();
        List<string> suffixedLastNames = new List<string>();
        
        for (int i = 0; i < 16000; i++)
        {
            var prefixedFirstName = affixes4.OrderBy(s => Guid.NewGuid()).First() + shortFirstNames.OrderBy(s => Guid.NewGuid()).First();
            prefixedFirstNames.Add(prefixedFirstName);
        }
        
        for (int i = 0; i < 16000; i++)
        {
            var suffixedFirstName = shortFirstNames.OrderBy(s => Guid.NewGuid()).First() + affixes3.OrderBy(s => Guid.NewGuid()).First();
            suffixedFirstNames.Add(suffixedFirstName);
        }

        for (int i = 0; i < 16000; i++)
        {
            var prefixedLastName = affixes4.OrderBy(s => Guid.NewGuid()).First() + shortLastNames.OrderBy(s => Guid.NewGuid()).First();
            prefixedLastNames.Add(prefixedLastName);
        }
        
        for (int i = 0; i < 16000; i++)
        {
            var suffixedLastName = shortLastNames.OrderBy(s => Guid.NewGuid()).First() + affixes3.OrderBy(s => Guid.NewGuid()).First();
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