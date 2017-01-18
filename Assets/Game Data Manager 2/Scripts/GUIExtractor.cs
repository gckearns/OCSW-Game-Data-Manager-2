using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using System.IO;

namespace GameDataManager
{
    public class GUIExtractor
    {

        static string path = "Assets/Unity Skin/";
        public static bool isDone = false;

        public static void Extract()
        {
            GUI.skin = null;
            GUISkin mySkin = GUI.skin;
            List<GUIStyle> styles = new List<GUIStyle>
            {
                mySkin.box,
                mySkin.button,
                mySkin.horizontalScrollbar,
                mySkin.horizontalScrollbarLeftButton,
                mySkin.horizontalScrollbarRightButton,
                mySkin.horizontalScrollbarThumb,
                mySkin.horizontalSlider,
                mySkin.horizontalSliderThumb,
                mySkin.label,
                mySkin.scrollView,
                mySkin.textArea,
                mySkin.textField,
                mySkin.toggle,
                mySkin.verticalScrollbar,
                mySkin.verticalScrollbarDownButton,
                mySkin.verticalScrollbarUpButton,
                mySkin.verticalScrollbarThumb,
                mySkin.verticalSlider,
                mySkin.verticalSliderThumb,
                mySkin.window
            };
            styles.AddRange(mySkin.customStyles);

            foreach (var item in styles)
            {
                SaveStyle(item);
            }
            isDone = true;
        }

        public static void SaveStyle(GUIStyle style)
        {
            GUIStyleState[] styleStates = new GUIStyleState[]
            {
                style.active,
                style.focused,
                style.hover,
                style.normal,
                style.onActive,
                style.onFocused,
                style.onHover,
                style.onNormal
            };
            string[] stateNames = new string[]
            {
                "active",
                "focused",
                "hover",
                "normal",
                "onActive",
                "onFocused",
                "onHover",
                "onNormal"
            };

            for (int i = 0; i < styleStates.Length; i++)
            {
                string state = stateNames[i];
                string folderPath = GUIExtractor.path + style.name + "/" + state;
                Texture2D background = styleStates[i].background;
                if (background != null)
                {
                    SaveTexture(background, folderPath, "/background_" + background.name + ".asset");
                }
                Texture2D[] backgrounds = styleStates[i].scaledBackgrounds;
                if (backgrounds != null)
                {
                    for (int j = 0; j < backgrounds.Length; j++)
                    {
                        Texture2D texture = backgrounds[j];
                        SaveTexture(texture, folderPath, "/background_" + j + "_" + texture.name + ".asset");
                    }
                }
            }

        }

        static void SaveTexture(Texture2D asset, string pathfolder, string filename)
        {
            if (!Directory.Exists(pathfolder))
            {
                Directory.CreateDirectory(pathfolder);
            }
            Texture2D texture = Texture2D.Instantiate<Texture2D>(asset);
            string fullPath = pathfolder + filename;
            AssetDatabase.CreateAsset(texture, fullPath);
            //Debug.Log(fullPath);
        }
    }
}
