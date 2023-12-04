using Game.Scripts.Data;
using Game.Scripts.Timer;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Game.Scripts.Behaviours.UI.TroopSelection
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
        public UIPopUpWaitBehaviour popUpWaitBehaviour;
        [Header("Selection")] [SerializeField] private Outline Outline;

        public bool isSelected
        {
            get
            {
                return Outline.enabled;
            }
            set
            {
                Outline.enabled = value;
            }
        }

        public delegate void TroopSelectEventHandler(UISelectionImageBehaviour UITroop);
        public event TroopSelectEventHandler TroopSelectEvent;

        private void Awake()
        {
            durationTimer = new DurationTimer(infoPopUpBehaviour.showInfoPopUpDelay);
        }

        private void Update()
        {
            CheckPopUp();
        }

        private void CheckPopUp()
        {
            if (isPointerDown && isPointerOver)
            {
                durationTimer.UpdateTimer();
                popUpWaitBehaviour.Loading(Input.mousePosition, durationTimer.GetPolledTime()/infoPopUpBehaviour.showInfoPopUpDelay);
                if (durationTimer.HasElapsed())
                {
                    popUpWaitBehaviour.Close();
                    if (!infoPopUpBehaviour.isOpened)
                        infoPopUpBehaviour.Open(troopData);
                    else
                        infoPopUpBehaviour.FollowPosition(Input.mousePosition);
                }
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            TroopSelectEvent?.Invoke(this);
        }

        public void SwitchSelection()
        {
            isSelected = !isSelected;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            isPointerDown = true;
            popUpWaitBehaviour.Open();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            isPointerDown = false;
            popUpWaitBehaviour.Close();
            infoPopUpBehaviour.Close();
            durationTimer.Reset();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            isPointerOver = true;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            isPointerOver = false;
            popUpWaitBehaviour.Close();
            infoPopUpBehaviour.Close();
            durationTimer.Reset();
        }
    }
}