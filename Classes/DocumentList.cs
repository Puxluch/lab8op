using System.Collections;

namespace Classes;

public class DocumentList : IEnumerable<Document>
{
    private List<Document> documents;

    public int Count => documents.Count;

    public DocumentList()
    {
        documents = new List<Document>();
    }

    public Document this[int index]
    {
        get
        {
            if (index < 0 || index >= documents.Count)
            {
                throw new IndexOutOfRangeException("Індекс виходить за межі");
            }
                return documents[index];
        }
        set
        {
            ArgumentNullException.ThrowIfNull(value);
            if (index < 0 || index >= documents.Count)
            {
                throw new IndexOutOfRangeException("Індекс виходить за межі");
            }
                documents[index] = value;
        }
    }

    public void AddDocument(Document document)
    {
        ArgumentNullException.ThrowIfNull(document);
        documents.Add(document);
    }

    public void RemoveDocument(Document document)
    {
        ArgumentNullException.ThrowIfNull(document);
        if (!documents.Remove(document))
        {
            throw new InvalidOperationException("Документ не знайдено");
        }
        }

    public void SortByTitle()
    {
        documents.Sort(delegate (Document x, Document y) {
            return StringComparer.CurrentCulture.Compare(x.Title, y.Title);
        });
    }

    public void SortByAuthor()
    {
        documents.Sort(delegate (Document x, Document y) {
            return StringComparer.CurrentCulture.Compare(x.Author, y.Author);
        });
    }

    public List<Document> SearchByKeyword(string keyword)
    {
        if (string.IsNullOrWhiteSpace(keyword)) return new List<Document>();
        return documents.Where(d => d.Title.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                                    d.Author.Contains(keyword, StringComparison.OrdinalIgnoreCase)).ToList();
    }

    public IEnumerator<Document> GetEnumerator()
    {
        return documents.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}