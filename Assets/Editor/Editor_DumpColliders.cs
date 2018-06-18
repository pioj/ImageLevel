using UnityEditor;
using UnityEngine;

namespace pioj.ImageLevel
{

    public class Editor_DumpColliders : ScriptableWizard
    {

        [Tooltip("CompositeCollider2D you want to extract from your Tilemap")]
        public CompositeCollider2D LevelCollider;
        private Vector2[] ColPoints;

        [MenuItem("Tools/ImageLevel/(Extra). Extract colliders from a CompositeCollider2D...")]
        static void CreateWizard()
        {
            ScriptableWizard.DisplayWizard<Editor_DumpColliders>("Extract CompositeCollider2D", "Export to new Gameobject");
        }

        void OnWizardCreate()
        {
            if (LevelCollider != null)
            {
                Transform Root = LevelCollider.transform.parent;
                ExtractColliders();
            }
        }

        void OnWizardUpdate()
        {
            helpString = "Be sure you have a CompositeCollider2D in your Tilemap!";
        }

        // When the user presses the "Apply" button OnWizardOtherButton is called.
        void OnWizardOtherButton() { }


        //el metodo
        void ExtractColliders()
        {
            int colscount = LevelCollider.pathCount;
            if (colscount > 0)
            {
                for (int i = 0; i < colscount; i++)
                {
                    int m = LevelCollider.GetPathPointCount(i);
                    ColPoints = new Vector2[m];
                    int n = LevelCollider.GetPath(i, ColPoints);

                    //detect which collider shape is better
                    if (ColPoints.Length == 4) CreateBoxCollider();
                    if (ColPoints.Length > 4) CreatePolyCollider();
                }
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            EditorUtility.FocusProjectWindow();

        }



        void CreateBoxCollider()
        {
            GameObject temp = new GameObject("Col", typeof(PolygonCollider2D));
            temp.transform.SetParent(null);
            PolygonCollider2D PolyCol = temp.GetComponent<PolygonCollider2D>();
            PolyCol.SetPath(0, ColPoints);
            //
            Vector2 boxcenter = PolyCol.bounds.center;
            Vector2 boxsize = PolyCol.bounds.size;
            DestroyImmediate(PolyCol);
            //
            BoxCollider2D BoxCol = temp.AddComponent<BoxCollider2D>();
            temp.transform.position = boxcenter;
            //BoxCol.offset = boxcenter;
            BoxCol.size = boxsize;
        }


        void CreatePolyCollider()
        {
            GameObject temp = new GameObject("Col", typeof(PolygonCollider2D));
            temp.transform.SetParent(null);
            PolygonCollider2D PolyCol = temp.GetComponent<PolygonCollider2D>();
            PolyCol.SetPath(0, ColPoints);
        }

    }
}



