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
                
                if (properties[i].PropertyType == typeof(GUIInt))
                {
                    GUIInt guiMember = OnGUIInt((GUIInt)properties[i].GetValue(gameElement,null));
                    properties[i].SetValue(gameElement, guiMember,null);
                }
                                
                if (properties[i].PropertyType == typeof(GUIEnum))
                {
                    GUIEnum guiMember = OnGUIEnum((GUIEnum)properties[i].GetValue(gameElement,null));
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
            //return obj.GetType().GetProperties(BindingFlags.Instance | BindingFlags.FlattenHierarchy | BindingFlags.Public);
            return obj.GetType().GetProperties();
        }

        static GUIString OnGUIString(GUIString guiMember)
        {
            guiMember.value = EditorGUILayout.TextField(guiMember.label, guiMember.value);
            return guiMember;
        }
                
        static GUIInt OnGUIInt(GUIInt guiMember)
        {
            guiMember.value = EditorGUILayout.IntField(guiMember.label, guiMember.value);
            return guiMember;
        }
                
        static GUIEnum OnGUIEnum(GUIEnum guiMember)
        {
            guiMember.value = EditorGUILayout.EnumPopupField(guiMember.label, guiMember.value);
            return guiMember;
        }
    }
}
