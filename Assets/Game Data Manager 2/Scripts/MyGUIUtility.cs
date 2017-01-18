using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GameDataManager
{
    public class MyGUIUtility
    {
        public static GameElement[] GetFilteredArray(GameElementList gameElements, string search)
        {
            GameElement[] results = System.Array.FindAll(gameElements.ToArray(), x => x.Name.ToLower().Contains(search.ToLower()));
            return results;
        }

        public static GUIObject[] GetFilteredArray(GUIObject[] guiObjects, string search)
        {
            GUIObject[] results = System.Array.FindAll(guiObjects, x => x.label.ToLower().Contains(search.ToLower()));
            return results;
        }

        public static string[] GetNames(GameElement[] elements)
        {
            string[] results = new string[elements.Length];
            for (int i = 0; i < elements.Length; i++)
            {
                results[i] = elements[i].Name;
            }
            return results;
        }

        public static string[] GetIDs(GameElement[] elements)
        {
            string[] results = new string[elements.Length];
            for (int i = 0; i < elements.Length; i++)
            {
                results[i] = elements[i].ID;
            }
            return results;
        }

        public static string[] GetTypes(GameElement[] elements)
        {
            string[] results = new string[elements.Length];
            for (int i = 0; i < elements.Length; i++)
            {
                results[i] = elements[i].gameElementType.ToString();
            }
            return results;
        }

        public static GUIRow[] GetGUIRows(GameElement[] elements)
        {
            GUIRow[] results = new GUIRow[elements.Length];
            for (int i = 0; i < elements.Length; i++)
            {
                results[i] = new GUIRow(elements[i]);
            }
            return results;
        }
    }
}
