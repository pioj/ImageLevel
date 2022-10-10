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

            //GUILayout.BeginVertical();
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
            //GUILayout.EndVertical();
            
            /*seccion del boton para generar un ACT de Adobe
            EditorGUILayout.Separator();
            if (GUILayout.Button("Generate .ACT file from current Palette"))
            {
            }
            */

            //EditorGUILayout.PropertyField(colors.GetArrayElementAtIndex(0).FindPropertyRelative("color"));
            serializedObject.ApplyModifiedProperties();
        }
    }
}

