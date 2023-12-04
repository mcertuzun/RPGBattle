using System;
using UnityEngine;

namespace Game.Scripts.Utilities.Timer
{
    [Serializable]
    public class DurationTimer
    {
        private float polledTime;
        private float durationTime;

        public DurationTimer(float durationTime)
        {
            Reset(durationTime);
        }

        public void UpdateTimer()
        {
            this.polledTime += Time.deltaTime;
        }

        public void Reset()
        {
            this.polledTime = 0;
        }

        public void Reset(float durationTime)
        {
            Reset();
            this.durationTime = durationTime;
        }

        public bool HasElapsed()
        {
            return Comparison.GreaterThanOrEquals(this.polledTime, this.durationTime);
        }

        public float GetRatio()
        {
            if (Comparison.LesserThanOrEquals(this.durationTime, 0))
            {
                return 1.0f;
            }

            float ratio = this.polledTime / this.durationTime;
            return Mathf.Clamp(ratio, 0, 1);
        }

        public float GetPolledTime()
        {
            return this.polledTime;
        }

        public void EndTimer()
        {
            this.polledTime = this.durationTime;
        }
    }
}