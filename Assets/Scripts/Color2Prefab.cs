using System.Collections;
using UnityEngine;

[System.Serializable]
public struct Color2Prefab {

    public Color32 color;
    public GameObject prefab;

    public Color2Prefab(Color32 c) {
        prefab = null;
        color = c;
    }
	
}
