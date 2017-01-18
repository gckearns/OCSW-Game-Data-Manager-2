using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace GameDataManager
{
    [System.Serializable]
    public class Unit : GameElement
    {
        private GUIObject[] guiData;
        protected GUIEnum unitType = new GUIEnum("Unit Type", UnitType.None);
        protected GUIGameObject prefabPath = new GUIGameObject("Prefab");
        protected GUIInt size = new GUIInt("Size",true,1,3);
        protected GUIInt constructTime = new GUIInt("Construction Time", 0);
        protected GUIInt yieldTime = new GUIInt("Yield Time", 0);
        protected GUIDictionary costs = new GUIDictionary("Costs", "Commodities", "Amounts", typeof(Commodity));
        protected GUIDictionary yields = new GUIDictionary("Yields", "Commodities", "Amounts", typeof(Commodity));
        protected GUIDictionary storage = new GUIDictionary("Storage", "Storage Types", "Amounts", typeof(StorageType));
        protected GUIEnum category = new GUIEnum("Building Category", BuildingCategory.None);
        protected GUIString description = new GUIString("Description");
        protected GUIInt power = new GUIInt("Power");
        protected GUIInt workers = new GUIInt("Workers");

        public Unit() {}

        public Unit(string name, string id, bool isDefault, string parentId)
        {
            this.Name = name;
            this.ID = id;
            this.IsDefault = isDefault;
            this.ParentId = parentId;
        }

        public Unit(Unit gameElement)
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
                        unitType,
                        prefabGUIGameObject,
                        size,
                        constructTime,
                        yieldTime,
                        costs,
                        yields,
                        storage,
                        category,
                        description,
                        power,
                        workers
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
                unitType = guiData[2] as GUIEnum;
                prefabPath = guiData[3] as GUIGameObject;
                size = guiData[4] as GUIInt;
                constructTime = guiData[5] as GUIInt;
                yieldTime = guiData[6] as GUIInt;
                costs = guiData[7] as GUIDictionary;
                yields = guiData[8] as GUIDictionary;
                storage = guiData[9] as GUIDictionary;
                category = guiData[10] as GUIEnum;
                description = guiData[11] as GUIString;
                power = guiData[12] as GUIInt;
                workers = guiData[13] as GUIInt;
            }
        }

        public UnitType UnitType
        {
            get
            {
                return (UnitType) unitType.value;
            }
            set
            {
                unitType.value = value;
            }
        }

        private GUIGameObject prefabGUIGameObject
        {
            get
            {
                if (prefabPath == null)
                {
                    prefabPath = new GUIGameObject("Prefab");
                }
                return prefabPath;
            }
        }

        [XmlIgnore]
        public GameObject Prefab
        {
            get
            {
                return prefabGUIGameObject.GameObject;
            }
        }

        public string PrefabPath
        {
            get
            {
                return prefabGUIGameObject.value;
            }
            set
            {
                prefabGUIGameObject.value = value;
            }
        }

        public int Size
        {
            get
            {
                return size.value;
            }
            set
            {
                size.value = value;
            }
        }

        public int ConstructTime
        {
            get
            {
                return constructTime.value;
            }
            set
            {
                constructTime.value = value;
            }
        }

        public int YieldTime
        {
            get
            {
                return yieldTime.value;
            }
            set
            {
                yieldTime.value = value;
            }
        }

        public StringIntDictionary Costs
        {
            get
            {
                return costs.value;
            }
            set
            {
                costs.value = value;
            }
        }

        public StringIntDictionary Yields
        {
            get
            {
                return yields.value;
            }
            set
            {
                yields.value = value;
            }
        }

        public StringIntDictionary Storage
        {
            get
            {
                return storage.value;
            }
            set
            {
                storage.value = value;
            }
        }

        public BuildingCategory Category
        {
            get
            {
                return (BuildingCategory) category.value;
            }
            set
            {
                category.value = value;
            }
        }

        public string Description
        {
            get
            {
                return description.value;
            }
            set
            {
                description.value = value;
            }
        }

        public int Power
        {
            get
            {
                return power.value;
            }

            set
            {
                power.value = value;
            }
        }

        public int Workers
        {
            get
            {
                return workers.value;
            }
            set
            {
                workers.value = value;
            }
        }
    }

    public enum UnitType
    {
        None = 0,
        Building,
        Ship
    }

    public enum BuildingCategory
    {
        None = 0,
        Housing,
        Industry,
        Services
    }
}
