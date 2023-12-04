using System;
using DG.Tweening;
using Game.Scripts.Utilities.Pooling;
using TMPro;
using UnityEngine;

namespace Game.Scripts.Utilities.UI
{
    public class FlyingText : MonoBehaviour
    {
        public TextMeshPro text;
        private PoolObject poolObject;
        private void Start()
        {
            poolObject = GetComponent<PoolObject>();
        }
     
        public void PlayFlyTween(string newText)
        {
            text.text = newText;
            if (text != null && DOTween.IsTweening(text)||text == null ) return;
            text.color = Color.red;
            text.transform.DOMoveY(text.transform.position.y + 1, 0.75f);
            text.DOFade(0f, 0.75f).OnComplete(()=>{poolObject.Release();});
        }
       
    }
}