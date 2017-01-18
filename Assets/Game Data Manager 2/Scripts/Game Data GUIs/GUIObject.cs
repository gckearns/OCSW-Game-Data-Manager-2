using UnityEngine;
using System.Collections;
using System;

namespace GameDataManager
{
    public class GUIObject
    {
        public virtual string label { get; protected set; }
        public object value { get; set; }

        public GUIObject() { }

        public GUIObject(string label)
        {
            this.label = label;
            value = 0;
        }

        public GUIObject(string label, object value)
        {
            this.label = label;
            this.value = value;
        }

        public virtual GUIObject Copy()
        {
            GUIObject copy = new GUIObject();
            copy.label = this.label;
            copy.value = this.value;
            return copy;
        }

        public override string ToString()
        {
            return string.Format("[GUIData]{0}:{1}", label, value);
        }
    }
}
