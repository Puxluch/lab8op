using System.Collections;

namespace Classes;

public class UserList : IEnumerable<User>
{
    private List<User> users;

    public int Count => users.Count;

    public UserList()
    {
        users = new List<User>();
    }

    public User this[int index]
    {
        get
        {
            if (index < 0 || index >= users.Count) throw new IndexOutOfRangeException("Індекс виходить за межі.");
            return users[index];
        }
        set
        {
            ArgumentNullException.ThrowIfNull(value);
            if (index < 0 || index >= users.Count) throw new IndexOutOfRangeException("Індекс виходить за межі.");
            users[index] = value;
        }
    }

    public void AddUser(User user)
    {
        ArgumentNullException.ThrowIfNull(user);
        users.Add(user);
    }

    public void RemoveUser(User user)
    {
        ArgumentNullException.ThrowIfNull(user);
        if (!users.Remove(user)) throw new InvalidOperationException("Користувача не знайдено.");
    }

    public void SortByName() => users.Sort((x, y) => string.Compare(x.Name, y.Name, StringComparison.Ordinal));
    public void SortBySurname() => users.Sort((x, y) => string.Compare(x.Surname, y.Surname, StringComparison.Ordinal));
    public void SortByGroup() => users.Sort((x, y) => string.Compare(x.AcademicGroup, y.AcademicGroup, StringComparison.Ordinal));

    public List<User> SearchByKeyword(string keyword)
    {
        if (string.IsNullOrWhiteSpace(keyword)) return new List<User>();
        return users.Where(u => u.Name.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                                u.Surname.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                                u.AcademicGroup.Contains(keyword, StringComparison.OrdinalIgnoreCase)).ToList();
    }

    public IEnumerator<User> GetEnumerator() => users.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}