using UnityEngine;
using System.Collections;
using UnityEditor;

namespace Devdog.LosPro.Editors
{
    [CustomEditor(typeof(AudioSourceBehaviour2D))]
    [CanEditMultipleObjects]
    public class AudioSourceBehaviour2DEditor : AudioSourceBehaviourEditorBase
    {
        /// <summary>
        /// Unity method, draws the outlines of the AudioSourceBehaviours to visualize the audio spheres.
        /// </summary>
        [DrawGizmo(GizmoType.Selected | GizmoType.Active | GizmoType.NonSelected)]
        private static void DrawOutlines(AudioSourceBehaviour2D source, GizmoType gizmoType)
        {
            if (drawDebug)
            {
                DrawDebug(source);
            }
        }
    }
}