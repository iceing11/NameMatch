namespace NameMatch
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            string input = "พีรดล ศิริพรพีระ";
            string inputSoundex = Soundex.GetSoundex(input.Split(' ')[0]) + Soundex.GetSoundex(input.Split(' ')[1]);
            int outputCount = 15;

            Database database = new Database(@"E:\University\Internship\Name Match\Program\NameMatch\NameMatch\CombinedNames.csv");
            var comparator = new CosineComparator();
            
            Console.WriteLine(outputCount + " Closest names to " + input + " (Soundex Code: " + inputSoundex + ") using " + comparator.GetName() + " is:");

            foreach (var result in database.GetClosestNames(input, comparator, outputCount))
            {
                Console.WriteLine(result.ToString());
            }
        }
    }
}
