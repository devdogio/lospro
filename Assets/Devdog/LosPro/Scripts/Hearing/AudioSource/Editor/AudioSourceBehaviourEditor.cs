using UnityEngine;
using System.Collections;
using UnityEditor;

namespace Devdog.LosPro.Editors
{
    [CustomEditor(typeof(AudioSourceBehaviour))]
    [CanEditMultipleObjects]
    public class AudioSourceBehaviourEditor : AudioSourceBehaviourEditorBase
    {
        /// <summary>
        /// Unity method, draws the outlines of the AudioSourceBehaviours to visualize the audio spheres.
        /// </summary>
        [DrawGizmo(GizmoType.Selected | GizmoType.Active | GizmoType.NonSelected)]
        private static void DrawOutlines(AudioSourceBehaviour source, GizmoType gizmoType)
        {
            if (drawDebug)
            {
                DrawDebug(source);
            }
        }
    }
}