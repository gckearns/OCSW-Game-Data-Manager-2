using UnityEngine;
using System.Collections;
using System.Reflection;
using UnityEditor;

namespace GameDataManager
{
    public class GUIHelper
    {

        public static void GetGUI(GameElement gameElement)
        {
            FieldInfo[] fields = GetFields(gameElement);
            PropertyInfo[] properties = GetProperties(gameElement);
            for (int i = 0; i < properties.Length; i++)
            {
                if (properties[i].PropertyType == typeof(GUIString))
                {
                    GUIString guiMember = OnGUIString((GUIString)properties[i].GetValue(gameElement,null));
                    properties[i].SetValue(gameElement, guiMember,null);
                }
                    
            }
            //for (int i = 0; i < fields.Length; i++)
            //{
            //    if (fields[i].FieldType == typeof(GUIString))
            //    {
            //        GUIString guiMember = OnGUIString((GUIString)fields[i].GetValue(gameElement));
            //        fields[i].SetValue(gameElement, guiMember);
            //    }

            //}
        }

        static FieldInfo[] GetFields(GameElement obj)
        {
            //return obj.GetType().GetFields(BindingFlags.Instance | BindingFlags.FlattenHierarchy | BindingFlags.Public);
            return obj.GetType().GetFields();
        }

        static PropertyInfo[] GetProperties(GameElement obj)
        {
            //return obj.GetType().GetFields(BindingFlags.Instance | BindingFlags.FlattenHierarchy | BindingFlags.Public);
            return obj.GetType().GetProperties();
        }

        static GUIString OnGUIString(GUIString guiString)
        {
            guiString.value = EditorGUILayout.TextField(guiString.label, guiString.value);
            if (guiString.value == "xyz")
            {
                Debug.Log("match xyz");
            }
            return guiString;
        }
    }
}
