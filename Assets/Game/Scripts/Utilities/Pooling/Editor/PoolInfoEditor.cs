using Game.Scripts.Utilities.Pooling.Core;
using UnityEditor;
using UnityEngine;

namespace Game.Scripts.Utilities.Pooling.Editor
{
#if UNITY_EDITOR
    [CustomEditor(typeof(PoolInfo))]
    public class PoolInfoEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            PoolInfo poolInfo = target as PoolInfo;

            if (poolInfo != null && poolInfo.Prefab != null)
                if (poolInfo.Prefab.GetComponent<IPoolObject>() == null)
                {

                    EditorGUILayout.HelpBox("Prefab To be pooled supposed to have an IPoolObject Interface Extender " +
                        "to be able to reset the object to instantiation state", MessageType.Error);
                    GUI.color = Color.red;

                }

            base.OnInspectorGUI();

        }
    }
#endif
}