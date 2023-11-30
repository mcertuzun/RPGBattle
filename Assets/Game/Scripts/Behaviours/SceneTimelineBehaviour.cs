using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;

namespace Game.Scripts.Behaviours
{
    public class SceneTimelineBehaviour : MonoBehaviour
    {
        [Header("Timeline")] public PlayableDirector cutsceneTimeline;

        [Header("Marker Events")] public UnityEvent cutsceneTimelineFinished;

        public void StartTimeline()
        {
            cutsceneTimeline.Play();
        }

        public void TimelineFinished()
        {
            cutsceneTimelineFinished.Invoke();
        }
    }
}