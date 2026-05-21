namespace Classes;

public class LibraryAccount
{
    private List<Document> borrowedDocuments;
    private const int MaxDocuments = 5;

    public IReadOnlyList<Document> BorrowedDocuments
    {
        get
        {
            return borrowedDocuments.AsReadOnly();
        }
    }

    public LibraryAccount()
    {
        borrowedDocuments = new List<Document>();
    }

    public void BorrowDocument(Document document, User user)
    {
        ArgumentNullException.ThrowIfNull(document);

        if (borrowedDocuments.Count >= MaxDocuments)
        {
            throw new InvalidOperationException($"Користувач не може взяти більше ніж {MaxDocuments} документів");
        }
        if (document.Borrower != null)
        {
            throw new InvalidOperationException("Цей документ вже видано іншому користувачу");
        }

        borrowedDocuments.Add(document);
        document.Borrower = user;
    }

    public void ReturnDocument(Document document)
    {
        ArgumentNullException.ThrowIfNull(document);

        if (borrowedDocuments.Remove(document))
        {
            document.Borrower = null;
        }
        else
        {
            throw new InvalidOperationException("Цей документ не знаходиться на рахунку даного користувача");
        }
    }
}