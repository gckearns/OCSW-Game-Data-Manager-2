using UnityEngine;
using System.Collections;

namespace GameDataManager
{
    public class GUIEnum : GUIObject
    {
        public new System.Enum value { get; set; }

        public GUIEnum() { }

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

        public override GUIObject Copy()
        {
            GUIEnum copy = new GUIEnum();
            copy.label = this.label;
            copy.value = this.value;
            return copy;
        }

        public override string ToString()
        {
            return string.Format("[GUIEnum]{0}:{1}",label,value);
        }
    }
}
