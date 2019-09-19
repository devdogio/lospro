using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;

namespace Devdog.LosPro.Editors
{
    [CustomPropertyDrawer(typeof(TargetCategoriesMaskAttribute))]
    public class TargetCategoriesMaskPropertyEditor : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            if (LosManager.instance == null || LosManager.instance.settings == null)
            {
                EditorGUI.PrefixLabel(position, label);
                position.x += EditorGUIUtility.labelWidth;
                position.width -= EditorGUIUtility.labelWidth;

                EditorGUI.HelpBox(position, "No manager or settings.", MessageType.Error);
                EditorGUI.EndProperty();
                return;
            }

            var categoryNames = LosManager.instance.settings.targetCategories.Select(o => o.name).ToArray();
            var categoryBitValues = LosManager.instance.settings.targetCategories.Select(o => o.bitFlagID).ToArray();

            property.intValue = DrawBitMaskField(position, property.intValue, categoryNames, categoryBitValues, label);
            EditorGUI.EndProperty();
        }


        public static int DrawBitMaskField(Rect rect, int mask, string[] itemNames, int[] itemValues, GUIContent label)
        {
            int val = mask;
            int maskVal = 0;
            for (int i = 0; i < itemValues.Length; i++)
            {
                if (itemValues[i] != 0)
                {
                    if ((val & itemValues[i]) == itemValues[i])
                        maskVal |= 1 << i;
                }
                else if (val == 0)
                    maskVal |= 1 << i;
            }
            int newMaskVal = EditorGUI.MaskField(rect, label, maskVal, itemNames);
            int changes = maskVal ^ newMaskVal;

            for (int i = 0; i < itemValues.Length; i++)
            {
                if ((changes & (1 << i)) != 0)            // has this list item changed?
                {
                    if ((newMaskVal & (1 << i)) != 0)     // has it been set?
                    {
                        if (itemValues[i] == 0)           // special case: if "0" is set, just set the val to 0
                        {
                            val = 0;
                            break;
                        }
                        else
                            val |= itemValues[i];
                    }
                    else                                  // it has been reset
                    {
                        val &= ~itemValues[i];
                    }
                }
            }
            return val;
        }
    }
}