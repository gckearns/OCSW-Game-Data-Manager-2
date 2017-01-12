using UnityEngine;
using System.Collections;

namespace GameDataManager
{
    public struct GUIEnum
    {
        public string label { get; private set; }
        public System.Enum value { get; set; }
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
        public override string ToString()
        {
            return string.Format("[GUIEnum]{0}:{1}",label,value);
        }
    }
}
