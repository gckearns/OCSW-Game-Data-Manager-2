using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEngine.Events;

namespace GameDataManager
{
    public class MyGUILayout
    {
        public static Texture2D GetTexture(Color color)
        {
            Texture2D tex = new Texture2D(4, 4);
            for (int y = 0; y < tex.height; y++)
            {
                for (int x = 0; x < tex.width; x++)
                {
                    tex.SetPixel(x, y, color);
                }
            }
            tex.Apply();
            return tex;
        }

        static GUIStyle GetStyle(int index, int length)
        {
            GUIStyle leftButton = new GUIStyle("TL tab left");
            GUIStyle middleButton = new GUIStyle("TL tab mid");
            GUIStyle rightButton = new GUIStyle("TL tab right");
            if (length == 1)
            {
                return middleButton;
            }
            if (index == 0)
            {
                return leftButton;
            }
            if (index == (length - 1))
            {
                return rightButton;
            }
            return middleButton;
        }

        public static int ToggleBar(int selected, string[] strings, params GUILayoutOption[] options)
        {
            int count = strings.Length;
            bool[] values = new bool[count];
            if (selected >= 0)
            {
                values[selected] = true;
            }
            else
            {
                selected = -1;
            }
            bool changed;
            EditorGUILayout.BeginHorizontal(options);
            for (int i = 0; i < count; i++)
            {
                EditorGUI.BeginChangeCheck();
                GUILayout.Toggle(values[i], strings[i], GetStyle(i, count));
                changed = EditorGUI.EndChangeCheck();
                if (changed)
                {
                    selected = i;
                }
            }
            EditorGUILayout.EndHorizontal();
            return selected;
        }

        public static int ToggleBar(int selected, string[] strings, GUIStyle style, params GUILayoutOption[] options)
        {
            int count = strings.Length;
            bool[] values = new bool[count];
            if (selected >= 0)
            {
                values[selected] = true;
            }
            else
            {
                selected = -1;
            }
            bool changed;
            EditorGUILayout.BeginHorizontal(style, options);
            for (int i = 0; i < count; i++)
            {
                EditorGUI.BeginChangeCheck();
                GUILayout.Toggle(values[i], strings[i], GetStyle(i, count));
                changed = EditorGUI.EndChangeCheck();
                if (changed)
                {
                    selected = i;
                }
            }
            EditorGUILayout.EndHorizontal();
            return selected;
        }

        public static int MenuBar(int selected, string[] strings, params GUILayoutOption[] options)
        {
            GUIStyle myButton = new GUIStyle(GUI.skin.button);
            myButton.margin = new RectOffset(0, 0, 0, 0);
            myButton.normal.background = null;
            int count = strings.Length;
            bool[] values = new bool[count];
            if (selected >= 0)
            {
                values[selected] = true;
            }
            else
            {
                selected = -1;
            }
            bool changed;
            EditorGUILayout.BeginHorizontal(options);
            for (int i = 0; i < count; i++)
            {
                EditorGUI.BeginChangeCheck();
                GUILayout.Toggle(values[i], strings[i], myButton);
                changed = EditorGUI.EndChangeCheck();
                if (changed)
                {
                    if (i == selected)
                    {
                        selected = -1;
                    }
                    else
                    {
                        selected = i;
                    }
                }
            }
            EditorGUILayout.EndHorizontal();
            return selected;
        }

        public static int MenuBar(int selected, string[] strings, GUIStyle style, params GUILayoutOption[] options)
        {
            GUIStyle myButton = new GUIStyle(GUI.skin.button);
            myButton.margin = new RectOffset(0, 0, 0, 0);
            myButton.normal.background = null;
            int count = strings.Length;
            bool[] values = new bool[count];
            if (selected >= 0)
            {
                values[selected] = true;
            }
            else
            {
                selected = -1;
            }
            bool changed;
            EditorGUILayout.BeginHorizontal(style, options);
            for (int i = 0; i < count; i++)
            {
                EditorGUI.BeginChangeCheck();
                GUILayout.Toggle(values[i], strings[i], myButton);
                changed = EditorGUI.EndChangeCheck();
                if (changed)
                {
                    if (i == selected)
                    {
                        selected = -1;
                    }
                    else
                    {
                        selected = i;
                    }
                }
            }
            EditorGUILayout.EndHorizontal();
            return selected;
        }

        public static int IntFieldMin(string label, int value, int leftValue, params GUILayoutOption[] options)
        {
            value = value < leftValue ? leftValue : value;
            value = EditorGUILayout.IntField(label, value, options);
            return value;
        }

        public static int RowSelection(int selected, GUIRow[] rows, GameDataManagerWindow window, params GUILayoutOption[] options)
        {
            int length = rows.Length;
            Event e = Event.current;
            bool changed = false;
            for (int i = 0; i < length; i++)
            {
                MyGUILayoutUtility.SetRowRects(rows[i]);
                bool isSelected = selected == i ? true : false;

                if (e.isMouse && e.type == EventType.MouseUp && rows[i].rect.Contains(e.mousePosition))
                {
                    isSelected = (e.button == 1) ? true : !isSelected;
                    selected = isSelected ? i : -1;
                    changed = true;
                    GUI.changed = changed;
                    e.Use();
                }
            }
            for (int i = 0; i < length; i++)
            {
                bool isSelected = selected == i ? true : false;
                MyGUILayoutUtility.ShowRow(rows[i],isSelected);
            }
            //window.Repaint();
            if (changed && e.button == 1)
            {
                window.showPopup = true;
                //PopupWindow.Show(new Rect(Event.current.mousePosition, Vector2.zero), new ContextPopup());
            }
            return selected;
        }

        public static Rect DataTableHeader(string[] labels, UnityAction[] actions, float[] minWidths, float height)
        {
            GUIStyle buttonStyle = MyGUIStyle.MyButton;
            Rect rect = EditorGUILayout.BeginHorizontal(GUILayout.Height(height));
            {
                for (int i = 0; i < labels.Length; i++)
                {
                    if (GUILayout.Button(labels[i], buttonStyle, GUILayout.MinWidth(minWidths[i]), GUILayout.Height(height)))
                    {
                        UnityAction action = actions[i];
                        if (action != null)
                        {
                            action();
                        }
                    }
                }
            }
            EditorGUILayout.EndHorizontal();
            return rect;
        }

        //		mySkin.box = defaultSkin.box;
        //		mySkin.button = defaultSkin.button;
        //		mySkin.customStyles = defaultSkin.customStyles;
        //		mySkin.font= defaultSkin.font;
        //		mySkin.horizontalScrollbar = defaultSkin.horizontalScrollbar;
        //		mySkin.horizontalScrollbarLeftButton = defaultSkin.horizontalScrollbarLeftButton;
        //		mySkin.horizontalScrollbarRightButton = defaultSkin.horizontalScrollbarRightButton;
        //		mySkin.horizontalScrollbarThumb = defaultSkin.horizontalScrollbarThumb;
        //		mySkin.horizontalSlider = defaultSkin.horizontalSlider;
        //		mySkin.horizontalSliderThumb = defaultSkin.horizontalSliderThumb;
        //		mySkin.label = defaultSkin.label;
        //		mySkin.scrollView = defaultSkin.scrollView;
        //		mySkin.settings.cursorColor = defaultSkin.settings.cursorColor;
        //		mySkin.settings.cursorFlashSpeed = defaultSkin.settings.cursorFlashSpeed;
        //		mySkin.settings.doubleClickSelectsWord = defaultSkin.settings.doubleClickSelectsWord;
        //		mySkin.settings.selectionColor = defaultSkin.settings.selectionColor;
        //		mySkin.settings.tripleClickSelectsLine = defaultSkin.settings.tripleClickSelectsLine;
        //		mySkin.textArea = defaultSkin.textArea;
        //		mySkin.textField = defaultSkin.textField;
        //		mySkin.toggle = defaultSkin.toggle;
        //		mySkin.verticalScrollbar = defaultSkin.verticalScrollbar;
        //		mySkin.verticalScrollbarDownButton = defaultSkin.verticalScrollbarDownButton;
        //		mySkin.verticalScrollbarUpButton = defaultSkin.verticalScrollbarUpButton;
        //		mySkin.verticalScrollbarThumb = defaultSkin.verticalScrollbarThumb;
        //		mySkin.verticalSlider = defaultSkin.verticalSlider;
        //		mySkin.verticalSliderThumb = defaultSkin.verticalSliderThumb;
        //		mySkin.window = defaultSkin.window;
    }
}
