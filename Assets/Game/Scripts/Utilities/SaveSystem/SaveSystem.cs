using System.Collections.Generic;
using Game.Scripts.Data;

namespace Game.Scripts.Utilities.SaveSystem
{
    using System.IO;
    using UnityEngine;

    public static class SaveSystem
    {
        private static string GetFilePath(string heroName)
        {
            return Path.Combine(Application.persistentDataPath, $"{heroName}.json");
        }

        public static void SaveTroopsData(List<TroopData> troops)
        {
            for (var i = 0; i < troops.Count; i++)
            {
                SaveHeroData(troops[i]);
            }
        }

        public static void LoadTroopsData(List<TroopData> troops)
        {
            for (var i = 0; i < troops.Count; i++)
            {
                LoadTroopData(troops[i]);
            }
        }
        public static void SaveHeroData(TroopData troop)
        {
            string json = JsonUtility.ToJson(troop);
            File.WriteAllText(GetFilePath(troop.Name), json);
        }

        public static void LoadTroopData(TroopData troop)
        {
            string filePath = GetFilePath(troop.Name);
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                JsonUtility.FromJsonOverwrite(json, troop);
            }
        }
    }

}