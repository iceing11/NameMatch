namespace NameMatch
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            string input = "เพชรวุวรรณ์ญลา เกตแก้วทองวดี";

            Database database = new Database(@"E:\University\Internship\Name Match\Program\NameMatch\NameMatch\CombinedNames.csv");
            
            Console.WriteLine("Closest name to " + input + " (Soundex Code: " + Soundex.GetSoundex(input.Split(' ')[0]) + Soundex.GetSoundex(input.Split(' ')[1]) + ") is:");
            Console.WriteLine(database.GetClosestName(input).ToString());
        }
    }
}
