namespace ManageEventBackend.Applications.Responses
{
    public class MapperPattern<T> where T : class, new()
    {
        private static Lazy<T> _instance = new Lazy<T>(() => new T());
        public static T Instance => _instance.Value;
    }
}
