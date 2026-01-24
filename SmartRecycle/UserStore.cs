using System.Collections.Generic;
using System.Linq;

namespace SmartRecycle
{
    public class UserStore
    {
        private readonly List<User> _users = new();

        public UserStore()
        {
            // Demo kullanıcılar
            _users.Add(new User("admin", "1234"));
            _users.Add(new User("furkan", "1234"));
        }

        public User? FindByUsername(string username)
        {
            return _users.FirstOrDefault(u => u.Username == username);
        }

        public bool Validate(string username, string password)
        {
            var u = FindByUsername(username);
            return u != null && u.Password == password;
        }
    }
}