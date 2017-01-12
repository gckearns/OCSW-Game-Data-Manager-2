using UnityEngine;
using System.Collections;

namespace GameDataManager
{
    public class GUIString
    {
        public string label { get; private set; }
        public string value { get; set; }

        public GUIString(string label)
        {
            this.label = label;
            value = "";
        }

        public GUIString(string label, string value)
        {
            this.label = label;
            this.value = value;
        }

        public override string ToString()
        {
            return string.Format("[GUIString]{0},{1}",label,value);
        }
    }
}
