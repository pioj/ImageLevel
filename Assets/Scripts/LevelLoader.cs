using System.Collections;
using UnityEngine;

public class LevelLoader : MonoBehaviour {

    public Texture2D LevelMap;

    public Color2Prefab[] color2prefab;

	// Use this for initialization
	void Awake () {
        LoadMap();
	}

    void EmptyMap() {
        while (transform.childCount > 0) {
            Transform c = transform.GetChild(0);
            c.SetParent(null);
            Destroy(c.gameObject);
        }
    }

    // Update is called once per frame
    void LoadMap() {
        EmptyMap();

        //
        Color32[] allPixels = LevelMap.GetPixels32();
        int width = LevelMap.width;
        int height = LevelMap.height;

        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++) {
                //
                SpawnTileAt(allPixels[(y * width) + x], x, y);
                }
            }
        }
    

    void SpawnTileAt(Color32 c, int x, int y) {
        //para el transparente/vacio
        Color32 empty = new Color32(255, 255, 255, 255);
        if (c.a <= 0 || c.Equals(empty)) return; 

        foreach (Color2Prefab ctp in color2prefab) {
            if (ctp.color.Equals(c)) { //o, si el R, G, B y A son los mismos....
                GameObject go = (GameObject)Instantiate(ctp.prefab, new Vector3(x, y, 0), Quaternion.identity);
                go.transform.SetParent(transform);
                return;
            }
        }
        Debug.LogError("no hay más elementos: " + c.ToString() );
    }

}
