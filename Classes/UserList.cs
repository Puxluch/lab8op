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
            if (index < 0 || index >= users.Count)
            {
                throw new IndexOutOfRangeException("Індекс виходить за межі.");
            }
                return users[index];
        }
        set
        {
            ArgumentNullException.ThrowIfNull(value);
            if (index < 0 || index >= users.Count)
            {
                throw new IndexOutOfRangeException("Індекс виходить за межі.");
            }
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
        if (!users.Remove(user))
        {
            throw new InvalidOperationException("Користувача не знайдено.");
        }
        }

    public void SortByName()
    {
        users.Sort(delegate (User x, User y) {
            return StringComparer.CurrentCulture.Compare(x.Name, y.Name);
        });
    }

    public void SortBySurname()
    {
        users.Sort(delegate (User x, User y) {
            return StringComparer.CurrentCulture.Compare(x.Surname, y.Surname);
        });
    }

    public void SortByGroup()
    {
        users.Sort(delegate (User x, User y) {
            return StringComparer.CurrentCulture.Compare(x.AcademicGroup, y.AcademicGroup);
        });
    }

    public List<User> SearchByKeyword(string keyword)
    {
        if (string.IsNullOrWhiteSpace(keyword))
        {
            return new List<User>();
        }
            return users.Where(u => u.Name.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                                u.Surname.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                                u.AcademicGroup.Contains(keyword, StringComparison.OrdinalIgnoreCase)).ToList();
    }

    public IEnumerator<User> GetEnumerator()
    {
        return users.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}