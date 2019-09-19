using System.Collections.Generic;
using System.Linq;
using System.Text;
using Devdog.General.Editors;
using UnityEditor;
using UnityEngine;

namespace Devdog.LosPro.Editors
{
    public class SightTargetCategoryEditor : EditorCrudBase<TargetCategory>
    {
        protected override List<TargetCategory> crudList
        {
            get { return new List<TargetCategory>(LosManager.instance.settings.targetCategories); }
            set { LosManager.instance.settings.targetCategories = value.ToArray(); }
        }

        public Editor itemEditorInspector;


        public SightTargetCategoryEditor(string singleName, string pluralName, EditorWindow window)
            : base(singleName, pluralName, window)
        {
            
        }

        protected override bool MatchesSearch(TargetCategory item, string searchQuery)
        {
            string search = searchQuery.ToLower();
            return (item.bitFlagID.ToString().Contains(search) || item.name.ToLower().Contains(search));
        }

        protected override void CreateNewItem()
        {
            var item = new TargetCategory();
            item.bitFlagID = (crudList.Count > 0) ? crudList.Max(o => o.bitFlagID) * 2 : 1;
            AddItem(item, true);
        }

        public override void DuplicateItem(int index)
        {
            var item = Clone(index);
            item.bitFlagID = (crudList.Count > 0) ? crudList.Max(o => o.bitFlagID) * 2 : 1;
            item.name += "(duplicate)";
            AddItem(item);
        }

        public override void Draw()
        {
            if (LosManager.instance == null || LosManager.instance.settings == null)
            {
                GUILayout.Label("No LosManager or Settings database found, can't edit.");
                return;
            }

            base.Draw();
        }

        protected override void DrawSidebarRow(TargetCategory item, int i)
        {
            //GUI.color = new Color(1.0f,1.0f,1.0f);
            BeginSidebarRow(item, i);

            DrawSidebarRowElement("#" + item.bitFlagID.ToString(), 40);
            DrawSidebarRowElement(item.name, 260);
            
            EndSidebarRow(item, i);
        }

        protected override void DrawDetail(TargetCategory item, int index)
        {
            EditorGUIUtility.labelWidth = EditorGUIUtility.labelWidth;


            EditorGUILayout.BeginVertical("box");

            EditorGUILayout.LabelField("Bitflag ID", item.bitFlagID.ToString());
            EditorGUILayout.Space();

            EditorGUILayout.LabelField("The name of the category, is displayed in the tooltip in UI elements.");
            item.name = EditorGUILayout.TextField("Category name", item.name);



            EditorGUILayout.EndVertical();


            EditorGUIUtility.labelWidth = 0;
        }

        protected override bool IDsOutOfSync()
        {
            return false;
        }

        protected override void SyncIDs()
        {

        }
    }
}
