using System;
using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using Devdog.General.Editors;
using EditorStyles = UnityEditor.EditorStyles;
using EditorUtility = UnityEditor.EditorUtility;

namespace Devdog.LosPro.Editors
{
    public class MainEditor : EditorWindow
    {
        private static int toolbarIndex { get; set; }

        public static EmptyEditor mainEmptyEditor { get; set; }
        public static SightTargetCategoryEditor targetCategoryEditor { get; set; }

        public static List<IEditorCrud> editors = new List<IEditorCrud>(8);

        private static MainEditor _window;
        public static MainEditor window
        {
            get
            {
                if (_window == null)
                    _window = GetWindow<MainEditor>(false, "Los Pro - Main manager", false);

                return _window;
            }
        }

        protected string[] editorNames
        {
            get
            {
                string[] items = new string[editors.Count];
                for (int i = 0; i < editors.Count; i++)
                {
                    items[i] = editors[i].ToString();
                }

                return items;
            }
        }

        [MenuItem("Tools/Los Pro/Main editor", false, -99)] // Always at the top
        public static void ShowWindow()
        {
            _window = GetWindow<MainEditor>(false, "Los Pro - Main manager", true);
        }

        private void OnEnable()
        {
            minSize = new Vector2(600.0f, 400.0f);
            toolbarIndex = 0;

            CreateEditors();
        }

        public static void SelectTab(Type type)
        {
            int i = 0;
            foreach (var editor in editors)
            {
                var ed = editor as EmptyEditor;
                if (ed != null)
                {
                    bool isChildOf = ed.childEditors.Select(o => o.GetType()).Contains(type);
                    if (isChildOf)
                    {
                        toolbarIndex = i;
                        for (int j = 0; j < ed.childEditors.Count; j++)
                        {
                            if (ed.childEditors[j].GetType() == type)
                            {
                                ed.toolbarIndex = j;
                            }
                        }

                        toolbarIndex = i;
                        ed.Focus();
                        window.Repaint();
                        return;
                    }
                }

                if (editor.GetType() == type)
                {
                    toolbarIndex = i;
                    editor.Focus();
                    window.Repaint();
                    return;
                }

                i++;
            }

            Debug.LogWarning("Trying to select tab in main editor, but type isn't in editor.");
        }

        public virtual void CreateEditors()
        {
            editors.Clear();
            mainEmptyEditor = new EmptyEditor("Items editor", this);
            mainEmptyEditor.childEditors.Add(new SightTargetCategoryEditor("Category", "Categories", this));
            editors.Add(mainEmptyEditor);

            editors.Add(new SettingsEditor("Settings", "Settings", this));
        }

        protected virtual void DrawToolbar()
        {
            EditorGUILayout.BeginHorizontal();

            var toolbarStyle = new GUIStyle(EditorStyles.toolbarButton);
            toolbarStyle.fixedHeight = 40;


            int before = toolbarIndex;
            toolbarIndex = GUILayout.Toolbar(toolbarIndex, editorNames, toolbarStyle);
            if (before != toolbarIndex)
            {
                editors[toolbarIndex].Focus();
            }

            EditorGUILayout.EndHorizontal();
        }

        public void OnGUI()
        {
            DrawToolbar();

            if (toolbarIndex < 0 || toolbarIndex >= editors.Count || editors.Count == 0)
            {
                toolbarIndex = 0;
                CreateEditors();
            }

            // Draw the editor
            editors[toolbarIndex].Draw();

            if (GUI.changed && LosManager.instance != null)
                EditorUtility.SetDirty(LosManager.instance.settings); // To make sure it gets saved.
        }
    }
}