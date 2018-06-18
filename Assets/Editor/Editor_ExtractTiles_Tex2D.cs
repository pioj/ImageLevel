using UnityEditor;
using UnityEngine;

namespace pioj.ImageLevel
{

    public class Editor_ExtractTiles_Tex2D : ScriptableWizard
    {

        public Texture2D Image;


        [MenuItem("Tools/ImageLevel/Step 1. Create TileSet/From Texture2D...")]
        static void CreateWizard()
        {
            ScriptableWizard.DisplayWizard<Editor_ExtractTiles_Tex2D>("Create TileSet from Texture2D", "Create Tileset");
            //If you don't want to use the secondary button simply leave it out:
            //ScriptableWizard.DisplayWizard<WizardCreateLight>("Create Light", "Create");

        }

        void OnWizardCreate()
        {
            if (Image != null) GeneratePalette();
        }

        void OnWizardUpdate()
        {
            helpString = "Please select a Texture2D first!";
        }

        // When the user presses the "Apply" button OnWizardOtherButton is called.
        void OnWizardOtherButton() { }


        //el metodo
        void GeneratePalette()
        {
            Color32[] AllPixels = Image.GetPixels32();

            string pathname = AssetDatabase.GenerateUniqueAssetPath("Assets/Resources/" + Image.name.ToString() + "_Palette" + ".asset");

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



