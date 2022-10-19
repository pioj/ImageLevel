using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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
            window.maxSize = new Vector2(300, 500);
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
                if (GUILayout.Button("Generate Palette...", GUILayout.ExpandWidth(true)))
                {
                    GeneratePalette();
                    this.ShowNotification(new GUIContent("Palette Asset created."));
                }
                EditorGUILayout.Space(5);
                if (GUILayout.Button("Generate .ACT File...", GUILayout.ExpandWidth(true)))
                {
                    GenerateACT();
                    this.ShowNotification(new GUIContent(".ACT file created."));
                }

                EditorGUILayout.LabelField("Colors detected: " + DetectColors(tex));
                EditorGUILayout.Space(5);
                EditorGUILayout.LabelField("Preview: " + tex.name);
                EditorGUI.DrawPreviewTexture(new Rect(5,60,base.position.width-10, base.position.height-10) , tex, null, ScaleMode.ScaleToFit);
            }
        }

        private static int DetectColors(Texture2D textu)
        {
            if (!textu) throw new Exception("Error! No texture loaded previously!");

            var count = textu.GetPixels32().Distinct().Count();
            return count;
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
            Color32[] AllPixels = tex.GetPixels32().Distinct().ToArray();

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
        
        private void GenerateACT()
        {
            Color32[] AllPixels = tex.GetPixels32().Distinct().ToArray();
            var colorCount = AllPixels.Length;

            var pathname = EditorUtility.SaveFilePanelInProject("Save .ACT color table as...", "ImageLevel_Colors", "act", "act");
            using (var stream = File.Open(pathname, FileMode.Create))
            {
                using (var writer = new BinaryWriter(stream, Encoding.Unicode, false))
                {
                    /*
                    writer.Write(1.250F);
                    writer.Write(@"c:\Temp");
                    writer.Write(10);
                    writer.Write(true);
                    */

                    foreach (var coloritem in AllPixels)
                    {
                        writer.Write(coloritem.r);
                        writer.Write(coloritem.g);
                        writer.Write(coloritem.b);
                    }
                    
                    //Now we fill the rest of the color palette with zeros, following Adobe scheme...
                    for (int j = colorCount; j < 768; j++)
                    {
                        writer.Write((byte)0);
                    }
                    
                    //truncate the file length so it's never larger than 772 bytes (256 colors *3 channels + 4bytes header)
                    writer.BaseStream.SetLength(772);
                    
                    //Now we end the file. The first 1byte tell the number of colors, the last 2bytes tell which color is transparent.
                    writer.BaseStream.Position = writer.BaseStream.Length - 3;
                    writer.Write((byte)colorCount);
                    writer.Write((byte)255);
                    writer.Write((byte)255);
                }
            }
        }
        
    }
}
