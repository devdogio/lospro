using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Devdog.General.Editors;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine.Assertions;
using EditorStyles = UnityEditor.EditorStyles;
using EditorUtility = UnityEditor.EditorUtility;

namespace Devdog.LosPro.Editors
{
    [CustomEditor(typeof(ObserverAggroBehaviour), true)]
    public class ObserverAggroBehaviourEditor : Editor
    {
        private ModuleList<IObserverAggroModule> _moduleList;
        private Dictionary<ISightTarget, bool> _aggroModulesFoldout = new Dictionary<ISightTarget, bool>();
        private bool _enabled = false;

        public void OnEnable()
        {
            var t = (ObserverAggroBehaviour)target;
            _enabled = true;

            _moduleList = new ModuleList<IObserverAggroModule>(t, this, "Aggro module");
        }

        public void OnDisable()
        {
            _enabled = false;
        }

        public override bool RequiresConstantRepaint()
        {
            if (EditorApplication.isPlaying && _enabled)
            {
                return true;
            }

            return false;
        }

        public override void OnInspectorGUI()
        {
            var t = (ObserverAggroBehaviour)target;

            base.OnInspectorGUI();
            DrawSep();
            GUILayout.Label("Debug values", Devdog.General.Editors.EditorStyles.titleStyle);
            GUILayout.Label("Current aggro:\t" + t.currentAggro + " / " + t.maxAggro);
            GUILayout.Label("Start location:\t" + t.startLocation);
            GUILayout.Label("Distance from start:\t" + (EditorApplication.isPlaying ? Vector3.Distance(t.transform.position, t.startLocation).ToString() : "-"));
            GUILayout.Label("Active module count:\t" + t.aggroComponents.Where(o => o != null).Cast<IObserverAggroModule>().Count(o => o.enabled));
            DrawSep();

            if (EditorApplication.isPlaying)
            {
                foreach (var info in t.observer.targetsInRange)
                {
                    if (_aggroModulesFoldout.ContainsKey(info.target) == false)
                    {
                        _aggroModulesFoldout.Add(info.target, false);
                    }
                }

                GUILayout.Label("Aggro values", Devdog.General.Editors.EditorStyles.titleStyle);
                var aggro = t.observer.targetsInRange.Where(o => t.GetAggroForTarget(o.target) != null).OrderByDescending(o => t.GetAggroForTarget(o.target).finalValue);
                foreach (var info in aggro)
                {
                    var foldout = new GUIStyle(EditorStyles.foldout);
                    foldout.fontStyle = FontStyle.Bold;

                    if (info.target == t.GetTargetWithHighestAggro().target)
                    {
                        GUI.color = Color.green;
                    }
                    _aggroModulesFoldout[info.target] = EditorGUILayout.Foldout(_aggroModulesFoldout[info.target], info.target.name + "\t-\t" + t.GetAggroForTarget(info.target), foldout);
                    if (_aggroModulesFoldout[info.target])
                    {
                        DrawModuleOutputForTarget(info.target);
                    }

                    GUI.color = Color.white;
                }

                DrawSep();
            }

            var before = EditorGUIUtility.labelWidth;
            EditorGUIUtility.labelWidth *= 1.6f;
            _moduleList.DoLayoutList();

            EditorGUIUtility.labelWidth = before;
        }

        private void DrawModuleOutputForTarget(ISightTarget sightTarget)
        {
            EditorGUI.indentLevel++;

            var t = (ObserverAggroBehaviour)target;

            var obj = t.aggroDict.FirstOrDefault(o => o.Key == sightTarget).Value;
            if (obj != null)
            {
                foreach (var module in t.aggroModules)
                {
                    if (module.enabled == false)
                    {
                        continue;
                    }

                    var max = t.aggroModules.Max(o => o.name.Length);
                    var tabs = Mathf.FloorToInt(max / (float)module.name.Length) + 1;
                    string final = module.name;

                    for (int i = 0; i < tabs; i++)
                    {
                        final += "\t";
                    }

                    EditorGUILayout.LabelField(string.Format("{0} -\t{1}", final, module.CalculateAggroForTarget(sightTarget)));
                }
            }

            EditorGUI.indentLevel--;
        }

        private void DrawSep()
        {
            GUILayout.Space(12);
            GUILayout.Button("", "sv_iconselector_sep");
        }

        [DrawGizmo(GizmoType.Selected | GizmoType.NonSelected)]
        protected static void GizmoTest(ObserverAggroBehaviour observerAggroBehaviour, GizmoType gizmoType)
        {
            if (EditorApplication.isPlaying)
            {
                var t = observerAggroBehaviour.GetTargetWithHighestAggro();
                if (t != null)
                {
                    Gizmos.color = Color.black;
                    var dir = t.target.transform.position - observerAggroBehaviour.transform.position;
                    DrawArrow(observerAggroBehaviour.transform.position + (dir * 0.1f), dir - (dir * 0.2f));
                    Gizmos.color = Color.white;
                }
            }
        }
        public static void DrawArrow(Vector3 pos, Vector3 direction)
        {
            Gizmos.DrawRay(pos, direction);

            Vector3 right = Quaternion.LookRotation(direction) * Quaternion.Euler(0f, 180f + 30f, 0f) * new Vector3(0, 0, 1f);
            Vector3 left = Quaternion.LookRotation(direction) * Quaternion.Euler(0f, 180f - 30f, 0f) * new Vector3(0, 0, 1f);

            Gizmos.DrawRay(pos + direction, right * 0.3f);
            Gizmos.DrawRay(pos + direction, left * 0.3f);
        }
    }
}