using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace GameDataManager
{
    [System.Serializable]
    public class Unit : GameElement
    {
        private List<string> costItemIDs = new List<string>();
        private List<int> costAmounts = new List<int>();
        private List<string> yieldItemIDs = new List<string>();
        private List<int> yieldAmounts = new List<int>();
        private List<CommodityCategory> storageTypes = new List<CommodityCategory>();
        private List<int> storageLimits = new List<int>();

        public Unit() {}

        public Unit(string name, string id)
        {
            this.name = name;
            this.id = id;
        }

        public string prefabPath { get; set; }
        public int size { get; set; }
        public int constructionTime { get; set; }
        public int yieldTime { get; set; }
        public UnitType unitType { get; set; }
        [XmlIgnore]
        public Dictionary<string,int> Costs {
            get
            {
                Dictionary<string, int> costs = new Dictionary<string, int>();
                for (int i = 0; i < costItemIDs.Count; i++)
                {
                    costs.Add(costItemIDs[i], costAmounts[i]);
                }
                return costs;
            }
            set
            {
                costItemIDs = new List<string>(value.Keys);
                costAmounts = new List<int>(value.Values);
            }
        }
        [XmlIgnore]
        public Dictionary<string, int> Yields
        {
            get
            {
                Dictionary<string, int> yields = new Dictionary<string, int>();
                for (int i = 0; i < yieldItemIDs.Count; i++)
                {
                    yields.Add(yieldItemIDs[i], yieldAmounts[i]);
                }
                return yields;
            }
            set
            {
                yieldItemIDs = new List<string>(value.Keys);
                yieldAmounts = new List<int>(value.Values);
            }
        }
        [XmlIgnore]
        public Dictionary<CommodityCategory, int> Storage
        {
            get
            {
                Dictionary<CommodityCategory, int> storage = new Dictionary<CommodityCategory, int>();
                for (int i = 0; i < storageTypes.Count; i++)
                {
                    storage.Add(storageTypes[i], storageLimits[i]);
                }
                return storage;
            }
            set
            {
                storageTypes = new List<CommodityCategory>(value.Keys);
                storageLimits = new List<int>(value.Values);
            }
        }

    }

    public enum UnitType
    {
        None = 0,
        Building,
        Ship
    }
}

