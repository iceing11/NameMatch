using System.Text.RegularExpressions;

namespace NameMatch;

public class Soundex
{
    char[] UdomTranslate1Value = "กขขขขขงจชชชสสสสดดตตททททททนนบปพพพฟฟมยยรรรรรวอฮฮ.".ToCharArray();
    char[] UdomTranslate1Key = "กขฃคฅฆงจฉชฌซศษสฎดฏตฐฑฒถทธณนบปผพภฝฟมญยรลฬฤฦวอหฮ.".ToCharArray();
    char[] UdomTranslate2Key = "มวำกขฃคฅฆงยญณนฎฏดตศษสบปพภผฝฟหอฮจฉชซฌฐฑฒถทธรฤลฬฦ".ToCharArray();
    char[] UdomTranslate2Value = "00011111122333444444455556666667777788888899999".ToCharArray();
    char[] PrayutC1 = "AEIOUHWYอBFPVบฝฟปผพภวCGJKQSXZขฃคฅฆฉขฌกจซศษสDTฎดฏตฐฑฒถทธLลฬMNมณนRร".ToCharArray();
    char[] _C0 = "AEIOUHWYอ".ToCharArray();
    char[] _C1 = "BFPVบฝฟปผพภว".ToCharArray();
    char[] _C2 = "CGJKQSXZขฃคฅฆฉขฌกจซศษส".ToCharArray();
    char[] _C3 = "DTฎดฏตฐฑฒถทธ".ToCharArray();
    char[] _C4 = "Lลฬ".ToCharArray();
    char[] _C5 = "MNมณน".ToCharArray();
    char[] _C6 = "Rร".ToCharArray();
    char[] _C7 = "AEIOUอ".ToCharArray();
    char[] _C8 = "Hหฮ".ToCharArray();
    char[] _C1_1 = "Wว".ToCharArray();
    char[] _C9 = "Yยญ".ToCharArray();
    char[] _C52 = "ง".ToCharArray();

    public string GetSoundex(string word, int code)
    {
        switch (code)
        {
            case 0: return GetSoundexByUdom(word);
            case 1: return GetSoundexByPS(word);
            default: return GetSoundexByUdom(word);
        }
    }

    //Generate Soundex using Udom83
    public string GetSoundexByUdom(string word)
        {
            string soundex = word;
            string result = "";

            Dictionary<char, char> translate1 = new Dictionary<char, char>();
            Dictionary<char, char> translate2 = new Dictionary<char, char>();

            for (int i = 0; i < UdomTranslate1Key.Length; i++)
            {
                translate1.Add(UdomTranslate1Key[i], UdomTranslate1Value[i]);
                translate2.Add(UdomTranslate2Key[i], UdomTranslate2Value[i]);
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

            /*
            while (result.Length < 6)
            {
                result += "0";
            }
            */

            return result;
        }
    
        //Generate Soundex using Prayut and SomchaiP
        public string GetSoundexByPS(string word)
        {
            string result = "";
            bool firstChar = true;

            foreach (var c in word.ToCharArray())
            {
                if (firstChar && _C0.Contains(c))
                {
                    result += "0";
                }
                else if (_C1.Contains(c))
                {
                    result += "1";
                }
                else if (_C2.Contains(c))
                {
                    result += "2";
                }
                else if (_C3.Contains(c))
                {
                    result += "3";
                }
                else if (_C4.Contains(c))
                {
                    result += "4";
                }
                else if (_C5.Contains(c))
                {
                    result += "5";
                }
                else if (_C6.Contains(c))
                {
                    result += "6";
                }
                else if (_C52.Contains(c))
                {
                    result += "52";
                }
                else if (_C7.Contains(c) && !firstChar)
                {
                    result += "7";
                }
                else if (_C8.Contains(c) && !firstChar)
                {
                    result += "8";
                }
                else if (_C1_1.Contains(c) && !firstChar)
                {
                    result += "1";
                }
                else if (_C9.Contains(c) && !firstChar)
                {
                    result += "9";
                }
            }
            
            return result;
        }
}