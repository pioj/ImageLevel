using UnityEngine;

namespace pioj.ImageLevel
{

    [System.Serializable]
    public struct Color2Prefab
    {

        public Color32 color;
        public GameObject prefab;
        public bool excludeFromMerge;

        public Color2Prefab(Color32 c)
        {
            prefab = null;
            color = c;
            excludeFromMerge = false;
        }

    }
}
