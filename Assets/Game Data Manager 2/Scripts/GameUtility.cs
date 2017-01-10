using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace GameDataManager
{
    public class GameUtility
    {
        public static List<Type> GameElements = new List<Type>{typeof(Unit), typeof(Commodity) };

        public static List<GameElementType> ItemEnums = new List<GameElementType>(new GameElementType[]
            {
            GameElementType.Unit,
            GameElementType.Commodity
            }
        );

        public static List<BuildingCategory> BuildingEnums = new List<BuildingCategory>(new BuildingCategory[]
        {
            BuildingCategory.Housing,
            BuildingCategory.Industry,
            BuildingCategory.Services,
        }
    );

        private static string[] _ItemTypeStrings;
        public static string[] ItemTypeStrings
        {
            get
            {
                if (_ItemTypeStrings == null)
                {
                    _ItemTypeStrings = new string[GameElements.Count];
                    for (int i = 0; i < GameElements.Count; i++)
                    {
                        _ItemTypeStrings[i] = GameElements[i].ToString();
                    }
                }
                return _ItemTypeStrings;
            }
        }

        //public static T NewElement<T>(GameElementType type)
        //    where T : new ()
        //{
        //    return new T();
        //}

        //public static void Test()
        //{
        //    Type t = typeof(Building);
        //    GameElement e = NewElement<>(GameElementType.Unit);
        //}

        //public static ItemConstructor GetConstructor(GameElementType type)
        //{
        //    switch (type)
        //    {
        //        case GameElementType.Unit:
        //            return new ItemConstructor(NewBuilding);
        //        //				break;
        //        case GameElementType.Commodity:
        //            return new ItemConstructor(NewCommodity);
        //        //				break;
        //        default:
        //            return null;
        //    }
        //}

        public static Unit NewBuilding(GameElementType type, string name, string ID)
        {
            Unit item = new Unit();
            return item;
        }

        public static Commodity NewCommodity(GameElementType type, string name, string ID)
        {
            Commodity item = new Commodity();
            return item;
        }
    }
}
