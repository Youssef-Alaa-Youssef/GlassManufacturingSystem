namespace Factory.PL.Helper
{
    public static class ConnectionStringBuilder
    {
        public static string Build(string baseConnectionString, string factoryName, string domain)
        {
            var databaseName = $"{domain.Replace(".", "_")}_{factoryName}";
            return baseConnectionString.Replace("{DatabaseName}", databaseName);
        }
    }
}
