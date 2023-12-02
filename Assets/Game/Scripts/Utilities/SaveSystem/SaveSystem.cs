using System.Collections.Generic;
using System.IO;
using Game.Scripts.Data;
using UnityEngine;

namespace Game.Scripts.Utilities.SaveSystem
{
    public static class SaveSystem
    {
        private static string GetFilePath(string heroName)
        {
            return Path.Combine(Application.persistentDataPath, $"{heroName}.json");
        }

        public static void SaveTroopsData(List<TroopData> troops)
        {
            TroopList troopList = new TroopList() { data = troops };
            string json = JsonUtility.ToJson(troopList);
            File.WriteAllText(GetFilePath("troops"), json);
        }

        public static List<TroopData> LoadTroopsData(List<TroopData> troops)
        {
            string filePath = GetFilePath("troops");
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                TroopList troopList = JsonUtility.FromJson<TroopList>(json);
                return troopList.data;
            }

            return new List<TroopData>();
        }

        public struct TroopList
        {
            public List<TroopData> data;
        }
    }
}