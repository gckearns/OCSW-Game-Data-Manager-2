using UnityEngine;
using System.Collections;
using UnityEditor;

namespace GameDataManager
{
    public class MyGUILayoutUtility
    {
        public static void SetBoxRect(GUIBox box)
        {
            GUILayout.Box(box.guiContent, box.guiStyle, box.Options);
            box.rect = GUILayoutUtility.GetLastRect();
        }

        public static void SetRowRects(GUIRow guiRow)
        {
            Rect rect = EditorGUILayout.BeginHorizontal();
            foreach (var item in guiRow.boxes)
            {
                SetBoxRect(item);
            }
            EditorGUILayout.EndHorizontal();
            guiRow.rect = rect;
        }

        public static void ShowRow(GUIRow guiRow, bool isOn)
        {
            if (Event.current.type == EventType.Repaint)
            {
                foreach (var box in guiRow.boxes)
                {
                    box.guiStyle.Draw(box.rect, box.guiContent, false, false, isOn, false);
                }
            }
        }

        public static Rect RowField(GUIBox[] boxes, bool isOn)
        {
            int length = boxes.Length;
            Rect[] rects = new Rect[length];

            Rect rect = EditorGUILayout.BeginHorizontal();
            for (int i = 0; i < length; i++)
            {
                GUIBox box = boxes[i];
                GUILayout.Box(box.guiContent, box.guiStyle, box.Options);
                rects[i] = GUILayoutUtility.GetLastRect();
                if (Event.current.type == EventType.Repaint)
                    box.guiStyle.Draw(rects[i], box.guiContent, false, false, isOn, false);
            }
            EditorGUILayout.EndHorizontal();
            return rect;
        }
    }
}
