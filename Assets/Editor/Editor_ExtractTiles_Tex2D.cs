using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;

namespace pioj.ImageLevel
{
    public class Editor_ExtractTiles_Tex2D : EditorWindow
    {
        public Texture2D tex;
        private static string path;
        
        // Add menu named "My Window" to the Window menu
        [MenuItem("Tools/ImageLevel/Step 1. Create Palette from Texture2D...")]
        static void Init()
        {
            // Get existing open window or if none, make a new one:
            Editor_ExtractTiles_Tex2D window = (Editor_ExtractTiles_Tex2D)EditorWindow.GetWindow(typeof(Editor_ExtractTiles_Tex2D));
            window.titleContent = new GUIContent("Extract Palette...");
            window.maxSize = new Vector2(300, 300);
            window.Show();
        }



        void OnGUI()
        {
            if (!tex) EditorGUILayout.HelpBox("Please Select or Load a Texture file.",MessageType.Warning);
            
            EditorGUILayout.Space(10);
            EditorGUILayout.BeginHorizontal(GUILayout.ExpandWidth(true));
            EditorGUIUtility.labelWidth = 15;
            if (GUILayout.Button("..."))
            {
                path = BuscaTexture();
                if (File.Exists(path))
                {
                    var file = File.ReadAllBytes(path);
                    tex = new Texture2D(2, 2);
                    tex.LoadImage(file);
                    tex.name = path;
                    tex.filterMode = FilterMode.Point;
                }
            }
            EditorGUILayout.LabelField("OR", GUILayout.MaxWidth(25));
            //
            var obj = EditorGUILayout.ObjectField("", tex, typeof(Texture2D), false, GUILayout.Height(17));
            if (obj) tex = (Texture2D) obj;
            //Image.filterMode = FilterMode.Point;
            EditorGUILayout.EndHorizontal();

            if (tex)
            {
                EditorGUILayout.Space(5);
                if (GUILayout.Button("Generate Palette", GUILayout.ExpandWidth(true)))
                {
                    GeneratePalette();
                }
                EditorGUILayout.LabelField("Preview: " + tex.name);
                EditorGUI.DrawPreviewTexture(new Rect(5,60,base.position.width-10, base.position.height-10) , tex, null, ScaleMode.ScaleToFit);
            }
        }

        private static string BuscaTexture()
        {
            path = EditorUtility.OpenFilePanelWithFilters("Select Texture...", Application.dataPath,
                new string[] {"png", "png"});
            return path;
        }
        
        
        //el metodo
        void GeneratePalette()
        {
            Color32[] AllPixels = tex.GetPixels32();

            var pathname = EditorUtility.SaveFilePanelInProject("Save Palette Asset as...", "Palette_xxx", "asset", "asset");

            //string pathname = AssetDatabase.GenerateUniqueAssetPath("Assets/Resources/" + tex.name.ToString() + "_Palette" + ".asset");

            SO_Color2Prefab tempAsset = CreateInstance<SO_Color2Prefab>();
            AssetDatabase.CreateAsset(tempAsset, pathname);

            for (int i = 0; i < AllPixels.Length; i++)
            {
                tempAsset.color2prefab.Add(new Color2Prefab(AllPixels[i]));
            }
            tempAsset.Clean();

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            EditorUtility.FocusProjectWindow();
        }
        
    }
}
