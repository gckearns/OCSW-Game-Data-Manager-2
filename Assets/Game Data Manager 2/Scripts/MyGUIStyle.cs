using UnityEngine;
using System.Collections;

namespace GameDataManager
{
    public class MyGUIStyle
    {
        private static GUIStyle myButton;
        private static GUIStyle leftVerticalStyle;
        private static GUIStyle rightVerticalStyle;
        private static GUIStyle scrollStyle;

        public static GUIStyle MyButton
        {
            get
            {
                if (myButton == null)
                {
                    myButton = new GUIStyle(GUI.skin.button);
                    myButton.normal.background = null;
                }
                return myButton;
            }
        }
        public static GUIStyle LeftVerticalStyle
        {
            get
            {
                if (leftVerticalStyle == null)
                {
                    leftVerticalStyle = new GUIStyle(GUI.skin.scrollView);
                    leftVerticalStyle.margin = new RectOffset(0, 4, 0, 2);
                }
                return leftVerticalStyle;
            }
        }
        public static GUIStyle RightVerticalStyle
        {
            get
            {
                if (rightVerticalStyle == null)
                {
                    rightVerticalStyle = new GUIStyle(GUI.skin.scrollView);
                    rightVerticalStyle.margin = new RectOffset(4, 0, 0, 2);
                }
                return rightVerticalStyle;
            }
        }

        public static GUIStyle ScrollStyle
        {
            get
            {
                if (scrollStyle == null)
                {
                    scrollStyle = new GUIStyle(GUI.skin.scrollView);
                    scrollStyle.normal.background = Resources.Load<Texture2D>("ColdSteelTexture");
                }
                return scrollStyle;
            }
        }
    }
}
