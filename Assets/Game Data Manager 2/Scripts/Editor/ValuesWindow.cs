using UnityEngine;
using System.Collections;
using UnityEditor;

namespace GameDataManager
{
    public class ValuesWindow : EditorWindow
    {
        GUISkin gSkin;

        private int toolSelected;
        private int itemSelected;
        private GameDatabase myDatabase = GameDatabaseManager.Database;
        private GameElementList selectedDatabase;
        private GameElement selectedGameData;
        public static bool isOpen = false;

        public StringIntDictionary modifiedArray;
        public string modifiedID = "";
        public GUIString modifiedGUIObject;

        void OnEnable()
        {
            Debug.Log("SelectWindow script OnEnable");
            minSize = new Vector2(544, 256);
            gSkin = Resources.Load("gskin") as GUISkin;
            isOpen = true;
        }

        void OnDisable()
        {
            GameDatabaseManager.SaveDatabase();
            isOpen = false;
        }



        private void LoadDatabase()
        {
            if (modifiedArray != null)
            {
                selectedDatabase = myDatabase[modifiedArray.KeyType];
            }
            else if (modifiedGUIObject != null)
            {
                selectedDatabase = myDatabase[modifiedGUIObject.ElementType];
            }
            Debug.Log("Loaded selected database: " + selectedDatabase.ToString());
            EditorGUIUtility.keyboardControl = 0;
        }

        void Validate(StringIntDictionary dictionary)
        {
            if (!modifiedArray.ContainsKey(selectedGameData.ID))
            {
                modifiedArray.Add(selectedGameData.ID, 5);

                FocusWindowIfItsOpen<GameDataManagerWindow>();

                Close();

                modifiedArray = null;
            }
            else
            {
                RemoveNotification();
                ShowNotification(new GUIContent("Duplicate item!"));
            }
        }

        void Validate(GUIString guiObject)
        {
            guiObject.value = selectedGameData.ID;

            FocusWindowIfItsOpen<GameDataManagerWindow>();

            Close();

            modifiedGUIObject = null;
            
        }

        void OnGUI()
        {
            GUI.skin = gSkin;
            LoadDatabase();
            MyDataview();
            MyToolbar();
        }

        void MyToolbar()
        {
            EditorGUILayout.BeginHorizontal(GUILayout.Height(32));
            if (GUILayout.Button("OK", GUILayout.Height(32)))
            {
                if (modifiedArray != null)
                {
                    Validate(modifiedArray);
                }
                else if (modifiedGUIObject != null && modifiedGUIObject.GetType() == typeof(GUIString))
                {
                    Validate(modifiedGUIObject);
                }
            }
            if (GUILayout.Button("Cancel", GUILayout.Height(32)))
            {
                Close();
            }
            EditorGUILayout.EndHorizontal();
        }

        private string leftSearch = "";
        private string rightSearch = "";
        private Vector2 leftScroll;
        private Vector2 rightScroll;
        private int mySelection;

        void MyDataview()
        {
            selectedGameData = null;
            Rect rect = EditorGUILayout.BeginVertical(MyGUIStyle.LeftVerticalStyle, GUILayout.ExpandWidth(true));
            { // Begin Left Pane
                EditorGUI.DrawRect(rect, Color.white);
                leftSearch = EditorGUILayout.TextField("Search", leftSearch, "SearchField");
                rect = EditorGUILayout.BeginHorizontal(GUILayout.Height(16));
                {
                    GUILayout.Button("Name", MyGUIStyle.MyButton, GUILayout.MinWidth(96), GUILayout.Height(16));
                    GUILayout.Button("ID", MyGUIStyle.MyButton, GUILayout.MinWidth(48), GUILayout.Height(16));
                    GUILayout.Button("Category", MyGUIStyle.MyButton, GUILayout.MinWidth(64), GUILayout.Height(16));
                }
                EditorGUILayout.EndHorizontal();
                leftScroll = EditorGUILayout.BeginScrollView(leftScroll, MyGUIStyle.ScrollStyle);
                {
                    EditorGUILayout.BeginHorizontal(GUILayout.Height(16));
                    {
                        if (selectedDatabase != null)
                        {
                            if (selectedDatabase.Count > 0)
                            {
                                if (modifiedID != "")
                                {
                                    mySelection = selectedDatabase.IDs.IndexOf(modifiedID);
                                    modifiedID = "";
                                }
                                if (mySelection >= selectedDatabase.Count)
                                {
                                    mySelection = (selectedDatabase.Count - 1);
                                }
                                if (mySelection < 0)
                                {
                                    mySelection = 0;
                                }
                                EditorGUI.BeginChangeCheck();
                                mySelection = GUILayout.SelectionGrid(mySelection, selectedDatabase.Names.ToArray(), 1, "SelectionButton", GUILayout.MinWidth(96));
                                mySelection = GUILayout.SelectionGrid(mySelection, selectedDatabase.IDs.ToArray(), 1, "SelectionButton", GUILayout.MinWidth(64));
                                mySelection = GUILayout.SelectionGrid(mySelection, selectedDatabase.ElementTypes.ToArray(), 1, "SelectionButton", GUILayout.MinWidth(64));
                                if (EditorGUI.EndChangeCheck())
                                {
                                    EditorGUIUtility.keyboardControl = 0;
                                }
                                selectedGameData = selectedDatabase[mySelection];
                            }
                        }
                    }
                    EditorGUILayout.EndHorizontal();
                }
                EditorGUILayout.EndScrollView();
            } // End Left Pane
            EditorGUILayout.EndVertical();
        }
    }
}
