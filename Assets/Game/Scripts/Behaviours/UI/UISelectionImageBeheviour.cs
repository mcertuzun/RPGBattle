using Game.Scripts.Data;
using Game.Scripts.Timer;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Game.Scripts.Behaviours.UI
{
    public class UISelectionImageBehaviour : MonoBehaviour, IPointerClickHandler, IPointerDownHandler,
        IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [Header("Data")] public TroopData troopData;

        [Header("Info Popup")] [SerializeField]
        private DurationTimer durationTimer;

        private bool isPointerDown;
        private bool isPointerOver;
        public UISelectionInfoPopUpBehaviour infoPopUpBehaviour;

        [Header("Selection")] [SerializeField] private Outline Outline;
        public bool isSelected;

        public delegate void TroopSelectEventHandler(UISelectionImageBehaviour UITroop);

        public event TroopSelectEventHandler TroopSelectEvent;

        private void Awake()
        {
            durationTimer = new DurationTimer(infoPopUpBehaviour.showInfoPopUpDelay);
        }

        private void Update()
        {
            if (isPointerDown && isPointerOver)
            {
                durationTimer.UpdateTimer();
                if (durationTimer.HasElapsed())
                {
                    if (!infoPopUpBehaviour.isOpened)
                        infoPopUpBehaviour.Open(troopData);
                    else
                        infoPopUpBehaviour.FollowPosition(Input.mousePosition);
                }
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            isSelected = !isSelected;
            Outline.enabled = isSelected;
            TroopSelectEvent?.Invoke(this);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            isPointerDown = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            isPointerDown = false;
            durationTimer.Reset();
            infoPopUpBehaviour.Close();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            isPointerOver = true;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            isPointerOver = false;
            infoPopUpBehaviour.Close();
            durationTimer.Reset();
        }
    }
}