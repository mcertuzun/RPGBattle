using Game.Scripts.Controllers.Troop;
using UnityEngine;

namespace Game.Scripts.Behaviours
{
    public class SpriteClickBehaviour : MonoBehaviour
    {

        public delegate void StartAttack();

        public event StartAttack StartAttackEvent;

        void OnMouseDown()
        {
            StartAttackEvent?.Invoke();
        }
    }
}