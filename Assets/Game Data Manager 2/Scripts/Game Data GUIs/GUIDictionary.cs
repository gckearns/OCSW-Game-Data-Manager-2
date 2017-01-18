using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GameDataManager
{
    public class GUIDictionary : GUIObject
    {
        public new StringIntDictionary value { get; set; }
        public bool showFoldout { get; set; }
        public string keysLabel { get; set; }
        public string valuesLabel { get; set; }

        public GUIDictionary() { }

        public GUIDictionary(string label, string keysLabel, string valuesLabel, System.Type keyType)
        {
            this.label = label;
            this.keysLabel = keysLabel;
            this.valuesLabel = valuesLabel;
            value = new StringIntDictionary();
            value.KeyType = keyType;
        }
        
        public GUIDictionary(string label, string keysLabel, string valuesLabel, StringIntDictionary value)
        {
            this.label = label;
            this.keysLabel = keysLabel;
            this.valuesLabel = valuesLabel;
            this.value = value;
        }

        public override GUIObject Copy()
        {
            GUIDictionary copy = new GUIDictionary();
            copy.label = this.label;
            copy.value = this.value;
            copy.showFoldout = this.showFoldout;
            copy.keysLabel = this.keysLabel;
            copy.valuesLabel = this.valuesLabel;
            return copy;
        }

        public override string ToString()
        {
            return string.Format("[GUIDictionary]{0}:{1}",label,value.KeyType.Name);
        }
    }
}
