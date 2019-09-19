using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

namespace Devdog.LosPro.Editors
{

    [InitializeOnLoad]
    public class AffectorZoneHieararchyEditor
    {
        private static Texture2D _texture;

        static AffectorZoneHieararchyEditor()
        {
            _texture = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/LosPro/EditorStyles/ScriptIcons/Zone.psd");
            EditorApplication.hierarchyWindowItemOnGUI += HierarchyItemCB;
        }

        static void HierarchyItemCB(int instanceID, Rect selectionRect)
        {
            var obj = EditorUtility.InstanceIDToObject(instanceID) as GameObject;
            if (obj != null)
            {
                var c = obj.GetComponent<IAffectorZone>();
                if (c != null)
                {
                    selectionRect.x = selectionRect.width - 5;
                    selectionRect.y -= 2;

                    selectionRect.height = 20;
                    selectionRect.width = 20;

                    GUI.Label(selectionRect, _texture);
                }
            }
        }
    }
}