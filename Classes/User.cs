namespace Classes;

public class User : Person
{
    public string AcademicGroup { get; set; }

    // Композиція: користувач володіє власним читацьким формуляром
    public LibraryAccount Account { get; private set; }

    public User(string name, string surname, string academicGroup) : base(name, surname)
    {
        if (string.IsNullOrWhiteSpace(academicGroup)) throw new ArgumentException("Група не може бути порожньою.", nameof(academicGroup));

        AcademicGroup = academicGroup;
        Account = new LibraryAccount();
    }

    public override string ToString()
    {
        return $"Ім'я: {Name}, Прізвище: {Surname}, Група: {AcademicGroup}\nВзятих документів: {Account.BorrowedDocuments.Count}";
    }
}