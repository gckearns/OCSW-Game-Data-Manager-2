using UnityEngine;
using System.Collections;
using System.Xml.Serialization;
using System.Collections.Generic;
using UnityEditor;
using System.IO;
using System;
using System.Runtime.Serialization;
using System.Xml;

namespace GameDataManager
{
    public class GameDatabaseManager
    {
        private static string path = "Assets/Game Data Manager 2/Resources/Database.xml";
        private static string pathFolder = "Assets/Game Data Manager 2/Resources";
        private static string parentfolder = "Game Data Manager 2/";
        private static string resourcesFolder = "Resources";
        private static GameDatabase database;
        private static XmlSerializer xmlSerializer;

        public static GameDatabase Database
        {
            get
            {
                if (database == null)
                {
                    LoadDatabase();
                }
                return database;
            }
        }

        static void CreateDatabase()
        {
            database = new GameDatabase();
            Debug.Log("Created new GameDatabase");
            SaveDatabase();
        }

        static void CreateDirectory()
        {
            AssetDatabase.CreateFolder(parentfolder, resourcesFolder);
            Debug.Log(string.Format("Created {0} folder in {1}", resourcesFolder, parentfolder));
            //        CreateDatabase();
        }

        public static void SaveDatabase()
        {
            if (database != null)
            {
                FileStream stream = new FileStream(path, FileMode.Create);
                
                Debug.Log("Beginning Serialization");
                try
                {
                    xmlSerializer = new XmlSerializer(typeof(GameDatabase),GameUtility.GameElements.ToArray());
                    xmlSerializer.Serialize(stream, database);
                }
                catch (Exception e)
                {
                    Debug.Log(e.InnerException.ToString());
                }

                stream.Close();
                Debug.Log("Saved database");
            }
            else
            {
                CreateDatabase();
            }

        }

        public static void LoadDatabase()
        {
            Directory.CreateDirectory(pathFolder);
            if (!File.Exists(path))
            {
                CreateDatabase();
            }
            FileStream stream = new FileStream(path, FileMode.Open);
            try
            {
                xmlSerializer = new XmlSerializer(typeof(GameDatabase));
                database = xmlSerializer.Deserialize(stream) as GameDatabase;
            }
            catch (Exception e)
            {
                //Debug.Log(e.InnerException.ToString());
                Debug.Log(e.GetBaseException().ToString());
            }
            Debug.Log("Loaded database from disk: " + database.ToString());
            stream.Close();
        }

        public static void ResetDatabase()
        {
            Debug.Log("Database reset.");
            database = null;
        }
    }
}
