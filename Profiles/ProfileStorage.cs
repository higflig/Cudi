using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Cudi.Profiles
{
    public static class ProfileStorage
    {
        public static void SaveProfiles<T>(IEnumerable<T> profiles, string filePath)
        {
            string jsonData = JsonConvert.SerializeObject(profiles, Formatting.Indented);
            File.WriteAllText(filePath, jsonData);
        }

        public static IEnumerable<T> LoadProfiles<T>(string filePath)
        {
            if (!SaveExists(filePath)) return null;
            string jsonData = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<List<T>>(jsonData);
        }

        public static bool SaveExists(string filePath)
        {
            return File.Exists(filePath);
        }
    }
}