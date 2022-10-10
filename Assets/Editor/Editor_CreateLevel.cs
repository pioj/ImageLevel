using System.Runtime.InteropServices;
using UnityEngine;
using UnityEditor;

namespace pioj.ImageLevel
{

    public class Editor_CreateLevel : ScriptableWizard
    {

        [Tooltip("Will be called 'NewLevel' by default")]
        public string LevelName = "";
        [Tooltip("The Sprite or Texture2D that will be read")]
        public Sprite Level;
        [Tooltip("Palette of colors & prefabs will be used for this level")]
        public SO_Color2Prefab LevelPalette;
        [Tooltip("Remove all Colliders from non-excluded?")]
        public bool DecorationMode;
        [Tooltip("This color will be ignored")]
        public Color32 Empty = new Color32(255, 255, 255, 255); //white by default

        private GameObject staticsParent;

        [MenuItem("Tools/ImageLevel/Step 2. Create Level GameObject From Sprite...")]
        static void CreateWizard()
        {
            ScriptableWizard.DisplayWizard<Editor_CreateLevel>("Create Level GameObject from Sprite", "Generate");
        }

        void OnWizardCreate()
        {
            var checkmain = (Level != null && LevelPalette != null);
            if (!checkmain) return;

            if (LevelName == null || LevelName == "") LevelName = "NewLevel";

            GameObject go = new GameObject(LevelName);
            staticsParent = new GameObject("Statics");
            staticsParent.transform.SetParent(go.transform);

            GenerateLevel(go);

            //Add the composite collider
            if (!DecorationMode) 
            { 
                staticsParent.AddComponent<Rigidbody2D>();
                staticsParent.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
                staticsParent.AddComponent<CompositeCollider2D>(); 
            }
            //mark Level root as static GO's
            staticsParent.isStatic = true;
        }

        void OnWizardUpdate()
        {
            helpString = "Please select a Sprite first!";
        }

        // When the user presses the "Apply" button OnWizardOtherButton is called.
        void OnWizardOtherButton() { }


        void GenerateLevel(GameObject parent)
        {
            Color32[] AllPixels; // = Level.texture.GetPixels32();

            int Levelx = (int)Level.rect.x;
            int Levely = (int)Level.rect.y;
            int LevelWidth = (int)Level.rect.width;
            int LevelHeight = (int)Level.rect.height;

            Color[] myPixels = Level.texture.GetPixels(Levelx, Levely, LevelWidth, LevelHeight);
            AllPixels = new Color32[myPixels.Length];
            for (int i = 0; i < myPixels.Length; i++)
            {
                AllPixels[i].r = (byte)(myPixels[i].r * 255);
                AllPixels[i].g = (byte)(myPixels[i].g * 255);
                AllPixels[i].b = (byte)(myPixels[i].b * 255);
                AllPixels[i].a = (byte)(myPixels[i].a * 255);
            }

            for (int x = Levelx; x < LevelWidth; x++)
            {
                for (int y = Levely; y < LevelHeight; y++)
                {
                    //
                    SpawnTileAt(parent.transform, AllPixels[(y * LevelWidth) + x], x, y);
                }
            }

        }


        void SpawnTileAt(Transform parent, Color32 c, int x, int y)
        {
            if (c.Equals(Empty)) return;

            SO_Color2Prefab mySO = LevelPalette;
            string sufix = "(Clone)";
            foreach (Color2Prefab ctp in mySO.color2prefab)
            {
                if (ctp.color.Equals(c))
                {
                    GameObject go = (GameObject)Instantiate(ctp.prefab, new Vector3(x, y, 0), Quaternion.identity);
                    go.name = go.name.Substring(0, go.name.Length - sufix.Length);

                    //to group or not to group
                    if (ctp.excludeFromMerge == false)
                    {
                        go.transform.SetParent(staticsParent.transform);
                        SetStaticsCollider(go.transform);
                        CheckDecorationMode(go.transform);
                        go.isStatic = true;
                    }
                    else
                    {
                        go.transform.SetParent(parent);
                    }
                    return;
                }
            }
            Debug.LogError("no more items in list: " + c.ToString());
        }


        void SetStaticsCollider(Transform myTile)
        {
            if (!DecorationMode)
            {
                Collider2D myCol = myTile.GetComponent<Collider2D>();
                if (myCol == null)
                {
                    myTile.gameObject.AddComponent<BoxCollider2D>();
                    SetStaticsCollider(myTile);
                }

                myCol.usedByComposite = true;
            }
        }

        void CheckDecorationMode(Transform myTile)
        {
            if (!DecorationMode) return;
            
            var hasOtherCol = myTile.TryGetComponent(typeof(CircleCollider2D), out Component circleCol);
            if (hasOtherCol) DestroyImmediate(circleCol);
            hasOtherCol = false;
            
            hasOtherCol = myTile.TryGetComponent(typeof(BoxCollider2D), out Component boxCol);
            if (hasOtherCol) DestroyImmediate(boxCol);
            hasOtherCol = false; 
            
            hasOtherCol = myTile.TryGetComponent(typeof(PolygonCollider2D), out Component polyCol);
            if (hasOtherCol) DestroyImmediate(polyCol);
            hasOtherCol = false; 

            hasOtherCol = myTile.TryGetComponent(typeof(EdgeCollider2D), out Component edgeCol);
            if (hasOtherCol) DestroyImmediate(edgeCol);
            hasOtherCol = false; 

            var otherCols = myTile.GetComponentsInChildren<Collider2D>(true);
            if (otherCols == null || otherCols.Length < 1) return;
            
            foreach (var colChild in otherCols)
            {
                DestroyImmediate(colChild);
            }
        }

    }
}
