using Game.Scripts;
using Game.Scripts.Controllers;
using Game.Scripts.Data;
using NUnit.Framework;
using UnityEngine;

namespace Tests.Test
{
    public class DataSaveLoadTests
    {
        // A Test behaves as an ordinary method
        [Test]
        public void DataSaveLoadTestsSimplePasses()
        {
            // Create an instance of TroopData
            TroopData troopData = ScriptableObject.CreateInstance<TroopData>();
            troopData.Name = "TestTroop";
            troopData.Health = 100f;
            troopData.AttackPower = 10f;
            troopData.Experience = 0;
            troopData.Level = 0;
                Debug.Log(troopData.ToString());
            
            // Modify and save data
            troopData.GainExperience(5); // This should also level up the troop
            troopData.Save();
            Debug.Log(troopData.ToString());

            // Create a new instance to load data
            TroopData loadedTroopData = ScriptableObject.CreateInstance<TroopData>();
            loadedTroopData.Name = "TestTroop";
            loadedTroopData.Load();
            Debug.Log(loadedTroopData.ToString());

            // Assert that the data is preserved
            Assert.AreEqual(troopData.Experience, loadedTroopData.Experience);
            Assert.AreEqual(troopData.Level, loadedTroopData.Level);
            Assert.AreEqual(troopData.AttackPower, loadedTroopData.AttackPower);
            Assert.AreEqual(troopData.Health, loadedTroopData.Health);

            // Cleanup
            troopData.ResetData();
        }
       
        //
        // // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // // `yield return null;` to skip a frame.
        // [UnityTest]
        // public IEnumerator DataSaveLoadTestsWithEnumeratorPasses()
        // {
        //     // Use the Assert class to test conditions.
        //     // Use yield to skip a frame.
        //     yield return null;
        // }
    }
}
