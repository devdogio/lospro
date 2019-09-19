using UnityEngine;
using System.Collections.Generic;
using UnityEditor;

namespace Devdog.LosPro.Editors
{
    [CustomPropertyDrawer(typeof(SightConfiguration))]
    public class SightConfigurationPropertyEditor : PropertyDrawer
    {
        private bool _unfolded = true;

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (_unfolded)
            {
                int rows = 27;
                if (IsRaycastLayerSetInlayerMask(property))
                {
                    rows += 2;
                }

                return EditorGUIUtility.singleLineHeight * rows;
            }

            return EditorGUIUtility.singleLineHeight + 2;
        }

        private bool IsRaycastLayerSetInlayerMask(SerializedProperty property)
        {
            var raycastLayer = property.FindPropertyRelative("raycastLayer");
            LayerMask raycastLayerMask = (LayerMask)raycastLayer.intValue;
            if (AudioSourceManager.hasInstance)
            {
                if (raycastLayerMask == (raycastLayerMask | (1 << LosManager.instance.settings.sightLayerID)) ||
                    raycastLayerMask == (raycastLayerMask | (1 << LosManager.instance.settings.hearingLayerID)))
                {
                    return true;
                }
            }

            if (AudioSourceManager2D.hasInstance)
            {
                if (raycastLayerMask == (raycastLayerMask | (1 << LosManager.instance.settings.sightLayerID)) ||
                    raycastLayerMask == (raycastLayerMask | (1 << LosManager.instance.settings.hearingLayerID)))
                {
                    return true;
                }
            }

            return false;
        }


        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);
            position.height = EditorGUIUtility.singleLineHeight + 2;
            _unfolded = EditorGUI.Foldout(position, _unfolded, property.displayName);
            if (_unfolded)
            {
                EditorGUI.indentLevel = 1;

                //var updateOneAtATime = property.FindPropertyRelative("updateOneAtATime");
                var sightTargetCategoriesMask = property.FindPropertyRelative("sightTargetCategoriesMask");
                var raycastLayer = property.FindPropertyRelative("raycastLayer");
                var updateInterval = property.FindPropertyRelative("updateInterval");
                var minDotValue = property.FindPropertyRelative("fieldOfViewDotValue");
                var sampleCount = property.FindPropertyRelative("sampleCount");
                var minVisibleFactor = property.FindPropertyRelative("minVisibleFactor");
                var instantlyDetectWhenTargetIsFullyVisible = property.FindPropertyRelative("instantlyDetectWhenTargetIsFullyVisible");
                var viewingDistance = property.FindPropertyRelative("viewingDistance");
                var extrapolatePath = property.FindPropertyRelative("extrapolatePath");
                var extrapolateSampleCount = property.FindPropertyRelative("extrapolateSampleCount");
                var onlyWithTag = property.FindPropertyRelative("onlyWithTag");
                var debug = property.FindPropertyRelative("debug");



                float angleRadians = Mathf.Acos(minDotValue.floatValue);
                float angleDegrees = angleRadians * Mathf.Rad2Deg;

                var r = position;

                #region INFO

                r.y += EditorGUIUtility.singleLineHeight + 4;
                r.height = EditorGUIUtility.singleLineHeight * 2;
                r.width -= 20;
                r.x += 20;


                if (IsRaycastLayerSetInlayerMask(property))
                {
                    EditorGUI.HelpBox(r, "Rayacst layer mask contains sight or hearing layer", MessageType.Warning);
                    r.y += r.height + 4;
                }


                EditorGUI.HelpBox(r, "Objects have to be atleast " + System.Math.Round(minVisibleFactor.floatValue * 100f, 2) + "% visible before detected.", MessageType.Info);
                r.y += r.height + 4;

                if (sampleCount.intValue > 20)
                {
                    EditorGUI.HelpBox(r, "More samples brings more accuracy but heavily decreases performance.\n10 Samples recommended for regular objects.\n20 Samples recommended for large objects.", MessageType.Warning);
                    r.y += r.height + 4;
                }

                EditorGUI.HelpBox(r, "Total of " + angleDegrees * 2 + " degrees viewing angle", MessageType.Info);
                r.y += r.height + 4;

                EditorGUI.HelpBox(r, "A max of " + sampleCount.intValue + " samples are taken at the same time.\n" +
                                        "Optimally " + Mathf.Round(sampleCount.intValue * minVisibleFactor.floatValue) + " samples will be used.", MessageType.Info);
                r.y += r.height + 4;


                r.width += 20;
                r.x -= 20;

                #endregion

                r.height = EditorGUIUtility.singleLineHeight;


                r.y += 10;
                EditorGUI.LabelField(r, "Visibility", EditorStyles.boldLabel);
                r.y += EditorGUIUtility.singleLineHeight + 4;

                EditorGUI.PropertyField(r, sightTargetCategoriesMask);
                r.y += EditorGUIUtility.singleLineHeight + 2;
                EditorGUI.PropertyField(r, raycastLayer);
                r.y += EditorGUIUtility.singleLineHeight + 2;
                EditorGUI.PropertyField(r, updateInterval);
                r.y += EditorGUIUtility.singleLineHeight + 2;
                EditorGUI.PropertyField(r, minDotValue);
                r.y += EditorGUIUtility.singleLineHeight + 2;
                EditorGUI.PropertyField(r, viewingDistance);
                if (viewingDistance.floatValue < 0f)
                    viewingDistance.floatValue = 0f;

                r.y += EditorGUIUtility.singleLineHeight + 2;
                EditorGUI.PropertyField(r, sampleCount);
                if (sampleCount.intValue < 1)
                    sampleCount.intValue = 1;

                r.y += EditorGUIUtility.singleLineHeight + 2;
                EditorGUI.PropertyField(r, minVisibleFactor);
                if (minVisibleFactor.floatValue < 0f)
                {
                    minVisibleFactor.floatValue = 0f;
                }

                if (minVisibleFactor.floatValue * sampleCount.intValue < 1f)
                {
                    minVisibleFactor.floatValue = 1f / sampleCount.intValue;
                }

                r.y += EditorGUIUtility.singleLineHeight + 2;
                EditorGUI.PropertyField(r, instantlyDetectWhenTargetIsFullyVisible);

                r.y += EditorGUIUtility.singleLineHeight + 2;

                r.y += 10;
                EditorGUI.LabelField(r, "Extrapolation", EditorStyles.boldLabel);
                r.y += EditorGUIUtility.singleLineHeight + 4;

                EditorGUI.PropertyField(r, extrapolatePath);
                r.y += EditorGUIUtility.singleLineHeight + 2;
                EditorGUI.PropertyField(r, extrapolateSampleCount);
                if (extrapolateSampleCount.intValue < 0)
                    extrapolateSampleCount.intValue = 0;

                r.y += EditorGUIUtility.singleLineHeight + 2;

                


                r.y += 10;
                EditorGUI.LabelField(r, "Misc", EditorStyles.boldLabel);
                r.y += EditorGUIUtility.singleLineHeight + 4;

                EditorGUI.PropertyField(r, onlyWithTag);
                r.y += EditorGUIUtility.singleLineHeight + 2;
                EditorGUI.PropertyField(r, debug);

                EditorGUI.indentLevel = 0;
            }
            EditorGUI.EndProperty();
        }
    }
}