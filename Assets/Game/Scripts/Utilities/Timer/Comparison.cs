using UnityEngine;

namespace Game.Scripts.Utilities.Timer
{
    public static class Comparison
    {
        public static bool Equals(float a, float b)
        {
            return Mathf.Approximately(a, b);
        }

        public static bool GreaterThanOrEquals(float a, float b)
        {
            return a > b || Equals(a, b);
        }

        public static bool LesserThanOrEquals(float a, float b)
        {
            return a < b || Equals(a, b);
        }
        public static bool LesserThan(float a, float b)
        {
            return a < b ;
        }
    }
}