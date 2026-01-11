using System.Collections.Generic;

namespace lib_utilities.Utilities
{
    public interface ICacheHelper
    {
        void Add(string key, object value);
        void Instance();
        bool Contains(string key);
        object? Get(string key);
        void Remove(string key);
    }

    public class CacheDictionary : ICacheHelper
    {
        private Dictionary<string, object>? data;

        public void Add(string key, object value)
        {
            Instance();
            data![key] = value;
        }

        public bool Contains(string key)
        {
            Instance();
            return data!.ContainsKey(key);
        }

        public object? Get(string key)
        {
            Instance();
            if (!Contains(key))
                return null;
            return data![key];
        }

        public void Instance()
        {
            if (data != null)
                return;
            data = new Dictionary<string, object>();
        }

        public void Remove(string key)
        {
            Instance();
            if (!Contains(key))
                return;
            data!.Remove(key);
        }
    }

    public class CacheHelper
    {
        private static ICacheHelper? ICacheHelper;

        public static void Add(string key, object value)
        {
            CreateInstance();
            ICacheHelper!.Add(key, value);
        }

        public static void CreateInstance(ICacheHelper? iCacheHelper = null)
        {
            if (ICacheHelper != null)
                return;
            if (iCacheHelper != null)
                ICacheHelper = iCacheHelper;
            else if (ICacheHelper == null)
                ICacheHelper = new CacheDictionary();
        }

        public static bool Contains(string key)
        {
            CreateInstance();
            return ICacheHelper!.Contains(key);
        }

        public static object? Get(string key)
        {
            CreateInstance();
            return ICacheHelper!.Get(key);
        }

        public static void Remove(string key)
        {
            CreateInstance();
            ICacheHelper!.Remove(key);
        }
    }
}