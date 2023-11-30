﻿using UnityEngine;

namespace Game.Scripts.Timer
{
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
            return Comparison.TolerantGreaterThanOrEquals(this.polledTime, this.durationTime);
        }

        public float GetRatio()
        {
            if (Comparison.TolerantLesserThanOrEquals(this.durationTime, 0))
            {
                // bad duration time value
                // if countdownTime is zero, ratio will be infinity (divide by zero)
                // we just return 1.0 here for safety
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