using UnityEngine;
using System.Collections;
using System;
using UnityEditor;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.Collections.Generic;

namespace GameDataManager
{
    [XmlInclude(typeof(Unit))]
    [XmlInclude(typeof(Commodity))]
    [XmlInclude(typeof(StorageType))]
    [System.Serializable]
    public abstract class GameElement
    {
        private GUIObject[] guiData;
        protected GUIObject[] guiDetails;
        protected GUIString name = new GUIString("Name");
        protected GUIString id = new GUIString("ID", true);
        protected GUIBool isDefault = new GUIBool("Defines Defaults");
        protected GUIString parentId = new GUIString("Parent");

        public GameElement() { }

        public GameElement(GameElement gameElement)
        {
            GUIData = gameElement.GUIData;
        }

        public string source { get; set; }
        public int index { get; set; }
        public GameElementType gameElementType { get; set; }
        
        [XmlIgnore]
        public virtual GUIObject[] GUIData
        {
            get
            {
                if (guiData == null)
                {
                    guiData = new GUIObject[]
                    {
                        name, //0
                        id //1
                    };
                }
                return guiData;
            }

            protected set
            {
                guiData = new GUIObject[value.Length];
                for (int i = 0; i < value.Length; i++)
                {
                    guiData[i] = value[i].Copy();
                }
                name = guiData[0] as GUIString;
                id = guiData[1] as GUIString;
            }
        }

        [XmlIgnore]
        public virtual GUIObject[] GUIDetails
        {
            get
            {
                if (guiDetails == null)
                {
                    guiDetails = new GUIObject[]
                    {
                        name, //0
                        id, //1
                        isDefault, //2
                        parentId //3
                    };
                }
                return guiDetails;
            }

            protected set
            {
                guiDetails = new GUIObject[value.Length];
                for (int i = 0; i < value.Length; i++)
                {
                    guiDetails[i] = value[i].Copy();
                }
                name = guiDetails[0] as GUIString;
                id = guiDetails[1] as GUIString;
            }
        }

        #region GUI Properties
        public string Name
        {
            get
            {
                return name.value;
            }
            set
            {
                name.value = value;
            }
        }

        public string ID
        {
            get
            {
                return id.value;
            }
            set
            {
                id.value = value;
            }
        }

        public bool IsDefault
        {
            get
            {
                return isDefault.value;
            }
            set
            {
                isDefault.value = value;
            }
        }

        public string ParentId
        {
            get
            {
                return parentId.value;
            }
            set
            {
                parentId.value = value;
            }
        }
        #endregion


        public GameElement(string name, string id)
        {
            this.Name = name;
            this.ID = id;
        }

        public static implicit operator bool(GameElement item)
        {
            return (item != null);
        }
    }

    public enum GameElementType
    {
        None = 0,
        Unit,
        Commodity
    }
}
