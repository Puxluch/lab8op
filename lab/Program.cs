using Classes;

namespace ClassesAndRelationships;

internal class Program
{
    private static Library library = new Library();

    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.InputEncoding = System.Text.Encoding.UTF8;

        // Додамо кілька початкових даних для зручності тестування
        SeedData();

        bool running = true;
        while (running)
        {
            Console.Clear();
            Console.WriteLine("==================================================");
            Console.WriteLine("   СИСТЕМА ОБЛІКУ СТУДЕНТСЬКОЇ БІБЛІОТЕКИ       ");
            Console.WriteLine("==================================================");
            Console.WriteLine("1. Вивести всіх користувачів (читачів)");
            Console.WriteLine("2. Вивести весь каталог документів (книг)");
            Console.WriteLine("3. Додати нового користувача");
            Console.WriteLine("4. Видалити користувача");
            Console.WriteLine("5. Додати новий документ до каталогу");
            Console.WriteLine("6. Видалити документ з каталогу");
            Console.WriteLine("7. Видати документ користувачу (Взяти книгу)");
            Console.WriteLine("8. Повернути документ до бібліотеки");
            Console.WriteLine("9. Пошук документів за ключовим словом");
            Console.WriteLine("10. Сортувати користувачів за групами");
            Console.WriteLine("0. Вихід з програми");
            Console.WriteLine("==================================================");
            Console.Write("Оберіть дію: ");

            string? choice = Console.ReadLine();
            Console.WriteLine();

            try
            {
                switch (choice)
                {
                    case "1":
                        ShowUsers();
                        break;
                    case "2":
                        ShowDocuments();
                        break;
                    case "3":
                        AddUser();
                        break;
                    case "4":
                        RemoveUser();
                        break;
                    case "5":
                        AddDocument();
                        break;
                    case "6":
                        RemoveDocument();
                        break;
                    case "7":
                        BorrowDocument();
                        break;
                    case "8":
                        ReturnDocument();
                        break;
                    case "9":
                        SearchDocuments();
                        break;
                    case "10":
                        SortUsers();
                        break;
                    case "0":
                        running = false;
                        Console.WriteLine("Програма завершує роботу. Гарного дня!");
                        break;
                    default:
                        Console.WriteLine("Некоректний вибір. Спробуйте ще раз.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Помилка: {ex.Message}");
                Console.ResetColor();
            }

            if (running)
            {
                Console.WriteLine("\nНатисніть будь-яку клавішу для повернення до меню...");
                Console.ReadKey();
            }
        }
    }

    private static void SeedData()
    {
        library.Users.AddUser(new User("Іван", "Іванов", "ІП-53"));
        library.Users.AddUser(new User("Анна", "Шевченко", "ІП-53"));

        library.Documents.AddDocument(new Document("C# in Depth", "Jon Skeet"));
        library.Documents.AddDocument(new Document("Clean Code", "Robert Martin"));
        library.Documents.AddDocument(new Document("Design Patterns", "Erich Gamma"));
    }

    private static void ShowUsers()
    {
        Console.WriteLine("--- СПИСОК КОРИСТУВАЧІВ ---");
        if (library.Users.Count == 0)
        {
            Console.WriteLine("У системі немає зареєстрованих користувачів.");
            return;
        }

        for (int i = 0; i < library.Users.Count; i++)
        {
            Console.WriteLine($"[{i + 1}] {library.Users[i].Surname} {library.Users[i].Name} (Група: {library.Users[i].AcademicGroup}) | Взято книг: {library.Users[i].Account.BorrowedDocuments.Count}");
        }
    }

    private static void ShowDocuments()
    {
        Console.WriteLine("--- КАТАЛОГ ДОКУМЕНТІВ ---");
        if (library.Documents.Count == 0)
        {
            Console.WriteLine("Каталог бібліотеки порожній.");
            return;
        }

        for (int i = 0; i < library.Documents.Count; i++)
        {
            var doc = library.Documents[i];
            string status = doc.Borrower != null ? $"Видано: {doc.Borrower.Surname} {doc.Borrower.Name}" : "В наявності";
            Console.WriteLine($"[{i + 1}] '{doc.Title}' - {doc.Author} ({status})");
        }
    }

    private static void AddUser()
    {
        Console.WriteLine("--- РЕЄСТРАЦІЯ НОВОГО КОРИСТУВАЧА ---");
        Console.Write("Введіть ім'я: ");
        string name = Console.ReadLine() ?? "";
        Console.Write("Введіть прізвище: ");
        string surname = Console.ReadLine() ?? "";
        Console.Write("Введіть академічну групу: ");
        string group = Console.ReadLine() ?? "";

        User newUser = new User(name, surname, group);
        library.Users.AddUser(newUser);
        Console.WriteLine($"Користувач {surname} {name} успішно доданий.");
    }

    private static void RemoveUser()
    {
        Console.WriteLine("--- ВИДАЛЕННЯ КОРИСТУВАЧА ---");
        ShowUsers();
        if (library.Users.Count == 0) return;

        Console.Write("\nВведіть номер користувача для видалення: ");
        if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= library.Users.Count)
        {
            User targetUser = library.Users[index - 1];
            if (targetUser.Account.BorrowedDocuments.Count > 0)
            {
                throw new InvalidOperationException("Неможливо видалити користувача, поки він не поверне всі книги!");
            }
            library.Users.RemoveUser(targetUser);
            Console.WriteLine("Користувача успішно видалено.");
        }
        else
        {
            Console.WriteLine("Некоректний номер.");
        }
    }

    private static void AddDocument()
    {
        Console.WriteLine("--- ДОДАВАННЯ ДОКУМЕНТА В КАТАЛОГ ---");
        Console.Write("Введіть назву книги: ");
        string title = Console.ReadLine() ?? "";
        Console.Write("Введіть автора: ");
        string author = Console.ReadLine() ?? "";

        Document newDoc = new Document(title, author);
        library.Documents.AddDocument(newDoc);
        Console.WriteLine($"Документ '{title}' успішно додано до каталогу.");
    }

    private static void RemoveDocument()
    {
        Console.WriteLine("--- ВИДАЛЕННЯ ДОКУМЕНТА З КАТАЛОГУ ---");
        ShowDocuments();
        if (library.Documents.Count == 0) return;

        Console.Write("\nВведіть номер документа для видалення: ");
        if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= library.Documents.Count)
        {
            Document targetDoc = library.Documents[index - 1];
            if (targetDoc.Borrower != null)
            {
                throw new InvalidOperationException("Неможливо видалити документ, який зараз на руках у читача!");
            }
            library.Documents.RemoveDocument(targetDoc);
            Console.WriteLine("Документ успішно видалено з каталогу.");
        }
        else
        {
            Console.WriteLine("Некоректний номер.");
        }
    }

    private static void BorrowDocument()
    {
        Console.WriteLine("--- ВИДАЧА КНИГИ ---");
        ShowUsers();
        if (library.Users.Count == 0) return;
        Console.Write("\nОберіть номер користувача: ");
        if (!int.TryParse(Console.ReadLine(), out int userIdx) || userIdx <= 0 || userIdx > library.Users.Count)
        {
            Console.WriteLine("Некоректний номер користувача.");
            return;
        }
        User user = library.Users[userIdx - 1];

        Console.WriteLine();
        ShowDocuments();
        if (library.Documents.Count == 0) return;
        Console.Write("\nОберіть номер документа, який хоче взяти користувач: ");
        if (!int.TryParse(Console.ReadLine(), out int docIdx) || docIdx <= 0 || docIdx > library.Documents.Count)
        {
            Console.WriteLine("Некоректний номер документа.");
            return;
        }
        Document doc = library.Documents[docIdx - 1];

        // Здійснюємо видачу
        user.Account.BorrowDocument(doc, user);
        Console.WriteLine($"Успішно! Користувач {user.Surname} взяв книгу '{doc.Title}'.");
    }

    private static void ReturnDocument()
    {
        Console.WriteLine("--- ПОВЕРНЕННЯ КНИГИ ---");
        ShowUsers();
        if (library.Users.Count == 0) return;
        Console.Write("\nОберіть номер користувача, який повертає книгу: ");
        if (!int.TryParse(Console.ReadLine(), out int userIdx) || userIdx <= 0 || userIdx > library.Users.Count)
        {
            Console.WriteLine("Некоректний номер.");
            return;
        }
        User user = library.Users[userIdx - 1];

        if (user.Account.BorrowedDocuments.Count == 0)
        {
            Console.WriteLine("У цього користувача немає взятих книг.");
            return;
        }

        Console.WriteLine($"\nКниги на руках у користувача {user.Surname}:");
        for (int i = 0; i < user.Account.BorrowedDocuments.Count; i++)
        {
            Console.WriteLine($"[{i + 1}] '{user.Account.BorrowedDocuments[i].Title}'");
        }

        Console.Write("Оберіть номер книги для повернення: ");
        if (!int.TryParse(Console.ReadLine(), out int bookIdx) || bookIdx <= 0 || bookIdx > user.Account.BorrowedDocuments.Count)
        {
            Console.WriteLine("Некоректний номер книги.");
            return;
        }

        Document docToReturn = user.Account.BorrowedDocuments[bookIdx - 1];
        user.Account.ReturnDocument(docToReturn);
        Console.WriteLine($"Книга '{docToReturn.Title}' успішно повернута в бібліотеку.");
    }

    private static void SearchDocuments()
    {
        Console.WriteLine("--- ПОШУК ДОКУМЕНТІВ ---");
        Console.Write("Введіть слово для пошуку (в назві або авторі): ");
        string keyword = Console.ReadLine() ?? "";

        var results = library.Documents.SearchByKeyword(keyword);
        Console.WriteLine($"\nЗнайдено документів: {results.Count}");
        foreach (var doc in results)
        {
            string status = doc.Borrower != null ? $"Видано: {doc.Borrower.Surname}" : "В наявності";
            Console.WriteLine($"- '{doc.Title}' від {doc.Author} [{status}]");
        }
    }

    private static void SortUsers()
    {
        library.Users.SortByGroup();
        Console.WriteLine("Користувачі успішно відсортовані за академічними групами.");
        ShowUsers();
    }
}