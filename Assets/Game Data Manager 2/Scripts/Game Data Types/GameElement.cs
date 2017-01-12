﻿using UnityEngine;
using System.Collections;
using System;
using UnityEditor;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace GameDataManager
{
    [XmlInclude(typeof(Unit))]
    [XmlInclude(typeof(Commodity))]
    [System.Serializable]
    public abstract class GameElement
    {
        private GUIString guiString = new GUIString("Label");

        public string name { get; set; }
        public string id { get; set; }
        public string source { get; set; }
        public int index { get; set; }
        public GameElementType gameElementType { get; set; }
        [XmlIgnore]
        public GameElement parent { get; set; }

        public GUIString GuiString {
            get
            {
                return guiString;
            }
            set
            {
                guiString = value;
            }
        }

        public GameElement()
        {
        }

        public GameElement(string name, string id)
        {
            this.name = name;
            this.id = id;
        }

        public void OnGUI()
        {
            name = EditorGUILayout.TextField("Name", name);
            EditorGUILayout.LabelField("ID", id);
        }
    }

    public enum GameElementType
    {
        None = 0,
        Unit,
        Commodity
    }
}
