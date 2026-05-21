namespace Classes;

public abstract class Person
{
    public string Name { get; set; }
    public string Surname { get; set; }

    protected Person(string name, string surname)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Ім'я не може бути порожнім", nameof(name));
        }
        if (string.IsNullOrWhiteSpace(surname))
        {
            throw new ArgumentException("Прізвище не може бути порожнім", nameof(surname));
        }

            Name = name;
        Surname = surname;
    }
}