using Game.Scripts.Data;
using TMPro;
using UnityEngine;

namespace Game.Scripts.Behaviours.UI.TroopSelection
{
    public class UISelectionInfoPopUpBehaviour : MonoBehaviour
    {
        public float showInfoPopUpDelay = 3;
        public GameObject PopUp;
        public TextMeshProUGUI dataText;
        public bool isOpened;

        private void Awake()
        {
            Reset();
        }

        public void Open(TroopData popUpData)
        {
            isOpened = true;
            dataText.text = $"Name: {popUpData.Name}\n " +
                            $"Level: {popUpData.Level}\n" +
                            $"Attack Power: {popUpData.AttackPower}\n" +
                            $"Experience: {popUpData.Experience}";
         
            PopUp.SetActive(isOpened);
        }

        public void FollowPosition(Vector3 position)
        {
            transform.position = position;
        }

        public void Close()
        {
            Reset();
        }

        private void Reset()
        {
            isOpened = false;
            PopUp.SetActive(isOpened);
            transform.position = Vector3.zero;
            dataText.text = "";
        }
    }
}