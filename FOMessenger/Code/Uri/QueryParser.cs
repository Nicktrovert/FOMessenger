namespace FOMessenger.Code.Uri
{
    public static class QueryParser
    {
        public static Dictionary<string, string> ParseQuery(string query)
        {
            Dictionary<string, string> KeyValueQuery = new Dictionary<string, string>();
            string[] splitQuery = query.Split('?', StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < splitQuery.Length; i++)
            {
                string[] keyValue = splitQuery[i].Split("=", StringSplitOptions.TrimEntries);
                string key = keyValue[0].ToLower();
                string value = keyValue[1];
                KeyValueQuery.Add(key, value);
            }
            return KeyValueQuery;
        }
    }
}
