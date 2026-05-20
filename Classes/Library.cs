namespace Classes;

public class Library
{
    // Агрегація списків у головному класі
    public UserList Users { get; private set; }
    public DocumentList Documents { get; private set; }

    public Library()
    {
        Users = new UserList();
        Documents = new DocumentList();
    }

    public string GetDocumentStatus(Document document)
    {
        ArgumentNullException.ThrowIfNull(document);

        bool exists = false;
        foreach (var doc in Documents)
        {
            if (doc == document)
            {
                exists = true;
                break;
            }
        }

        if (!exists)
        {
            return "Цей документ не знайдено у каталозі бібліотеки.";
        }

        if (document.Borrower != null)
        {
            return $"Документ '{document.Title}' виданий: {document.Borrower.Name} {document.Borrower.Surname}.";
        }

        return $"Документ '{document.Title}' є в наявності.";
    }
}