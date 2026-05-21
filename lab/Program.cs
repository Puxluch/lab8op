using Classes;

namespace ClassesAndRelationships;

internal class Program
{
    private static Library library = new Library();

    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.InputEncoding = System.Text.Encoding.UTF8;

        SeedData();

        bool running = true;
        while (running)
        {
            Console.Clear();
            Console.WriteLine("Студентська бібліотека\n");
            Console.WriteLine("1 - Вивести список всіх користувачів");
            Console.WriteLine("2 - Переглянути дані конкретного користувача");
            Console.WriteLine("3 - Додати нового користувача");
            Console.WriteLine("4 - Змінити дані користувача");
            Console.WriteLine("5 - Видалити користувача");
            Console.WriteLine("6 - Сортувати користувачів за ім'ям");
            Console.WriteLine("7 - Сортувати користувачів за прізвищем");
            Console.WriteLine("8 - Сортувати користувачів за групою");
            Console.WriteLine("9 - Пошук серед користувачів за ключовим словом\n");
            Console.WriteLine("10 - Вивести весь каталог документів");
            Console.WriteLine("11 - Переглянути дані конкретного документу");
            Console.WriteLine("12 - Додати новий документ");
            Console.WriteLine("13 - Змінити дані документу");
            Console.WriteLine("14 - Видалити документ з каталогу");
            Console.WriteLine("15 - Сортувати документи за НАЗВОЮ");
            Console.WriteLine("16 - Сортувати документи за АВТОРОМ");
            Console.WriteLine("17 - Пошук серед документів за ключовим словом\n");
            Console.WriteLine("18 - Видати документ користувачу");
            Console.WriteLine("19 - Повернути книжку в бібліотеку");
            Console.WriteLine("20 - Перевірити статус документа");
            Console.WriteLine("21 - Переглянути книги конкретного користувача");
            Console.Write("Оберіть дію: ");

            string choice = Console.ReadLine();
            if (choice == null) choice = "";
            Console.WriteLine();

            try
            {
                switch (choice)
                {
                    case "1": ShowUsers(); break;
                    case "2": ShowSpecificUser(); break;
                    case "3": AddUser(); break;
                    case "4": EditUser(); break;
                    case "5": RemoveUser(); break;
                    case "6": library.Users.SortByName(); Console.WriteLine("Відсортовано за іменем"); ShowUsers(); break;
                    case "7": library.Users.SortBySurname(); Console.WriteLine("Відсортовано за прізвищем"); ShowUsers(); break;
                    case "8": library.Users.SortByGroup(); Console.WriteLine("Відсортовано за групою"); ShowUsers(); break;
                    case "9": SearchUsers(); break;
                    case "10": ShowDocuments(); break;
                    case "11": ShowSpecificDocument(); break;
                    case "12": AddDocument(); break;
                    case "13": EditDocument(); break;
                    case "14": RemoveDocument(); break;
                    case "15": library.Documents.SortByTitle(); Console.WriteLine("Відсортовано за назвою"); ShowDocuments(); break;
                    case "16": library.Documents.SortByAuthor(); Console.WriteLine("Відсортовано за автором"); ShowDocuments(); break;
                    case "17": SearchDocuments(); break;
                    case "18": BorrowDocument(); break;
                    case "19": ReturnDocument(); break;
                    case "20": CheckBookStatusDirectly(); break;
                    case "21": ShowUserBooks(); break;
                    default:
                        Console.WriteLine("Некоректний вибір");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Помилка: " + ex.Message);
                Console.ResetColor();
            }

            if (running)
            {
                Console.WriteLine("\nНатисніть будь-яку клавішу");
                Console.ReadKey();
            }
        }
    }

    private static void SeedData()
    {
        library.Users.AddUser(new User("Іван", "Мельник", "ІП-53"));
        library.Users.AddUser(new User("Анна", "Шевченко", "ІП-54"));
        library.Documents.AddDocument(new Document("book1", "author1"));
        library.Documents.AddDocument(new Document("book2", "author2"));
    }

    private static void ShowUsers()
    {
        Console.WriteLine("Список користувачів");
        if (library.Users.Count == 0) { Console.WriteLine("Користувачів немає"); return; }
        for (int i = 0; i < library.Users.Count; i++)
        {
            User u = library.Users[i];
            Console.WriteLine("[" + (i + 1) + "] " + u.Surname + " " + u.Name + " (" + u.AcademicGroup + ")");
        }
    }

    private static void ShowSpecificUser()
    {
        ShowUsers();
        if (library.Users.Count == 0) return;
        Console.Write("\nВведіть номер користувача для перегляду - ");
        if (int.TryParse(Console.ReadLine(), out int idx) && idx > 0 && idx <= library.Users.Count)
        {
            User u = library.Users[idx - 1];
            Console.WriteLine("\nЧитач");
            Console.WriteLine("Прізвище - " + u.Surname);
            Console.WriteLine("Ім'я -- " + u.Name);
            Console.WriteLine("Група - " + u.AcademicGroup);
            Console.WriteLine("Кількість книг на руках - " + u.Account.BorrowedDocuments.Count + "/5");
        }
    }

    private static void AddUser()
    {
        Console.Write("Введіть ім'я - "); string name = Console.ReadLine() ?? "";
        Console.Write("Введіть прізвище - "); string surname = Console.ReadLine() ?? "";
        Console.Write("Введіть групу - "); string group = Console.ReadLine() ?? "";
        library.Users.AddUser(new User(name, surname, group));
        Console.WriteLine("Користувача додано");
    }

    private static void EditUser()
    {
        ShowUsers();
        if (library.Users.Count == 0) return;
        Console.Write("\nОберіть номер користувача для редагування  ");
        if (int.TryParse(Console.ReadLine(), out int idx) && idx > 0 && idx <= library.Users.Count)
        {
            User u = library.Users[idx - 1];
            Console.Write("Нове ім'я (залишити порожнім для збереження [" + u.Name + "]): ");
            string name = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(name)) u.Name = name;

            Console.Write("Нове прізвище (залишити порожнім для збереження [" + u.Surname + "]): ");
            string surname = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(surname)) u.Surname = surname;

            Console.Write("Нова група (залишити порожнім для збереження [" + u.AcademicGroup + "]): ");
            string group = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(group)) u.AcademicGroup = group;

            Console.WriteLine("Даноновлено");
        }
    }

    private static void RemoveUser()
    {
        ShowUsers();
        if (library.Users.Count == 0) return;
        Console.Write("\nВведіть номер для видалення: ");
        if (int.TryParse(Console.ReadLine(), out int idx) && idx > 0 && idx <= library.Users.Count)
        {
            User u = library.Users[idx - 1];
            if (u.Account.BorrowedDocuments.Count > 0) throw new InvalidOperationException("Поверніть спочатку книги!");
            library.Users.RemoveUser(u);
            Console.WriteLine("Користувача видалено.");
        }
    }

    private static void SearchUsers()
    {
        Console.Write("Ключове слово для пошуку читача: ");
        string kw = Console.ReadLine() ?? "";
        List<User> res = library.Users.SearchByKeyword(kw);
        Console.WriteLine("Знайдено користувачів: " + res.Count);
        foreach (User u in res) Console.WriteLine("- " + u.Surname + " " + u.Name + " (" + u.AcademicGroup + ")");
    }

    private static void ShowDocuments()
    {
        Console.WriteLine("Каталог документів");
        if (library.Documents.Count == 0) { Console.WriteLine("Каталог порожній"); return; }
        for (int i = 0; i < library.Documents.Count; i++)
        {
            Console.WriteLine("[" + (i + 1) + "] '" + library.Documents[i].Title + "' - " + library.Documents[i].Author);
        }
    }

    private static void ShowSpecificDocument()
    {
        ShowDocuments();
        if (library.Documents.Count == 0) return;
        Console.Write("\nВведіть номер документу для перегляду - ");
        if (int.TryParse(Console.ReadLine(), out int idx) && idx > 0 && idx <= library.Documents.Count)
        {
            Document d = library.Documents[idx - 1];
            Console.WriteLine("\nІнформація про докуменнт");
            Console.WriteLine("Назва: " + d.Title);
            Console.WriteLine("Автор: " + d.Author);
            Console.WriteLine("Статус: " + library.GetDocumentStatus(d));
        }
    }

    private static void AddDocument()
    {
        Console.Write("Назва книги: "); string t = Console.ReadLine() ?? "";
        Console.Write("Автор: "); string a = Console.ReadLine() ?? "";
        library.Documents.AddDocument(new Document(t, a));
        Console.WriteLine("Документ додано");
    }

    private static void EditDocument()
    {
        ShowDocuments();
        if (library.Documents.Count == 0) return;
        Console.Write("\nОберіть номер документу для редагування - ");
        if (int.TryParse(Console.ReadLine(), out int idx) && idx > 0 && idx <= library.Documents.Count)
        {
            Document d = library.Documents[idx - 1];
            Console.Write("Нова назва (залишити порожньою [" + d.Title + "]): ");
            string t = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(t)) d.Title = t;

            Console.Write("Новий автор (залишити порожнім [" + d.Author + "]): ");
            string a = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(a)) d.Author = a;

            Console.WriteLine("Дані документу оновлено");
        }
    }

    private static void RemoveDocument()
    {
        ShowDocuments();
        if (library.Documents.Count == 0) return;
        Console.Write("\nВведіть номер для видалення з каталогу - ");
        if (int.TryParse(Console.ReadLine(), out int idx) && idx > 0 && idx <= library.Documents.Count)
        {
            Document d = library.Documents[idx - 1];
            if (d.Borrower != null) throw new InvalidOperationException("Книга на руках у читач");
            library.Documents.RemoveDocument(d);
            Console.WriteLine("Документ видалено");
        }
    }

    private static void SearchDocuments()
    {
        Console.Write("Ключове слово для пошуку книги - ");
        string kw = Console.ReadLine() ?? "";
        List<Document> res = library.Documents.SearchByKeyword(kw);
        Console.WriteLine("Знайдено книг: " + res.Count);
        foreach (Document d in res) Console.WriteLine("- '" + d.Title + "' (" + d.Author + ")");
    }

    private static void BorrowDocument()
    {
        ShowUsers(); if (library.Users.Count == 0) return;
        Console.Write("Номер користувача: ");
        if (int.TryParse(Console.ReadLine(), out int uIdx) && uIdx > 0 && uIdx <= library.Users.Count)
        {
            ShowDocuments(); if (library.Documents.Count == 0) return;
            Console.Write("Номер книги: ");
            if (int.TryParse(Console.ReadLine(), out int dIdx) && dIdx > 0 && dIdx <= library.Documents.Count)
            {
                library.Users[uIdx - 1].Account.BorrowDocument(library.Documents[dIdx - 1], library.Users[uIdx - 1]);
                Console.WriteLine("Книгу видано");
            }
        }
    }

    private static void ReturnDocument()
    {
        ShowUsers(); if (library.Users.Count == 0) return;
        Console.Write("Номер користувача, який повертає - ");
        if (int.TryParse(Console.ReadLine(), out int uIdx) && uIdx > 0 && uIdx <= library.Users.Count)
        {
            User u = library.Users[uIdx - 1];
            if (u.Account.BorrowedDocuments.Count == 0) { Console.WriteLine("У користувача немає книг."); return; }
            for (int i = 0; i < u.Account.BorrowedDocuments.Count; i++)
                Console.WriteLine("[" + (i + 1) + "] " + u.Account.BorrowedDocuments[i].Title);
            Console.Write("Яку книгу повернути? ");
            if (int.TryParse(Console.ReadLine(), out int bIdx) && bIdx > 0 && bIdx <= u.Account.BorrowedDocuments.Count)
            {
                u.Account.ReturnDocument(u.Account.BorrowedDocuments[bIdx - 1]);
                Console.WriteLine("Книгу повернуто.");
            }
        }
    }

    private static void CheckBookStatusDirectly()
    {
        ShowDocuments();
        if (library.Documents.Count == 0) return;
        Console.Write("\nОберіть номер книги для перевірки її знаходження - ");
        if (int.TryParse(Console.ReadLine(), out int idx) && idx > 0 && idx <= library.Documents.Count)
        {
            Console.WriteLine(library.GetDocumentStatus(library.Documents[idx - 1]));
        }
    }

    private static void ShowUserBooks()
    {
        ShowUsers();
        if (library.Users.Count == 0) return;
        Console.Write("\nОберіть номер користувача для перегляду його формуляра - ");
        if (int.TryParse(Console.ReadLine(), out int idx) && idx > 0 && idx <= library.Users.Count)
        {
            User u = library.Users[idx - 1];
            Console.WriteLine("\nДокументи на руках у " + u.Surname + " " + u.Name + ":");
            if (u.Account.BorrowedDocuments.Count == 0) Console.WriteLine("Нічого немає");
            for (int i = 0; i < u.Account.BorrowedDocuments.Count; i++)
            {
                Console.WriteLine("- '" + u.Account.BorrowedDocuments[i].Title + "' (" + u.Account.BorrowedDocuments[i].Author + ")");
            }
        }
    }
}