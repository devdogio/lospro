using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Devdog.General.Editors;
using UnityEditor;
using UnityEngine;

namespace Devdog.LosPro.Editors
{
    public class SettingsEditor : EditorCrudBase<SettingsEditor.CategoryLookup>
    {
        public class CategoryLookup
        {
            public string name { get; set; }
            public List<SerializedProperty> serializedProperties = new List<SerializedProperty>(8);

            public CategoryLookup()
            {

            }
            public CategoryLookup(string name)
            {
                this.name = name;
            }

            public override bool Equals(object obj)
            {
                var o = obj as CategoryLookup;
                return o != null && o.name == name;
            }

            public override int GetHashCode()
            {
                return base.GetHashCode();
            }
        }

        private SerializedObject _serializedObject;
        public SerializedObject serializedObject
        {
            get
            {
                if (_serializedObject == null)
                    _serializedObject = new SerializedObject(settings);

                return _serializedObject;
            }
        }


        protected LosSettings _settings;
        protected LosSettings settings
        {
            get
            {
                if (_settings == null && LosManager.instance != null)
                    _settings = LosManager.instance.settings;

                return _settings;
            }
        }

        protected override List<CategoryLookup> crudList
        {
            get
            {
                var list = new List<CategoryLookup>(8);
                if (settings != null)
                {
                    var fields = settings.GetType().GetFields();

                    CategoryLookup currentCategory = null;
                    foreach (var field in fields)
                    {
                        var customAttributes = field.GetCustomAttributes(typeof(CategoryAttribute), true);
                        if (customAttributes.Length == 1)
                        {
                            // Got a category marker
                            currentCategory = new CategoryLookup(customAttributes[0].ToString());
                            list.Add(currentCategory);
                        }

                        if (currentCategory != null)
                        {
                            var prop = serializedObject.FindProperty(field.Name);
                            if (prop != null)
                                currentCategory.serializedProperties.Add(prop);
                        }
                    }
                }

                return list;
            }
            set
            {
                // Doesn't do anything...
            }
        }

        public SettingsEditor(string singleName, string pluralName, EditorWindow window)
            : base(singleName, pluralName, window)
        {
            this.canCreateItems = false;
            this.canDeleteItems = false;
            this.canReOrderItems = false;
            this.canDuplicateItems = false;
            this.hideCreateItem = true;
        }

        protected override void CreateNewItem()
        {

        }

        public override void DuplicateItem(int index)
        {

        }

        protected override bool MatchesSearch(CategoryLookup category, string searchQuery)
        {
            string search = searchQuery.ToLower();
            return category.name.ToLower().Contains(search) || category.serializedProperties.Any(o => o.displayName.ToLower().Contains(search));
        }

        protected override void DrawSidebarRow(CategoryLookup category, int i)
        {
            BeginSidebarRow(category, i);

            DrawSidebarRowElement(category.name, 400);

            EndSidebarRow(category, i);
        }

        protected override void DrawDetail(CategoryLookup category, int index)
        {
            EditorGUILayout.BeginVertical("box");
            EditorGUIUtility.labelWidth = EditorGUIUtility.labelWidth;


            serializedObject.Update();
            foreach (var setting in category.serializedProperties)
            {
                EditorGUILayout.PropertyField(setting, true);
            }
            serializedObject.ApplyModifiedProperties();


            EditorGUIUtility.labelWidth = 0; // Resets it to the default
            EditorGUILayout.EndVertical();
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
