using UnityEngine;
using System.Collections;
using UnityEditor;

namespace GameDataManager
{
    public class GameDataManagerWindow : EditorWindow
    {
        [MenuItem("Manager/Game Data Manager Window")]
        public static void Init()
        {
            GameDataManagerWindow window = GetWindow<GameDataManagerWindow>();
            window.Show();
        }
        [MenuItem("Manager/GDMW Reset")]
        public static void Reset()
        {
            GameDataManagerWindow window = GetWindow<GameDataManagerWindow>();
            GameDatabaseManager.ResetDatabase();
            GameDatabaseManager.SaveDatabase();
            window.Close();
        }

        GUISkin gSkin;

        private int toolSelected;
        private int itemSelected;
        private GameDatabase myDatabase = GameDatabaseManager.Database;
        private GameElementList selectedDatabase;
        private GameElement selectedGameData;

        void OnEnable()
        {
            Debug.Log("GameDataManagerWindow script OnEnable");
            minSize = new Vector2(544, 256);
            gSkin = Resources.Load("gskin") as GUISkin;
        }

        void OnDisable()
        {
            GameDatabaseManager.SaveDatabase();
        }

        private void LoadDatabase()
        {
            //selectedDatabase = myDatabase[GameUtility.ItemEnums[selectedTool]];
            selectedDatabase = myDatabase[GameUtility.GameElements[selectedTool]];
            Debug.Log("Loaded selected database: " + selectedDatabase.ToString());
            EditorGUIUtility.keyboardControl = 0;
        }

        void OnGUI()
        {
            GUI.skin = gSkin;
            MyMenu();
            MyToolbar();
            MyTabs();
            MyDataview();
            MyInfobar();
        }

        int selectedMenu = -1;

        void MyMenu()
        {
            Rect rect = EditorGUILayout.BeginHorizontal(GUILayout.Height(10), GUILayout.Width(position.width));
            EditorGUI.DrawRect(rect, Color.white);
            selectedMenu = MyGUILayout.MenuBar(selectedMenu, new string[] { "File", "Edit", "View", "Help" }, GUILayout.Width(160));
            EditorGUILayout.EndHorizontal();
        }

        void MyToolbar()
        {
            EditorGUILayout.BeginHorizontal(GUILayout.Height(48), GUILayout.Width(144));
            if (GUILayout.Button("New", GUILayout.Width(48), GUILayout.Height(48)))
            {
                if (!ElementPropertiesWindow.isOpen)
                {
                    ElementPropertiesWindow w = ScriptableObject.CreateInstance<ElementPropertiesWindow>();
                    // validate a selected database
                    w.elementList = selectedDatabase;
                    w.parentWindow = this;
                    w.ShowUtility();
                }
                //Refresh and select the new data
            }
            if (GUILayout.Button("Delete", GUILayout.Width(48), GUILayout.Height(48)))
            {
                // Put a warning dialogue here
                // validate a selected database
                selectedDatabase.Remove(selectedGameData);
            }
            if (GUILayout.Button("Save", GUILayout.Width(48), GUILayout.Height(48)))
            {
                GameDatabaseManager.SaveDatabase();
            }
            EditorGUILayout.EndHorizontal();
        }

        private int selectedTool = -1;

        void MyTabs()
        {
            EditorGUILayout.BeginHorizontal(GUILayout.Height(32), GUILayout.Width(192));
            EditorGUI.BeginChangeCheck();
            selectedTool = MyGUILayout.ToggleBar(selectedTool, GameUtility.ItemTypeStrings);
            if (EditorGUI.EndChangeCheck())
                LoadDatabase();
            EditorGUILayout.EndHorizontal();
        }

        private string leftSearch;
        private string rightSearch;
        private Vector2 leftScroll;
        private Vector2 rightScroll;
        private int mySelection;

        void MyDataview()
        {
            Rect rect = EditorGUILayout.BeginHorizontal(GUILayout.ExpandHeight(true));
            GUIStyle myButton = new GUIStyle(GUI.skin.button);
            myButton.normal.background = null;
            GUIStyle leftVerticalStyle = new GUIStyle(GUI.skin.scrollView);
            leftVerticalStyle.margin = new RectOffset(0, 4, 0, 2);
            GUIStyle rightVerticalStyle = new GUIStyle(GUI.skin.scrollView);
            rightVerticalStyle.margin = new RectOffset(4, 0, 0, 2);
            GUIStyle scrollStyle = new GUIStyle(GUI.skin.scrollView);
            scrollStyle.normal.background = Resources.Load<Texture2D>("ColdSteelTexture");
            selectedGameData = null;
            rect = EditorGUILayout.BeginVertical(leftVerticalStyle, GUILayout.ExpandWidth(true));
            { // Begin Left Pane
                EditorGUI.DrawRect(rect, Color.white);
                leftSearch = EditorGUILayout.TextField("Search", leftSearch, "SearchField");
                rect = EditorGUILayout.BeginHorizontal(GUILayout.Height(16));
                {
                    GUILayout.Button("Name", myButton, GUILayout.MinWidth(96), GUILayout.Height(16));
                    GUILayout.Button("ID", myButton, GUILayout.MinWidth(48), GUILayout.Height(16));
                    GUILayout.Button("Category", myButton, GUILayout.MinWidth(64), GUILayout.Height(16));
                }
                EditorGUILayout.EndHorizontal();
                leftScroll = EditorGUILayout.BeginScrollView(leftScroll, scrollStyle);
                {
                    EditorGUILayout.BeginHorizontal(GUILayout.Height(16));
                    {
                        if (selectedDatabase != null)
                        {
                            if (selectedDatabase.Count > 0)
                            {
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
            rect = EditorGUILayout.BeginVertical(rightVerticalStyle, GUILayout.ExpandWidth(true));
            { // Begin Right Pane
                EditorGUI.DrawRect(rect, Color.white);
                rightSearch = EditorGUILayout.TextField("Search", rightSearch, "SearchField");
                rect = EditorGUILayout.BeginHorizontal(GUILayout.Height(16));
                {
                    GUILayout.Button("Field", myButton, GUILayout.Width(128), GUILayout.Height(16));
                    GUILayout.Button("Value", myButton, GUILayout.MinWidth(96), GUILayout.Height(16));
                }
                EditorGUILayout.EndHorizontal();
                rightScroll = EditorGUILayout.BeginScrollView(rightScroll, scrollStyle);
                {
                    if (selectedGameData != null)
                    {
                        selectedGameData.OnGUI();
                        GUIHelper.GetGUI(selectedGameData);
                    }
                }
                EditorGUILayout.EndScrollView();
            } // End Right Pane
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndHorizontal();
        }

        void MyInfobar()
        {
            Rect rect = EditorGUILayout.BeginHorizontal(GUILayout.Height(10), GUILayout.Width(position.width));
            //		EditorGUI.DrawRect(rect, new Color(0.6353f,0.6353f,0.6353f));
            EditorGUI.DrawRect(rect, new Color(0.7608f, 0.7608f, 0.7608f));
            EditorGUILayout.LabelField("Some information", GUILayout.MinWidth(172));
            EditorGUILayout.LabelField("Object Count:", (selectedDatabase != null) ? selectedDatabase.Count.ToString() : "", GUILayout.MinWidth(172), GUILayout.MaxWidth(196));
            EditorGUILayout.LabelField("Field Count:", "150", GUILayout.MinWidth(172), GUILayout.MaxWidth(196));
            EditorGUILayout.EndHorizontal();
        }
    }
}
