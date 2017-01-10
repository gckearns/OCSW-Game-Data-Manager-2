using UnityEditor;
using UnityEngine;

namespace GDM1
{
    public class CostWindow : EditorWindow
    {

        [MenuItem("Manager/Cost Window")]
        public static void Init()
        {
            CostWindow window = ScriptableObject.CreateInstance<CostWindow>();
            window.ShowUtility();
        }

        int toolSelected;
        int itemSelected;
        Vector2 scrollPos;

        void OnGUI()
        {
            EditorGUI.DrawRect(new Rect(0, 0, position.width, position.height), new Color(0.6353f, 0.6353f, 0.6353f));
            Rect resizeRect;
            EditorGUILayout.LabelField("Commodity Costs");
            EditorGUILayout.BeginHorizontal();
            {// window & buttons
                Rect windowRect = EditorGUILayout.BeginHorizontal();
                { //window columns
                    resizeRect = new Rect(windowRect.xMax - 5, windowRect.y, 10, windowRect.height);
                    EditorGUI.DrawRect((windowRect), Color.white);
                    //				EditorGUI.DrawRect((resizeRect),Color.red);
                    EditorGUILayout.BeginVertical();
                    {
                        GUILayout.Button("Commodity");
                        itemSelected = GUILayout.SelectionGrid(itemSelected, new string[] { "Demo", "Demo", "Demo" }, 1);
                    }
                    EditorGUILayout.EndVertical();
                    EditorGUILayout.BeginVertical();
                    {
                        GUILayout.Button("Amount");
                        itemSelected = GUILayout.SelectionGrid(itemSelected, new string[] { "1", "2", "3" }, 1);
                    }
                    EditorGUILayout.EndVertical();
                }
                EditorGUILayout.EndHorizontal(); //window columns
                EditorGUILayout.BeginVertical(GUILayout.Width(8));
                { //Buttons
                    GUILayout.Button("+");
                    GUILayout.Button("-");
                    GUILayout.Button("^");
                    GUILayout.Button("v");
                }
                EditorGUILayout.EndVertical(); //Buttons
            }
            EditorGUILayout.EndHorizontal(); // window & buttons
            EditorGUILayout.BeginHorizontal();
            {// select commodity & set amount
                EditorGUILayout.BeginVertical();
                {
                    EditorGUILayout.LabelField("Commodity");
                    scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
                    { // select commodity window
                        EditorGUILayout.BeginVertical();
                        {
                            GUILayout.Button("Commodity");
                            itemSelected = GUILayout.SelectionGrid(itemSelected, new string[] { "Oxygen", "Metal", "Dirt" }, 1);
                        }
                        EditorGUILayout.EndVertical();
                    }
                    EditorGUILayout.EndScrollView(); // select commodity window
                }
                EditorGUILayout.EndVertical();
                EditorGUILayout.BeginVertical(GUILayout.Width(8));
                { //Amount
                    EditorGUILayout.LabelField("Amount");
                    EditorGUILayout.IntField(0);
                }
                EditorGUILayout.EndVertical(); //Amount
            }
            EditorGUILayout.EndHorizontal(); // select commodity & set amount
                                             //		if (resizeRect.Contains(Event.current.mousePosition)) {
            EditorGUIUtility.AddCursorRect(resizeRect, MouseCursor.ResizeHorizontal);
            //		}
        }
    }
}
