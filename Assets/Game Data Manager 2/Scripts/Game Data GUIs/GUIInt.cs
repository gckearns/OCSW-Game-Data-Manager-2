using UnityEngine;
using System.Collections;

namespace GameDataManager
{
    public struct GUIInt
    {
        public string label;
        public int value;

        public GUIInt(string label)
        {
            this.label = label;
            value = 0;
        }

        public GUIInt(string label, int value)
        {
            this.label = label;
            this.value = value;
        }
    }
}
