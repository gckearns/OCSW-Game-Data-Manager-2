using UnityEngine;
using System.Collections;
using System;
using UnityEditor;

namespace GameDataManager
{
    public class GUIGameObject : GUIObject
    {
        private string gameObjectPath;
        private GameObject gameObject;

        public new string value
        {
            get
            {
                return gameObjectPath;
            }
            set
            {
                gameObjectPath = value;
                gameObject = AssetDatabase.LoadAssetAtPath<GameObject>(gameObjectPath);
            }
        }

        public GameObject GameObject
        {
            get
            {
                return gameObject;
            }
            set
            {
                gameObject = value;
                gameObjectPath = AssetDatabase.GetAssetPath(gameObject);
            }
        }

        public GUIGameObject() { }

        public GUIGameObject(string label)
        {
            this.label = label;
            value = null;
        }

        public GUIGameObject(string label, string value)
        {
            this.label = label;
            this.value = value;
        }

        public override GUIObject Copy()
        {
            GUIGameObject copy = new GUIGameObject();
            copy.label = this.label;
            copy.value = this.value;
            copy.gameObjectPath = this.gameObjectPath;
            copy.gameObject = this.gameObject;
            return copy;
        }

        public override string ToString()
        {
            return string.Format("[GUIGameObject]{0}:{1}", label, value);
        }
    }
}
