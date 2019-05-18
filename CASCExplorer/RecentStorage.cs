using Newtonsoft.Json;
using System.Collections.Generic;

namespace CASCExplorer
{
    class RecentStorage
    {
        public string Path { get; set; }
        public string Product { get; set; }

        public static List<RecentStorage> Storages { get; private set; }

        public static bool Load(string json)
        {
            if (string.IsNullOrWhiteSpace(json))
            {
                Storages = new List<RecentStorage>();
                return true;
            }
            else
            {
                try
                {
                    Storages = JsonConvert.DeserializeObject<List<RecentStorage>>(json);
                    return true;
                }
                catch
                {
                    Storages = new List<RecentStorage>();
                    return false;
                }
            }
        }

        public static string Save()
        {
            return JsonConvert.SerializeObject(Storages);
        }
    }
}
