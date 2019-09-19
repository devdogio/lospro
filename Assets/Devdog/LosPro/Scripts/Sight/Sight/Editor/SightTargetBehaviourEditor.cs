
using UnityEngine;
using System.Collections.Generic;
using UnityEditor;

namespace Devdog.LosPro.Editors
{
    [CustomEditor(typeof(SightTargetBehaviour))]
    public class SightTargetBehaviourEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            //            base.OnInspectorGUI();
            DrawDefaultInspector();

            var t = (SightTargetBehaviour)target;
            if (t.gameObject.GetComponentInChildren<Collider>() == null)
            {
                EditorGUILayout.HelpBox("No Collider found on object, can't be detected.", MessageType.Warning);
            }
        }


        /// <summary>
        /// Unity method, draws the outlines of the AudioSourceBehaviours to visualize the audio spheres.
        /// </summary>
        [DrawGizmo(GizmoType.Selected | GizmoType.Active)]
        private static void DrawOutlines(SightTargetBehaviour source, GizmoType gizmoType)
        {
            if (source.indexingConfig.indexingType == TargetIndexingType.Manual)
            {
                foreach (var raycastPoint in source.indexingConfig.raycastPoints)
                {
                    if (raycastPoint == null)
                    {
                        continue;
                    }

                    Gizmos.color = new Color(1f, 0f, 0f);
                    Gizmos.DrawCube(raycastPoint.position, Vector3.one * 0.075f);
                    Gizmos.color = Color.white;
                }
            }
        }
    }
}