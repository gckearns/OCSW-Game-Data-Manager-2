using System.Xml.Serialization;
using UnityEditor;

namespace GDM1
{
    [XmlInclude(typeof(Building))]
    [XmlInclude(typeof(Commodity))]
    [XmlInclude(typeof(Ship))]
    [System.Serializable]
    public abstract class GameItem
    {

        //public ItemType itemType { get; set; }
        public string itemName { get; set; }
        public string itemID { get; set; }
        public abstract string catName { get; }
        public string description { get; set; }

        public GameItem() { }

        public virtual void OnGUI()
        {
            itemName = EditorGUILayout.TextField("Name", itemName);
            EditorGUILayout.LabelField("ID", itemID);
        }
    }
}

