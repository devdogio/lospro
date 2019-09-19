using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEditor;

namespace Devdog.LosPro.Editors
{
    [CustomEditor(typeof(ListenerBehaviour2D))]
    public class ListenerBehaviour2DEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            var t = (ListenerBehaviour2D) target;
            if (t.gameObject.GetComponentsInChildren<Collider2D>().Any(o => o.isTrigger == false) == false)
            {
                GUILayout.Space(10);
                GUILayout.BeginVertical(Devdog.General.Editors.EditorStyles.boxStyle);

                GUILayout.Label("Missing a 2D collider on this object, or child.", Devdog.General.Editors.EditorStyles.titleStyle);
                GUILayout.Label("Listener can't hear anything.");
                if (GUILayout.Button("Add 2D box collider"))
                {
                    t.gameObject.AddComponent<BoxCollider2D>();
                }

                GUILayout.EndVertical();
                GUILayout.Space(10);
            }
        }
    }
}