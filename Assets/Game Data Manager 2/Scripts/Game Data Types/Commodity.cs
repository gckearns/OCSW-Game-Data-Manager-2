using UnityEngine;
using System.Collections;
using System.Xml.Serialization;

namespace GameDataManager
{
    public class Commodity : GameElement
    {
        private GUIObject[] guiData;
        protected GUIString storageType = new GUIString("Storage Type",typeof(StorageType));
        protected GUIFloat priceMin = new GUIFloat("Min Price");
        protected GUIFloat priceMax = new GUIFloat("Max Price");

        public Commodity() {}

        public Commodity(string name, string id, bool isDefault, string parentId)
        {
            this.Name = name;
            this.ID = id;
            this.IsDefault = isDefault;
            this.ParentId = parentId;
        }

        public Commodity(Commodity gameElement)
        {
            GUIData = gameElement.GUIData;
        }

        [XmlIgnore]
        public override GUIObject[] GUIData
        {
            get
            {
                if (guiData == null)
                {
                    guiData = new GUIObject[]
                    {
                        name,
                        id,
                        storageType,
                        priceMin,
                        priceMax
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
                storageType = guiData[2] as GUIString;
                priceMin = guiData[3] as GUIFloat;
                priceMax = guiData[4] as GUIFloat;
            }
        }

        public string StorageType
        {
            get
            {
                return storageType.value;
            }
            set
            {
                storageType.value = value;
            }
        }

        public string StorageTypeAssemblyName
        {
            get
            {
                return storageType.ElementTypeAssemblyName;
            }
            set
            {
                storageType.ElementTypeAssemblyName = value;
            }
        }

        public float PriceMin
        {
            get
            {
                return priceMin.value;
            }
            set
            {
                priceMin.value = value;
            }
        }

        public float PriceMax
        {
            get
            {
                return priceMax.value;
            }
            set
            {
                priceMax.value = value;
            }
        }
    }
}
