using System;
using UnityEditor;
using UnityEngine;

namespace Game.Data.New_Folder
{
    [CreateAssetMenu(menuName = "Create Stat", fileName = "Stat", order = 0)]
    [Serializable]
    public class Stat : ScriptableObject
    {
        public float value;
        public string Name;
        public int Level;
        public float CurrentExperience;
        public float RequiredExperience;
        public AnimationCurve ExperienceCurve;

        public float NextExp(int level)
        {
            return ExperienceCurve.Evaluate(level) * 100;
        }

        public void AddExperience(float exp)
        {
            if (CurrentExperience >= RequiredExperience)
            {
                Debug.Log(ToString());
                RequiredExperience = NextExp(Level);
                Debug.Log(ToString());
            }
        }

        public override string ToString()
        {
            return
                $"{nameof(Level)}: {Level}, {nameof(CurrentExperience)}: {CurrentExperience}, {nameof(RequiredExperience)}: {RequiredExperience}";
        }
    }

    [CustomEditor(typeof(Stat))]
    class DecalMeshHelperEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            var stat = target as Stat;
            base.OnInspectorGUI();
            if (GUILayout.Button("Test"))
                stat.AddExperience(stat.value);
        }
    }
}