using UnityEngine;
using UnityEditor;

namespace GameDataManager
{
    public class ContextPopup : PopupWindowContent
    {
        GameDataManagerWindow managerWindow;

        GameDataManagerWindow ManagerWindow
        {
            get
            {
                if (!managerWindow)
                {
                    managerWindow = EditorWindow.GetWindow<GameDataManagerWindow>();
                    //editorWindow.Focus();
                }
                return managerWindow;
            }
        }

        public override Vector2 GetWindowSize()
        {
            return new Vector2(200, 110);
        }

        public override void OnGUI(Rect rect)
        {
            if (GUILayout.Button("Add", MyGUIStyle.MyButton))
            {
                ManagerWindow.NewGameElement();
            }
            if (GUILayout.Button("Remove", MyGUIStyle.MyButton))
            {
                ManagerWindow.RemoveSelectedGameElement();
            }
            if (GUILayout.Button("Duplicate", MyGUIStyle.MyButton))
            {
                ManagerWindow.DuplicateSelectedGameElement();
            }
            if (GUILayout.Button("Modify", MyGUIStyle.MyButton))
            {
                ManagerWindow.ModifySelectedGameElement();
            }
            GUILayout.Button("Reset", MyGUIStyle.MyButton);
        }

        public override void OnOpen()
        {
            Debug.Log("Popup opened: " + this);
            editorWindow.Repaint();
        }

        public override void OnClose()
        {
            Debug.Log("Popup closed: " + this);

        }
    }
}
