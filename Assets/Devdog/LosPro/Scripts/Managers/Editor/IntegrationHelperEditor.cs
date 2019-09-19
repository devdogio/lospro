using UnityEngine;
using System.Collections;
using UnityEditor;
using Devdog.General.Editors;

namespace Devdog.LosPro.Editors
{
    public class IntegrationHelperEditor : IntegrationHelperEditorBase
    {
        [MenuItem(LosPro.ToolsMenuPath + "Integrations", false, 0)]
        public static void ShowWindow()
        {
            EditorWindow.GetWindow<IntegrationHelperEditor>(true, "Integrations", true);
        }

        protected override void DrawIntegrations()
        {

            ShowIntegration("PlayMaker", "Quickly make gameplay prototypes, A.I. behaviors, animation graphs, interactive objects, cut-scenes, walkthroughs", GetUrlForProductWithID("368"), "PLAYMAKER");
            ShowIntegration("Behavior Designer", "Behavior trees are used by AAA studios to create a lifelike AI. With Behavior Designer, you can bring the power of behaviour trees to Unity!", GetUrlForProductWithID("15277"), "BEHAVIOR_DESIGNER");

        }
    }
}