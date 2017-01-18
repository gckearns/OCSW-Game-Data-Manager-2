using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Text;
using System.Collections.Generic;
using System;

namespace GameDataManager
{
    public class ElementPropertiesWindow : EditorWindow
    {
        public GameElement gameElement;
        public System.Type gameElementType;
        public string objectName = "New Object";
        public string objectId = "newobject";
        public bool isDefault = false;
        public string parentId = "default";
        int parentSelected;
        public static bool isOpen = false;
        public GameElementList elementList;
        public EditorWindow parentWindow;
        WindowMode mode;

        public void NewGameElement(GameDataManagerWindow window)
        {
            gameElementType = window.selectedElementList.ElementType;
            elementList = window.selectedElementList;
            parentWindow = window;
            gameElement = (GameElement)Activator.CreateInstance(gameElementType, null);
            gameElement.Name = objectName;
            gameElement.ID = objectId;
            mode = WindowMode.Add;
            ShowUtility();
        }

        public void ModifyGameElement(GameElement element, GameDataManagerWindow window)
        {
            gameElementType = window.selectedElementList.ElementType;
            elementList = window.selectedElementList;
            parentWindow = window;
            gameElement = element;
            mode = WindowMode.Modify;
            objectId = gameElement.ID;
            ShowUtility();
        }

        public void DuplicateGameElement(GameElement element, GameDataManagerWindow window)
        {
            gameElementType = window.selectedElementList.ElementType;
            elementList = window.selectedElementList;
            parentWindow = window;
            gameElement = (GameElement)Activator.CreateInstance(gameElementType, new object[] { element });
            objectName = gameElement.Name;
            objectId = gameElement.ID;
            mode = WindowMode.Duplicate;
            SuggestID();
            ShowUtility();
        }

        int ParentSelected
        {
            get
            {
                GameElement element = elementList[gameElement.ParentId];
                return element? elementList.IndexOf(element) + 1 : 0;
            }
            set
            {
                GameElement element = elementList[value - 1];
                string id = "";
                if (element)
                    id = element.ID;
                gameElement.ParentId = id;
            }
        }

        void OnDisable()
        {
            isOpen = false;
        }

        void OnGUI()
        {
            isOpen = true;
            EditorGUILayout.BeginVertical(GUILayout.ExpandHeight(true));
            {
                gameElement.Name = EditorGUILayout.TextField("Name", gameElement.Name);
                EditorGUILayout.BeginHorizontal();
                {
                    objectId = EditorGUILayout.TextField("ID", objectId);
                    if (GUILayout.Button("Suggest"))
                    {
                        EditorGUIUtility.keyboardControl = 0;
                        SuggestID();
                    }
                }
                EditorGUILayout.EndHorizontal();
                isDefault = EditorGUILayout.Toggle("Defines Defaults", isDefault);
                List<string> parentList = new List<string> { " " };
                parentList.AddRange(elementList.Names.ToArray());
                ParentSelected = EditorGUILayout.Popup("Parent", ParentSelected, parentList.ToArray());
            }
            EditorGUILayout.EndVertical();
            EditorGUILayout.BeginHorizontal();
            {
                if (GUILayout.Button("OK"))
                {
                    ValidateID();
                }
                if (GUILayout.Button("Cancel"))
                {
                    Close();
                }
            }
            EditorGUILayout.EndHorizontal();
            Event e = Event.current;
            //        Debug.Log ("Event: " + e.type.ToString());
            if (e.type == EventType.MouseUp)
            {
                EditorGUIUtility.keyboardControl = 0;
            }
        }

        void ValidateID()
        {
            if (IsValidID(objectId))
            {
                gameElement.ID = objectId;
                if (mode == WindowMode.Add || mode == WindowMode.Duplicate)
                {
                    elementList.Add(gameElement);
                }
                parentWindow.Focus();
                Close();
            }
            else
            {
                RemoveNotification();
                ShowNotification(new GUIContent("ID already exists!"));
            }
        }

        void SuggestID()
        {
            StringBuilder sb = new StringBuilder();
            List<char> charList = new List<char>(gameElement.Name.ToCharArray()); //convert the name field to a char array
            foreach (char c in charList)
            { //iterate through each char in the char array
                if (!char.IsWhiteSpace(c))
                { //check that the char is not whitespace
                    sb.Append(char.ToLower(c)); //convert the char to lowercase and add it to the stringbuilder
                }
            }
            string tryID = sb.ToString(); //convert the formatted string to a string
            int appendInt = 2; //start the potential appended number with 2
            bool appended = false;
            while (!IsValidID(tryID))
            { //validate string as a unique id
                if (appended)
                { //if this was looped it will have a number appended
                    sb.Remove(sb.Length - 1, 1); //remove the old appended number from the end of the string builder
                }
                tryID = sb.Append(appendInt).ToString(); //add the append number to the id string
                appendInt++;
                appended = true;
            }
            objectId = tryID;
        }

        bool IsValidID(string checkID)
        {
            if (mode == WindowMode.Modify && objectId == gameElement.ID)
            {
                return true;
            }
            else if (elementList)
            {
                return !elementList.IDs.Contains(checkID);
            }
            else
            {
                throw new NullReferenceException("Database not selected");
            }
        }
    }

    enum WindowMode
    {
        Add,
        Modify,
        Duplicate
    }
}
