using UnityEngine;
using System.Collections;
using UnityEditor;

namespace Devdog.LosPro.Editors
{
    public class AudioSourceBehaviourEditorBase : Editor
    {
        protected static bool drawDebug;
        protected const string DrawDebugKey = "LOS_PRO_DRAW_AUDIO_SOURCE_DEBUG";


        [InitializeOnLoadMethod]
        private static void StartMethod()
        {
            drawDebug = EditorPrefs.GetBool(DrawDebugKey, true);
        }

//        public virtual void OnEnable()
//        {
//        }

        protected static void DrawDebug(IAudioSource source)
        {
            var c = Color.cyan;
            c.a = 0.3f;
            Gizmos.color = c;
            Gizmos.DrawWireSphere(source.transform.position, source.transform.localScale.x);
            Gizmos.color = Color.white;
        }

        public override void OnInspectorGUI()
        {
            if (GUILayout.Button("Show / Hide audio spheres"))
            {
                drawDebug = !drawDebug;
                EditorPrefs.SetBool(DrawDebugKey, drawDebug);
            }

            base.OnInspectorGUI();
        }
    }
}