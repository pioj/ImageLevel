using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace pioj.ImageLevel
{
    [CustomEditor(typeof(SO_Color2Prefab)),CanEditMultipleObjects]
    public class Editor_SOColor2Prefab : Editor
    {
        private SerializedProperty colors;
        
        void OnEnable()
        {
            colors = serializedObject.FindProperty("color2prefab");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            /* -FOR EXPERIMENTAL BRANCH ONLY-
            EXPERIMENTAL_DrawNewInspector()
            */ //-FOR EXPERIMENTAL BRANCH ONLY-
            base.OnInspectorGUI();
         
            //EditorGUILayout.PropertyField(colors.GetArrayElementAtIndex(0).FindPropertyRelative("color"));
            serializedObject.ApplyModifiedProperties();
        }

        
        private void EXPERIMENTAL_DrawNewInspector()
        {
            foreach (SerializedProperty item in colors)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Color",GUILayout.MinWidth(50));
                EditorGUILayout.PropertyField(item.FindPropertyRelative("color"),GUIContent.none);
                EditorGUILayout.Space(10);
                EditorGUILayout.LabelField("Prefab",GUILayout.MinWidth(50));
                EditorGUILayout.PropertyField(item.FindPropertyRelative("prefab"),GUIContent.none,GUILayout.MinWidth(70));
                EditorGUILayout.Space(10);
                EditorGUILayout.LabelField("Exclude from Merge?",GUILayout.MinWidth(120));
                EditorGUILayout.PropertyField(item.FindPropertyRelative("excludeFromMerge"), GUIContent.none);
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.Separator();
            }
        }
    }
}

