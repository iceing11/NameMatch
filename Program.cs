using System.Diagnostics;

namespace NameMatch
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Soundex soundex = new Soundex();

            string input = "สัมฤทธิ์ เตชะวงศ์ธรรม";
            int soundexCode = 1; // 0: Udom, 1: Prayut-Somchai
            string inputSoundex = soundex.GetSoundex(input.Split(' ')[0], soundexCode) + "/" + soundex.GetSoundex(input.Split(' ')[1], soundexCode);
            var outputThreshold = 0.8;

            Database database = new Database(@"E:\University\Internship\Name Match\Program\NameMatch\NameMatch\CombinedNames.csv", soundexCode);
            var comparator = new LevenshteinComparator();
            
            Console.WriteLine("Closest names to " + input + " (Soundex Code: " + inputSoundex + ") using " + comparator.GetName() + " is:");
            
            foreach (var result in database.GetClosestSoundexes(input, comparator, outputThreshold))
            {
                Console.WriteLine(result.ToString());
            }
            
            /*
            Stopwatch sw = new Stopwatch();

            for(int index = 0; index < 21; index++)
            {
                sw = Stopwatch.StartNew();
                database.GetClosestSoundexes(input, comparator, outputThreshold);
                Console.WriteLine(sw.ElapsedMilliseconds);
            }

            sw.Stop();
            
            */
        }
    }
}
