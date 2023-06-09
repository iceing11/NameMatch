namespace NameMatch;

public class SoundexPair
{
    private Person _person;
    private string _soundex;

    public SoundexPair(Person person, string soundex)
    {
        _person = person;
        _soundex = soundex;
    }

    public Person GetPerson()
    {
        return _person;
    }

    public string GetSoundex()
    {
        return _soundex;
    }
}