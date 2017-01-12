using UnityEngine;
using System.Collections;

namespace GameDataManager
{
    public struct GUIEnum
    {
        public string label;
        public System.Enum value;

        public GUIEnum(string label)
        {
            this.label = label;

            value = null;
        }

        public GUIEnum(string label, System.Enum value)
        {
            this.label = label;
            this.value = value;
        }
    }
}
