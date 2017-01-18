using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace GameDataManager
{
    public class GameUtility
    {
        public static List<Type> GameElements = new List<Type>{typeof(Unit), typeof(Commodity), typeof(StorageType) };
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
                        _ItemTypeStrings[i] = GameElements[i].Name;
                    }
                }
                return _ItemTypeStrings;
            }
        }
    }
}
