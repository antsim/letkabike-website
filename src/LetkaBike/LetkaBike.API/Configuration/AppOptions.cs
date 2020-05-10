namespace LetkaBike.API.Configuration
{
    public class AppOptions
    {
        public UseDb UseDb { get; set; }
        public string AuthSecret { get; set; }
    }

    public enum UseDb
    {
        None = 0,
        InMemory = 1,
        Sqlite = 2,
        SqlServer = 3
    }
}