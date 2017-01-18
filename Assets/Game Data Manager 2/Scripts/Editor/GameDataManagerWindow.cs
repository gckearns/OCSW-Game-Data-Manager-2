using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Runtime.Serialization;

namespace GameDataManager
{
    public class GameDataManagerWindow : EditorWindow
    {
        [MenuItem("Manager/Game Data Manager Window")]
        public static void Init()
        {
            GameDataManagerWindow window = GetWindow<GameDataManagerWindow>();
            window.Show();
            TestObject.Test();
        }

        [MenuItem("Manager/GDMW Reset")]
        public static void Reset()
        {
            GameDataManagerWindow window = GetWindow<GameDataManagerWindow>();
            window.Close();
            GameDatabaseManager.ResetDatabase();
        }

        GUISkin gSkin;

        private int toolSelected;
        private int itemSelected;
        private GameDatabase myDatabase;
        public GameElementList selectedElementList;
        private GameElement selectedGameData;
        private GameElement[] shownElements;

        GameElement[] ShownElements
        {
            get
            {
                if (shownElements == null)
                {
                    shownElements = new GameElement[0];
                }
                return shownElements;
            }
        }

        GameDatabase MyDatabase
        {
            get
            {
                if (myDatabase == null)
                {
                    myDatabase = GameDatabaseManager.Database;
                }
                return myDatabase;
            }
        }

        void OnEnable()
        {
            Debug.Log("GameDataManagerWindow script OnEnable");
            minSize = new Vector2(544, 256);
            gSkin = Resources.Load("GUI Skins/gskin") as GUISkin;
            LoadDatabase();
        }

        void OnDisable()
        {
            Debug.Log("GameDataManagerWindow script OnDisable");
            GameDatabaseManager.SaveDatabase();
        }

        private void LoadDatabase()
        {
            myDatabase = null;
            selectedElementList = MyDatabase[GameUtility.GameElements[selectedTool]];
            Debug.Log("Loaded selected database: " + selectedElementList.ToString());
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
                NewGameElement();
            }
            if (GUILayout.Button("Delete", GUILayout.Width(48), GUILayout.Height(48)))
            {
                RemoveSelectedGameElement();
            }
            if (GUILayout.Button("Save", GUILayout.Width(48), GUILayout.Height(48)))
            {
                GameDatabaseManager.SaveDatabase();
            }
            if (GUILayout.Button("Load", GUILayout.Width(48), GUILayout.Height(48)))
            {
                GameDatabaseManager.LoadDatabase();
                LoadDatabase();
            }
            EditorGUILayout.EndHorizontal();
        }

        private int selectedTool = -1;
        public int SelectedTool
        {
            get
            {
                if (GameUtility.GameElements.Count == 0)
                {
                    return -1;
                }
                else
                {
                    return Mathf.Clamp(selectedTool, 0, GameUtility.GameElements.Count - 1);
                }
            }
            set
            {
                if (selectedTool != value)
                {
                    selectedTool = value;
                    LoadDatabase();
                }
                selectedTool = value;
            }
        }

        void MyTabs()
        {
            SelectedTool = MyGUILayout.ToggleBar(SelectedTool, GameUtility.ItemTypeStrings, GUILayout.Height(32), GUILayout.Width(192));
        }

        private string leftSearch = "";
        private string rightSearch = "";
        private Vector2 leftScroll;
        private Vector2 rightScroll;
        private int mySelection;
        private int mSelection;

        void MyDataview()
        {
            EditorGUILayout.BeginHorizontal(GUILayout.ExpandHeight(true));
            {
                selectedGameData = null;
                if (selectedElementList == null)
                {
                    LoadDatabase();
                }
                DataviewLeft();
                DataviewRight();
            }
            EditorGUILayout.EndHorizontal();
        }

        bool btnOn;
        public bool showPopup;

        void DataviewLeft()
        {
            Rect leftVerticalRect = EditorGUILayout.BeginVertical(MyGUIStyle.LeftVerticalStyle, GUILayout.ExpandWidth(true));
            { // Begin Left Pane
                EditorGUI.DrawRect(leftVerticalRect, Color.white);
                leftSearch = EditorGUILayout.TextField("Search", leftSearch, "SearchField");
                shownElements = MyGUIUtility.GetFilteredArray(selectedElementList, leftSearch);
                MyGUILayout.DataTableHeader(new string[] { "Name", "ID", "Category" },null,new float[] { 96, 48, 64 }, 16);
                leftScroll = EditorGUILayout.BeginScrollView(leftScroll, MyGUIStyle.ScrollStyle);
                {// Begin Scroll View
                    #region OldDataTableGUI
                    //EditorGUILayout.BeginHorizontal(GUILayout.Height(16));
                    //{
                    //    if (gameElements != null)
                    //    {
                    //        if (gameElements.Length > 0)
                    //        {
                    //            if (mySelection >= gameElements.Length)
                    //            {
                    //                mySelection = (gameElements.Length - 1);
                    //            }
                    //            if (mySelection < 0)
                    //            {
                    //                mySelection = 0;
                    //            }
                    //            EditorGUI.BeginChangeCheck();

                    //            string[] searchResults = MyGUIUtility.GetNames(gameElements);
                    //            mySelection = GUILayout.SelectionGrid(mySelection, searchResults, 1, "SelectionButton", GUILayout.MinWidth(96));

                    //            searchResults = MyGUIUtility.GetIDs(gameElements);
                    //            mySelection = GUILayout.SelectionGrid(mySelection, searchResults, 1, "SelectionButton", GUILayout.MinWidth(64));

                    //            searchResults = MyGUIUtility.GetTypes(gameElements);
                    //            mySelection = GUILayout.SelectionGrid(mySelection, searchResults, 1, "SelectionButton", GUILayout.MinWidth(64));

                    //            if (EditorGUI.EndChangeCheck())
                    //            {
                    //                EditorGUIUtility.keyboardControl = 0;
                    //            }
                    //            selectedGameData = gameElements[mySelection];
                    //        }
                    //    }
                    //}
                    //EditorGUILayout.EndHorizontal();
                    #endregion

                    GUIRow[] rows = MyGUIUtility.GetGUIRows(ShownElements);
                    mSelection = MyGUILayout.RowSelection(mSelection, rows, this);
                    if (mSelection >= 0 && mSelection < ShownElements.Length)
                    {
                        selectedGameData = ShownElements[mSelection];
                    }
                    Repaint();
                    if (showPopup)
                    {
                        showPopup = false;
                        PopupWindow.Show(new Rect(Event.current.mousePosition, Vector2.zero), new ContextPopup());                        
                    }
                }// End Scroll View
                EditorGUILayout.EndScrollView();
            } // End Left Pane
            EditorGUILayout.EndVertical();
        }

        void DataviewRight()
        {
            Rect rightVerticalRect = EditorGUILayout.BeginVertical(MyGUIStyle.RightVerticalStyle, GUILayout.ExpandWidth(true));
            { // Begin Right Pane
                EditorGUI.DrawRect(rightVerticalRect, Color.white);
                rightSearch = EditorGUILayout.TextField("Search", rightSearch, "SearchField");
                MyGUILayout.DataTableHeader(new string[] { "Field", "Value"}, null, new float[] { 128, 96 }, 16);
                rightScroll = EditorGUILayout.BeginScrollView(rightScroll, MyGUIStyle.ScrollStyle);
                {
                    if (selectedGameData != null)
                    {
                        GUIObject[] guiObjects = MyGUIUtility.GetFilteredArray(selectedGameData.GUIData, rightSearch);
                        GUIHelper.GetGUI(guiObjects);
                    }
                }
                EditorGUILayout.EndScrollView();
            } // End Right Pane
            EditorGUILayout.EndVertical();
        }

        void MyInfobar()
        {
            Rect rect = EditorGUILayout.BeginHorizontal(GUILayout.Height(10), GUILayout.Width(position.width));
            //		EditorGUI.DrawRect(rect, new Color(0.6353f,0.6353f,0.6353f));
            EditorGUI.DrawRect(rect, new Color(0.7608f, 0.7608f, 0.7608f));
            EditorGUILayout.LabelField("Some information", GUILayout.MinWidth(172));
            EditorGUILayout.LabelField("Object Count:", selectedElementList? ShownElements.Length.ToString() : "", GUILayout.MinWidth(172), GUILayout.MaxWidth(196));
            EditorGUILayout.LabelField("Field Count:", selectedGameData? selectedGameData.GUIData.Length.ToString() : "0", GUILayout.MinWidth(172), GUILayout.MaxWidth(196));
            EditorGUILayout.EndHorizontal();
        }

        public void NewGameElement()
        {
            if (!ElementPropertiesWindow.isOpen)
            {
                ElementPropertiesWindow elementWindow = CreateInstance<ElementPropertiesWindow>();
                elementWindow.NewGameElement(this);
            }
            //Refresh and select the new data
        }

        public void RemoveSelectedGameElement()
        {
            // Put a warning dialogue here
            // validate a selected database
            selectedElementList.Remove(selectedGameData);
        }

        public void ModifySelectedGameElement()
        {
            // Put a warning dialogue here
            // validate a selected database
            ElementPropertiesWindow elementWindow = CreateInstance<ElementPropertiesWindow>();
            elementWindow.ModifyGameElement(selectedGameData, this);
        }

        public void DuplicateSelectedGameElement()
        {
            // Put a warning dialogue here
            // validate a selected database
            ElementPropertiesWindow elementWindow = CreateInstance<ElementPropertiesWindow>();
            elementWindow.DuplicateGameElement(selectedGameData, this);
        }
    }
}
