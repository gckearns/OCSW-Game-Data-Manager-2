using UnityEngine;
using System.Collections;

namespace GameDataManager
{
    public class GUIBool : GUIObject
    {
        public new bool value { get; set; }

        public GUIBool() { }

        public GUIBool(string label)
        {
            this.label = label;
            value = false;
        }

        public override GUIObject Copy()
        {
            GUIBool copy = new GUIBool();
            copy.label = this.label;
            copy.value = this.value;
            return copy;
        }

        public override string ToString()
        {
            return string.Format("[GUIBool]{0}:{1}", label, value);
        }
    }
}