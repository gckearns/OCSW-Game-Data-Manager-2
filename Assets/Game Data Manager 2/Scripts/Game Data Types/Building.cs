using UnityEngine;
using System.Collections;

namespace GameDataManager
{
    public class Building : Unit
    {
        public Building() { }

        public string description { get; set; }
        public int power { get; set; }
        public int workers { get; set; }
        public BuildingCategory category { get; set; }
    }

    public enum BuildingCategory
    {
        None = 0,
        Housing,
        Industry,
        Services
    }
}
