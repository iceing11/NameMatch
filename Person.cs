namespace NameMatch;

public class Person
{
    private string _firstName;
    private string _lastName;
    
    public Person(string firstName, string lastName)
    {
        _firstName = firstName;
        _lastName = lastName;
    }

    public string GetFirstName()
    {
        return _firstName;
    }

    public string GetLastName()
    {
        return _lastName;
    }

    public string GetFullName()
    {
        return _firstName + _lastName;
    }
    public string GetFullName(string delimiter)
    {
        return _firstName + delimiter + _lastName;
    }

    public void ChangeName(string firstName, string lastName)
    {
        _firstName = firstName;
        _lastName = lastName;
    }
}