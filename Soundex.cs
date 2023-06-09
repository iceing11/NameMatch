using System.Text.RegularExpressions;

namespace NameMatch;

public static class Soundex
{
    //Generate Soundex using Udom83
    public static string GetSoundex(string word)
        {
            string soundex = word;
            string result = "";
            char[] translate1Key = "กขฃคฅฆงจฉชฌซศษสฎดฏตฐฑฒถทธณนบปผพภฝฟมญยรลฬฤฦวอหฮ.".ToCharArray();
            char[] translate1Value = "กขขขขขงจชชชสสสสดดตตททททททนนบปพพพฟฟมยยรรรรรวอฮฮ.".ToCharArray();
            char[] translate2Key = "มวำกขฃคฅฆงยญณนฎฏดตศษสบปพภผฝฟหอฮจฉชซฌฐฑฒถทธรฤลฬฦ".ToCharArray();
            char[] translate2Value = "00011111122333444444455556666667777788888899999".ToCharArray();

            Dictionary<char, char> translate1 = new Dictionary<char, char>();
            Dictionary<char, char> translate2 = new Dictionary<char, char>();

            for (int i = 0; i < translate1Key.Length; i++)
            {
                translate1.Add(translate1Key[i], translate1Value[i]);
                translate2.Add(translate2Key[i], translate2Value[i]);
            }
            
            soundex = Regex.Replace(soundex, "รร([เ-ไ])", "ัน$1"); //4
            soundex = Regex.Replace(soundex, "รร([ก-ฮ][ก-ฮเ-ไ])", "ั$1"); //5
            soundex = Regex.Replace(soundex, "รร([ก-ฮ][ะ-ู่-์])", "ัน$1");
            soundex = Regex.Replace(soundex, "รร", "ัน"); //1
            soundex = Regex.Replace(soundex, "ํ", "ัน"); //_ํ
            soundex = Regex.Replace(soundex, "ไ([ก-ฮ]ย)", "$1"); //2
            soundex = Regex.Replace(soundex, "[ไใ]([ก-ฮ])", "$1ย");
            soundex = Regex.Replace(soundex, "ำ(ม[ะ-ู])", "ม$1"); //3
            soundex = Regex.Replace(soundex, "ำม", "ม");
            soundex = Regex.Replace(soundex, "ำ", "ม");
            soundex = Regex.Replace(soundex, "จน์|มณ์|ณฑ์|ทร์|ตร์|[ก-ฮ]์|[ก-ฮ][ะ-ู]์", ""); //6
            soundex = Regex.Replace(soundex, "[ะ-์]", ""); //7
            soundex = Regex.Replace(soundex, "\\s*ณ\\s+", "ณ"); //ณ
            soundex = Regex.Replace(soundex, "ป.", "ปอ"); //ป.

            //Console.WriteLine(soundex);
            
            bool firstLetter = true;
            
            foreach (char c in soundex.ToCharArray())
            {
                char add;
                
                if (firstLetter)
                {
                    add = translate1[c];
                    firstLetter = false;
                }
                else
                {
                    add = translate2[c];
                }

                result += add;
            }

            while (result.Length < 9)
            {
                result += "0";
            }

            return result;
        }
}