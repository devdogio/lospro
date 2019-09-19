using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEditor;

namespace Devdog.LosPro.Editors
{
    [CustomEditor(typeof(ExtrapolationDebugger), true)]
    public class ExtrapolationDebuggerEditor : Editor
    {
        private Dictionary<ISightTarget, bool> _foldout = new Dictionary<ISightTarget, bool>(); 
        private IEnumerable<KeyValuePair<ISightTarget, FixedSizeQueue<SightTargetSampleData>>> _samples;
        private IObserver _observer;


        protected void OnEnable()
        {
            var t = (ExtrapolationDebugger)target;
            _observer = t.gameObject.GetComponent<IObserver>();
        }


        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (_observer == null)
            {
                EditorGUILayout.HelpBox("No IObserver found - Can't display extrapolation data", MessageType.Warning);
                return;
            }

            if (EditorApplication.isPlaying == false)
            {
                EditorGUILayout.HelpBox("Extrapolation data can only be seen in playmode.", MessageType.Info);
                return;
            }


            var sight = _observer.sight;
            sight.extrapolator.GetAllSamples(out _samples);
            EditorGUILayout.BeginVertical("box");

            if (_samples.Any() == false)
            {
                EditorGUILayout.LabelField("No extrapolation data available.");
            }

            foreach (var k in _samples)
            {
                var key = k.Key;
                var val = k.Value;

                if (key == null || key.IsDestroyed())
                {
                    continue;
                }

                EditorGUI.indentLevel++;

                if (_foldout.ContainsKey(key) == false)
                {
                    _foldout.Add(key, false);
                }

                _foldout[key] = EditorGUILayout.Foldout(_foldout[key], key.name);
                if (_foldout[key])
                {
                    foreach (var sampleData in val)
                    {
                        EditorGUILayout.LabelField("Sample: " + sampleData.position + " - " + sampleData.time + "(s)");
                    }
                }

                EditorGUI.indentLevel--;
            }

            EditorGUILayout.EndVertical();
        }
    }
}