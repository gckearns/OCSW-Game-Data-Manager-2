using UnityEngine;
using System.Collections;

namespace GameDataManager
{
    public class GUIRow
    {
        public GUIBox[] boxes { get; set; }
        public Rect rect { get; set; }

        public GUIRow(GameElement gameElement)
        {
            GUIStyle btnStyle = new GUIStyle("SelectionButton");
            boxes = new GUIBox[] 
            {
                new GUIBox(gameElement.Name, btnStyle, GUILayout.MinWidth(96)),
                new GUIBox(gameElement.ID, btnStyle, GUILayout.MinWidth(64)),
                new GUIBox(gameElement.gameElementType.ToString(), btnStyle, GUILayout.MinWidth(64)) };
        }

        public GUIRow(GameElement gameElement, GUILayoutOptions optionsOne, GUILayoutOptions optionsTwo, GUILayoutOptions optionsThree)
        {
            GUIStyle btnStyle = new GUIStyle("SelectionButton");
            boxes = new GUIBox[]
            {
                new GUIBox(gameElement.Name, btnStyle, optionsOne.options),
                new GUIBox(gameElement.ID, btnStyle, optionsTwo.options),
                new GUIBox(gameElement.gameElementType.ToString(), btnStyle, optionsThree.options) };
        }

        public GUIRow(GameElement gameElement, GUIStyle boxStyle)
        {
            boxes = new GUIBox[]
            {
                new GUIBox(gameElement.Name, boxStyle, GUILayout.MinWidth(96)),
                new GUIBox(gameElement.ID, boxStyle, GUILayout.MinWidth(64)),
                new GUIBox(gameElement.gameElementType.ToString(), boxStyle, GUILayout.MinWidth(64)) };
        }

        public GUIRow(GameElement gameElement, GUIStyle boxStyle, GUILayoutOptions optionsOne, GUILayoutOptions optionsTwo, GUILayoutOptions optionsThree)
        {
            boxes = new GUIBox[]
            {
                new GUIBox(gameElement.Name, boxStyle, optionsOne.options),
                new GUIBox(gameElement.ID, boxStyle, optionsTwo.options),
                new GUIBox(gameElement.gameElementType.ToString(), boxStyle, optionsThree.options) };
        }
    }
}
