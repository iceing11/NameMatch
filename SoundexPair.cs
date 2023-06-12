namespace NameMatch;

public class SoundexPair
{
    private Person _person;
    private string _firstNameSoundex;
    private string _lastNameSoundex;

    public SoundexPair(Person person, string firstNameSoundex, string lastNameSoundex)
    {
        _person = person;
        _firstNameSoundex = firstNameSoundex;
        _lastNameSoundex = lastNameSoundex;
    }

    public Person GetPerson()
    {
        return _person;
    }

    public string GetFirstNameSoundex()
    {
        return _firstNameSoundex;
    }
    
    public string GetLastNameSoundex()
    {
        return _lastNameSoundex;
    }
}