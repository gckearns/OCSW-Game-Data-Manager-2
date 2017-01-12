using UnityEngine;
using System.Collections;

namespace GameDataManager
{
    public struct GUIInt
    {        
        public string label { get; private set; }        
        public int value { get; set; }
        
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
        public override string ToString()
        {
            return string.Format("[GUIInt]{0}:{1}",label,value);
        }
    }
}
