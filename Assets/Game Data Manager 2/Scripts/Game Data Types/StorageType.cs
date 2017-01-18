using UnityEngine;
using System.Collections;
using System;

namespace GameDataManager
{
    [System.Serializable]
    public class StorageType : GameElement
    {
        public StorageType() { }

        public StorageType(string name, string id, bool isDefault, string parentId)
        {
            this.Name = name;
            this.ID = id;
            this.IsDefault = isDefault;
            this.ParentId = parentId;
        }

        public StorageType(StorageType gameElement)
        {
            GUIData = gameElement.GUIData;
        }
    }
}
