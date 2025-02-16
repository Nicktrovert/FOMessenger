using Microsoft.AspNetCore.Components;
using System.ComponentModel;

namespace FOMessenger.Code.Uri
{
    public class QueryHandler
    {
        private Dictionary<string, string> _QueryMap;
        public Dictionary<string, string> QueryMap
        {
            get
            {
                return _QueryMap;
            }
            private set
            {
                _QueryMap = value;
            }
        }

        public dynamic this[string key]
        {
            get
            {
                if (DoesKeyExist(key.ToLower()))
                {
                    return GetString(key.ToLower());
                }
                else
                {
                    return null;
                }
            }
            private set
            {
                throw new InvalidOperationException();
            }
        }

        public QueryHandler() 
        {
            _QueryMap = new();
        }

        public bool DoesKeyExist(string key)
        {
            try
            {
                return QueryMap.ContainsKey(key.ToLower()) && QueryMap.TryGetValue(key.ToLower(), out _) && QueryMap[key.ToLower()] != null;
            }
            catch
            {
                return false;
            }
        }

        public T GetValue<T>(string key)
        {
            // Automatically gets the correct Converter for the specified type.
            // Not that safe since it gives an error if the converter doesn't exist or can't convert.
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(T));
            return (T)converter.ConvertFromString(QueryMap[key]);
        }

        public string GetString(string key)
        {
            if (QueryMap.TryGetValue(key.ToLower(), out string? value))
            {
                return value;
            }
            else
            {
                throw new KeyNotFoundException();
            }
        }

        public bool GetBoolean(string key)
        {
            if (QueryMap.TryGetValue(key.ToLower(), out string? value))
            {
                try
                {
                    // Try to convert for string ("true" || "false")
                    return Convert.ToBoolean(value.ToLower());
                }
                catch
                {
                    // Try to convert for integer (not 0 || 0)
                    return Convert.ToBoolean(Convert.ToInt32(value));
                }
            }
            else
            {
                throw new KeyNotFoundException();
            }
        }

        public decimal GetDecimal(string key)
        {
            if (QueryMap.TryGetValue(key.ToLower(), out string? value))
            {
                return Convert.ToDecimal(value);
            }
            else
            {
                throw new KeyNotFoundException();
            }
        }

        public float GetFloat(string key)
        {
            if (QueryMap.TryGetValue(key.ToLower(), out string? value))
            {
                return float.Parse(value);
            }
            else
            {
                throw new KeyNotFoundException();
            }
        }

        public double GetDouble(string key)
        {
            if (QueryMap.TryGetValue(key.ToLower(), out string? value))
            {
                return Convert.ToDouble(value);
            }
            else
            {
                throw new KeyNotFoundException();
            }
        }

        public Dictionary<string, string> Parse(string query, out Dictionary<string, string> output)
        {
            string[] splitQuery = query.Split('?', StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < splitQuery.Length; i++)
            {
                string[] keyValue = splitQuery[i].Split("=", StringSplitOptions.TrimEntries);
                string key = keyValue[0].ToLower();
                string value = keyValue[1];
                _QueryMap.Add(key, value);
            }
            output = QueryMap;
            return QueryMap;
        }

        public Dictionary<string, string> Parse(string query)
        {
            return Parse(query, out _);
        }

        public void Parse(System.Uri uri)
        {
            Parse(uri.Query);
        }
        public void Parse(NavigationManager navMan)
        {
            System.Uri uri = navMan.ToAbsoluteUri(navMan.Uri);
            Parse(uri);
        }
    }
}
