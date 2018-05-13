using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;


[CreateAssetMenu(fileName = "LevelPallete", menuName = "ImageLevel/Palette", order = 1)]
[System.Serializable]
public class SO_Color2Prefab : ScriptableObject {

    public List<Color2Prefab> color2prefab = new List<Color2Prefab>();

    public void Clean() {
        if (color2prefab != null && color2prefab.Count > 1) {
            List<Color2Prefab> noDupes = color2prefab.Distinct().ToList();
            color2prefab = noDupes;
        }
    }
        
}
