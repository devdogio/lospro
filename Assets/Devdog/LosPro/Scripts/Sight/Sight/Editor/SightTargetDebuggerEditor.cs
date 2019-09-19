
using UnityEngine;
using System.Collections.Generic;
using UnityEditor;

namespace Devdog.LosPro.Editors
{
    [CustomEditor(typeof(SightTargetDebugger))]
    public class SightTargetDebuggerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            //            base.OnInspectorGUI();
            DrawDefaultInspector();

            var t = (SightTargetDebugger)target;
            var tar = t.gameObject.GetComponent<ISightTarget>();

            EditorGUILayout.BeginVertical(Devdog.General.Editors.EditorStyles.boxStyle);
            if (EditorApplication.isPlaying)
            {
                EditorGUILayout.LabelField("Run-time target information", Devdog.General.Editors.EditorStyles.titleStyle);

                EditorGUILayout.LabelField("Is visible: \t\t" + t.isVisible);
                EditorGUILayout.LabelField("Visible by: \t" + t.visibleBy.Count + " observers");

                EditorGUILayout.BeginVertical(Devdog.General.Editors.EditorStyles.boxStyle);

                foreach (var sight in t.visibleBy)
                {
                    var info = sight.GetInRangeTargetInfo(tar);

                    EditorGUILayout.LabelField("Sight: \t\t\t" + sight.name, Devdog.General.Editors.EditorStyles.titleStyle);
                    EditorGUILayout.LabelField("% visible: \t", info.visibleFactor * 100 + "%");
                    EditorGUILayout.LabelField("Visible for seconds: \t", info.visibleForSeconds + "s");
                    EditorGUILayout.LabelField("Distance: \t", info.distance + " units");
                    EditorGUILayout.LabelField("Last seen at: \t", info.lastSeenAt.ToString());

                    EditorGUILayout.Space();
                }

                EditorGUILayout.EndVertical();


            }
            else
            {
                EditorGUILayout.LabelField("Run-time information only visible when in play mode");
            }

            EditorGUILayout.EndVertical();
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

