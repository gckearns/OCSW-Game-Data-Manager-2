using UnityEngine;
using System.Collections;

namespace GameDataManager
{
    public class GUIString : GUIObject
    {
        public new string value { get; set; }
        public bool isLabel { get; set; }
        private System.Type elementType;
        private string elementTypeAssemblyName;

        public GUIString() { }
        
        public GUIString(string label)
        {
            this.label = label;
            value = "";
            isLabel = false;
        }

        public GUIString(string label, bool isLabel)
        {
            this.label = label;
            this.isLabel = isLabel;
            value = "";
        }

        public GUIString(string label, System.Type elementType)
        {
            this.label = label;
            this.value = "";
            this.isLabel = false;
            this.ElementType = elementType;
        }

        public string ElementTypeAssemblyName
        {
            get
            {
                return elementType.AssemblyQualifiedName;
            }
            set
            {
                elementTypeAssemblyName = value;
                elementType = System.Type.GetType(elementTypeAssemblyName);
            }
        }

        public System.Type ElementType
        {
            get
            {
                return elementType;
            }

            set
            {
                elementType = value;
                elementTypeAssemblyName = elementType.AssemblyQualifiedName;
            }
        }

        public override GUIObject Copy()
        {
            GUIString copy = new GUIString();
            copy.label = this.label;
            copy.value = this.value;
            copy.isLabel = this.isLabel;
            copy.elementType = this.elementType;
            copy.elementTypeAssemblyName = this.elementTypeAssemblyName;
            return copy;
        }

        public override string ToString()
        {
            return string.Format("[GUIString]{0}:{1}",label,value);
        }
    }
}
