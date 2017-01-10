using UnityEngine;
using System.Collections;

namespace GameDataManager
{
    public class Commodity : GameElement
    {
        public float priceMin { get; set; }
        public float priceMax { get; set; }
        public CommodityCategory category { get; set; }

        public Commodity() {}
    }

    public enum CommodityCategory
    {
        None = 0,
        Solid,
        Liquid,
        Gas
    }
}

