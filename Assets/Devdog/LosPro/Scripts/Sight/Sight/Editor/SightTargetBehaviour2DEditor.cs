
using UnityEngine;
using System.Collections.Generic;
using UnityEditor;

namespace Devdog.LosPro.Editors
{
    [CustomEditor(typeof(SightTargetBehaviour2D))]
    public class SightTargetBehaviour2DEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            //            base.OnInspectorGUI();
            DrawDefaultInspector();

            var t = (SightTargetBehaviour2D)target;
            if (t.gameObject.GetComponentInChildren<Collider2D>() == null)
            {
                EditorGUILayout.HelpBox("No Collider2D found on object, can't be detected.", MessageType.Warning);
            }
        }
    }
}