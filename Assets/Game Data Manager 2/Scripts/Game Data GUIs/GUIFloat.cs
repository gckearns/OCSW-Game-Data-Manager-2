using UnityEngine;
using System.Collections;

namespace GameDataManager
{
    public class GUIFloat : GUIObject
    {
        public new float value { get; set; }

        public GUIFloat() { }

        public GUIFloat(string label)
        {
            this.label = label;
            value = 0f;
        }

        public GUIFloat(string label, float value)
        {
            this.label = label;
            this.value = value;
        }

        public override GUIObject Copy()
        {
            GUIFloat copy = new GUIFloat();
            copy.label = this.label;
            copy.value = this.value;
            return copy;
        }

        public override string ToString()
        {
            return string.Format("[GUIFloat]{0}:{1}", label, value);
        }
    }
}