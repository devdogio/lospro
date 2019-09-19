using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;

namespace Devdog.LosPro.Editors
{
    [CustomPropertyDrawer(typeof(TargetCategoryAttribute))]
    public class TargetCategoryPropertyDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            if (property.intValue < 0)
            {
                property.intValue = 0;
            }

            var categoryNames = new string[] { "No Settings set in manager!" };
            var categoryBitValues = new int[] { 0 };
            if (LosManager.instance != null && LosManager.instance.settings != null)
            {
                var l = new List<string>();
                l.Add("None");
                l.AddRange(LosManager.instance.settings.targetCategories.Select(o => o.name));
                categoryNames = l.ToArray();

                var l2 = new List<int>();
                l2.Add(0);
                l2.AddRange(LosManager.instance.settings.targetCategories.Select(o => o.bitFlagID));
                categoryBitValues = l2.ToArray();
            }

            var r = position;
            r.width = EditorGUIUtility.labelWidth;
            EditorGUI.PrefixLabel(r, label);

            position.x += r.width;
            position.width -= r.width;
            property.intValue = EditorGUI.IntPopup(position, property.intValue, categoryNames, categoryBitValues);


            EditorGUI.EndProperty();
        }
    }
}