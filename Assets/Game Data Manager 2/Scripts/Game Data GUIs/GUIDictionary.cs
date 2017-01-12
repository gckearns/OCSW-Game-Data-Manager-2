using UnityEngine;
using System.Collections;

namespace GameDataManager
{
    public class GUIDictionary
    {
        public string label { get; private set;}
        public string value { get; set; }
        public GUIDictionary(string label)
        {
            this.label = label;
            value = "";
        }
        
        public GUIDictionary(string label, string value)
        {
            this.label = label;
            this.value = value;
        }
        
        public override string ToString()
        {
            return string.Format("[GUIDictionary]{0}:{1}",label,value);
        }
    }
}
