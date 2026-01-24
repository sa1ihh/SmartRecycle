namespace SmartRecycle
{
    public class AuthService
    {
        private readonly UserStore _store;

        public AuthService(UserStore store)
        {
            _store = store;
        }

        public bool Validate(string username, string password)
        {
            return _store.Validate(username, password);
        }
    }
}