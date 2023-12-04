using Assets.Library.Pooling;
using UnityEngine;

namespace Game.Scripts.Behaviours.Troop.DamageDisplay
{
    
        public class TroopDamageDisplayListener : MonoBehaviour
        {
        
            [Header("Damage Data Sources")]
            public TroopDamageDisplayBehaviour[] unitDamageDisplayBehaviours;

            [Header("Pooled Objects")]
            public IPoolObject TextPoolObject;
            public IPoolObject HitVFXPoolObject;

            void OnEnable()
            {
                for(int i = 0; i < unitDamageDisplayBehaviours.Length; i++)
                {
                    unitDamageDisplayBehaviours[i].DamageDisplayEvent += ShowDamageDisplays;
                }
            }

            void OnDisable()
            {
                for(int i = 0; i < unitDamageDisplayBehaviours.Length; i++)
                {
                    unitDamageDisplayBehaviours[i].DamageDisplayEvent -= ShowDamageDisplays;
                }
            }

            void ShowDamageDisplays(int damageAmount, Transform damageLocation, Color damageColor)
            {
                //TextPoolObject.
                // numberDisplayManager.ShowNumber(damageAmount, damageLocation, damageColor);
                // hitVFXDisplayManager.ShowHitVFX(damageLocation);
            }

        }  
}