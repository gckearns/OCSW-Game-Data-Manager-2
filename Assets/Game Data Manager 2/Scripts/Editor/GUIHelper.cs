using UnityEngine;
using System.Collections;
using System.Reflection;
using UnityEditor;

namespace GameDataManager
{
    public class GUIHelper
    {

        public static void GetGUI(GUIObject[] guiObjects)
        {
            for (int i = 0; i < guiObjects.Length; i++)
            {
                System.Type guiDataType = guiObjects[i].GetType();
                var guiData = guiObjects[i];

                if (guiDataType == typeof(GUIString))
                {
                    guiData = OnGUIString(guiData as GUIString);
                }

                else if (guiDataType == typeof(GUIInt))
                {
                    guiData = OnGUIInt(guiData as GUIInt);
                }

                else if (guiDataType == typeof(GUIEnum))
                {
                    guiData = OnGUIEnum(guiData as GUIEnum);
                }

                else if (guiDataType == typeof(GUIDictionary))
                {
                    guiData = OnGUIDictionary(guiData as GUIDictionary);
                }

                else if (guiDataType == typeof(GUIGameObject))
                {
                    guiData = OnGUIGameObject(guiData as GUIGameObject);
                }

                else if (guiDataType == typeof(GUIFloat))
                {
                    guiData = OnGUIFloat(guiData as GUIFloat);
                }

                else if (guiDataType == typeof(GUIBool))
                {
                    guiData = OnGUIBool(guiData as GUIBool);
                }
            }
        }

        static GUIString OnGUIString(GUIString guiMember)
        {
            if (guiMember.isLabel)
            {
                EditorGUILayout.LabelField(guiMember.label, guiMember.value);
                return guiMember;
            }
            else if (guiMember.ElementType != null)
            {
                GameElement element = GameDatabaseManager.Database[guiMember.ElementType][guiMember.value];
                string itemLabel = element != null ? element.Name : "";
                if (GUILayout.Button(itemLabel))
                {
                    if (!ValuesWindow.isOpen)
                    {
                        ValuesWindow w = EditorWindow.GetWindow<ValuesWindow>();
                        w.modifiedGUIObject = guiMember;
                        w.ShowUtility();
                    }
                }
                return guiMember;
            }
            else
            {
                guiMember.value = EditorGUILayout.TextField(guiMember.label, guiMember.value);
                return guiMember;
            }
        }
                
        static GUIInt OnGUIInt(GUIInt guiMember)
        {
            if (guiMember.isSlider)
            {
                guiMember.value = EditorGUILayout.IntSlider(guiMember.label, guiMember.value, guiMember.MinValue, guiMember.MaxValue);
            }
            else if (guiMember.hasMinValue)
            {
                guiMember.value = MyGUILayout.IntFieldMin(guiMember.label, guiMember.value, guiMember.MinValue);
            }
            else
            {
                guiMember.value = EditorGUILayout.IntField(guiMember.label, guiMember.value);
            }

            return guiMember;
        }
                
        static GUIEnum OnGUIEnum(GUIEnum guiMember)
        {
            guiMember.value = EditorGUILayout.EnumPopup(guiMember.label, guiMember.value);
            return guiMember;
        }

        static GUIDictionary OnGUIDictionary(GUIDictionary guiMember)
        {
            guiMember.showFoldout = EditorGUILayout.Foldout(guiMember.showFoldout, guiMember.label);
            if (guiMember.showFoldout)
            {
                EditorGUILayout.BeginHorizontal();
                {
                    EditorGUILayout.LabelField(guiMember.keysLabel);
                    EditorGUILayout.LabelField(guiMember.valuesLabel);
                    if (GUILayout.Button("+"))
                    {
                        if (!ValuesWindow.isOpen)
                        {
                            ValuesWindow w = EditorWindow.GetWindow<ValuesWindow>();
                            w.modifiedArray = guiMember.value;
                            w.ShowUtility();
                        }
                    }
                }
                EditorGUILayout.EndHorizontal();
                for (int i = 0; i < guiMember.value.Count; i++)
                {
                    EditorGUILayout.BeginHorizontal();
                    {
                        string itemLabel = GameDatabaseManager.Database[guiMember.value.KeyType][guiMember.value.Keys[i]].Name;
                        if (GUILayout.Button(itemLabel))
                        {
                            if (!ValuesWindow.isOpen)
                            {
                                ValuesWindow w = EditorWindow.GetWindow<ValuesWindow>();
                                w.modifiedArray = guiMember.value;
                                w.ShowUtility();
                            }
                        }
                        guiMember.value.Values[i] = EditorGUILayout.IntField(guiMember.value.Values[i]);
                        if (GUILayout.Button("-"))
                        {
                            guiMember.value.RemoveAt(i);
                        }
                    }
                    EditorGUILayout.EndHorizontal();
                }
            }
            return guiMember;
        }

        static GUIGameObject OnGUIGameObject(GUIGameObject guiMember)
        {
            guiMember.GameObject = EditorGUILayout.ObjectField(guiMember.label, guiMember.GameObject, typeof(GameObject),false, GUILayout.MinWidth(256)) as GameObject;
            return guiMember;
        }

        static GUIFloat OnGUIFloat(GUIFloat guiMember)
        {
            guiMember.value = EditorGUILayout.FloatField(guiMember.label, guiMember.value);
            return guiMember;
        }

        static GUIBool OnGUIBool(GUIBool guiMember)
        {
            guiMember.value = EditorGUILayout.Toggle(guiMember.label, guiMember.value);
            return guiMember;
        }
    }
}
