using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
