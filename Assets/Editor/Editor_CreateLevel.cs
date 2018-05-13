﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Editor_CreateLevel : ScriptableWizard {

    public string LevelName = "";
    public Sprite Level;
    public SO_Color2Prefab LevelPalette;
    public Color32 Empty = new Color32(255,255,255,255); //white by default

    [MenuItem("Tools/ImageLevel/Step 2. Create Level GameObject From Sprite...")]
    static void CreateWizard()
    {
        ScriptableWizard.DisplayWizard<Editor_CreateLevel>("Create Level GameObject from Sprite", "Generate");

    }

    void OnWizardCreate()
    {
        if (Level != null && LevelPalette != null)
        {
            if (LevelName == null || LevelName == "") LevelName = "NewLevel";
            
            GameObject go = new GameObject(LevelName);
            GenerateLevel(go);
        }
    }

    void OnWizardUpdate()
    {
        helpString = "Please select a Sprite first!";
    }

    // When the user presses the "Apply" button OnWizardOtherButton is called.
    void OnWizardOtherButton() { }


    void GenerateLevel(GameObject parent) {
        Color32[] AllPixels = Level.texture.GetPixels32();
        int Levelx = (int)Level.rect.x;
        int Levely = (int)Level.rect.y;
        int LevelWidth = (int)Level.rect.width;
        int LevelHeight = (int)Level.rect.height;

        for (int x = Levelx; x < LevelWidth; x++)
        {
            for (int y = Levely; y < LevelHeight; y++)
            {
                //
                SpawnTileAt(parent.transform, AllPixels[(y * LevelWidth) + x], x, y);
            }
        }
    }


    void SpawnTileAt(Transform parent, Color32 c, int x, int y) {
        if (c.Equals(Empty)) return;

        SO_Color2Prefab mySO = LevelPalette;

        foreach (Color2Prefab ctp in mySO.color2prefab) {
            if (ctp.color.Equals(c)) {
                GameObject go = (GameObject)Instantiate(ctp.prefab, new Vector3(x, y, 0), Quaternion.identity);
                go.transform.SetParent(parent);
                return;
            }
        }
        Debug.LogError("no more items in list: " + c.ToString());
    }
}