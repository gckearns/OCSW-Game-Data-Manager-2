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

        static void CreateDatabaseFile()
        {
            FileStream stream = GetSaveStream();
            try
            {
                xmlSerializer = new XmlSerializer(typeof(GameDatabase), GameUtility.GameElements.ToArray());
                xmlSerializer.Serialize(stream, database);
            }
            catch (Exception e)
            {
                Debug.Log(e.InnerException.ToString());
            }
            stream.Close();
            Debug.Log("Created new GameDatabase file");
        }

        public static void SaveDatabase()
        {
            GameDatabase gameDatabase = Database;
            FileStream stream = GetSaveStream();
            try
            {
                xmlSerializer = new XmlSerializer(typeof(GameDatabase),GameUtility.GameElements.ToArray());
                xmlSerializer.Serialize(stream, gameDatabase);
            }
            catch (Exception e)
            {
                Debug.Log(e.InnerException.ToString());
            }

            stream.Close();
            Debug.Log("Saved database: " + gameDatabase.ToString());
        }

        public static void LoadDatabase()
        {
            if (database == null)
            {
                database = new GameDatabase();
                Debug.Log("Created new database in memory");
            } 
            FileStream stream = GetLoadStream();
            try
            {
                xmlSerializer = new XmlSerializer(typeof(GameDatabase));
                database = xmlSerializer.Deserialize(stream) as GameDatabase;
            }
            catch (Exception e)
            {
                Debug.Log(e.GetBaseException().ToString());
            }
            stream.Close();
            Debug.Log("Loaded database: " + database.ToString());
        }

        static FileStream GetLoadStream()
        {
            ConfirmDatabaseFile();
            return new FileStream(path, FileMode.Open);
        }

        static FileStream GetSaveStream()
        {
            ConfirmDatabaseDirectory();
            return new FileStream(path, FileMode.Create);
        }

        static void ConfirmDatabaseDirectory()
        {
            if (!Directory.Exists(pathFolder))
            {
                Directory.CreateDirectory(pathFolder);
            }
        }

        static void ConfirmDatabaseFile()
        {
            ConfirmDatabaseDirectory();
            if (!File.Exists(path))
            {
                CreateDatabaseFile();
            }
        }

        public static void ResetDatabase()
        {
            Debug.Log("Database reset.");
            database = new GameDatabase();
            SaveDatabase();
        }
    }
}
