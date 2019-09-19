using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Linq;

namespace Devdog.LosPro.Editors
{
    [CustomEditor(typeof(ListenerBehaviour))]
    public class ListenerBehaviourEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            var t = (ListenerBehaviour) target;
            if (t.gameObject.GetComponentsInChildren<Collider>().Any(o => o.isTrigger == false) == false)
            {
                GUILayout.Space(10);
                GUILayout.BeginVertical(Devdog.General.Editors.EditorStyles.boxStyle);

                GUILayout.Label("Missing a collider on this object, or child.", Devdog.General.Editors.EditorStyles.titleStyle);
                GUILayout.Label("Listener can't hear anything.");
                if (GUILayout.Button("Add box collider"))
                {
                    t.gameObject.AddComponent<BoxCollider>();
                }

                GUILayout.EndVertical();
                GUILayout.Space(10);
            }
        }
    }
}