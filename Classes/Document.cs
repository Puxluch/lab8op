namespace Classes;

public class Document : IDocument
{
    public string Title { get; set; }
    public string Author { get; set; }

    // Асоціація: документ знає читача, який його взяв
    public User? Borrower { get; set; }

    public Document(string title, string author)
    {
        if (string.IsNullOrWhiteSpace(title)) throw new ArgumentException("Назва не може бути порожньою.", nameof(title));
        if (string.IsNullOrWhiteSpace(author)) throw new ArgumentException("Автор не може бути порожнім.", nameof(author));

        Title = title;
        Author = author;
        Borrower = null;
    }

    public override string ToString()
    {
        return $"Назва: '{Title}', Автор: '{Author}'" + (Borrower != null ? $" (Взято: {Borrower.Name} {Borrower.Surname})" : " (Доступно)");
    }
}