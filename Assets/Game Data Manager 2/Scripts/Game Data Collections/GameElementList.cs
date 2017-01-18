using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace GameDataManager
{
    [System.Serializable]
    public class GameElementList 
    {
        private string elementTypeAssemblyName;
        private Type elementType;
        private List<GameElement> gameElements = new List<GameElement>();

        public GameElementList() { }

        public GameElementList(Type type)
        {
            ElementType = type;
        }

        public GameElementType gameElementType { get; set; }

        public List<GameElement> gameItems
        {
            get
            {
                return gameElements;
            }
        }

        [XmlIgnore]
        public List<string> Names
        {
            get
            {
                List<string> list = new List<string>();
                for (int i = 0; i < this.Count; i++)
                {
                    list.Add(this[i].Name);
                }
                return list;
            }
        }

        [XmlIgnore]
        public List<string> IDs
        {
            get
            {
                List<string> list = new List<string>();
                for (int i = 0; i < this.Count; i++)
                {
                    list.Add(this[i].ID);
                }
                return list;
            }
        }

        [XmlIgnore]
        public List<string> ElementTypes
        {
            get
            {
                List<string> list = new List<string>();
                for (int i = 0; i < this.Count; i++)
                {
                    list.Add(this[i].gameElementType.ToString());
                }
                return list;
            }
        }

        public string ElementTypeAssemblyName
        {
            get
            {
                return elementTypeAssemblyName;
            }

            set
            {
                elementTypeAssemblyName = value;
                elementType = Type.GetType(value, true);
            }
        }

        [XmlIgnore]
        public Type ElementType
        {
            get
            {
                if (elementType == null)
                {
                    elementType = Type.GetType(elementTypeAssemblyName);
                }
                return elementType;
            }

            set
            {
                elementType = value;
                elementTypeAssemblyName = value.AssemblyQualifiedName;
            }
        }

        /// <summary>
        /// Searches for a <see cref="GameElement"/> with the specified ID and returns the first occurance within the entire <see cref="GameElementList"/>
        /// </summary>
        public GameElement this[string id]
        {
            get
            {
                return IDs.Contains(id)? gameElements.Find(x=>x.ID.Equals(id)) : null;
            }

            set
            {
                gameElements[IndexOf(this[id])] = value;
            }
        }

        public void Add(string name, string id, bool isDefault, string parentId)
        {
            Add((GameElement)Activator.CreateInstance(ElementType, new object[] { name, id, isDefault, parentId }));
        }

        /// <summary>
        /// Searches for a <see cref="GameElement"/>with the specified ID and removes the first occurance from within the entire <see cref="GameElementList"/>
        /// </summary>
        /// 
        public void Remove(string id)
        {
            Remove(this[id]);
        }

        public GameElement[] ToArray()
        {
            return gameElements.ToArray();
        }

        #region IList<> interface implementation methods
        public GameElement this[int index]
        {
            get
            {
                return (index >= 0 && index <= gameElements.Count - 1)? gameElements[index] : null;
            }

            set
            {
                gameElements[index] = value;
            }
        }

        public int Count
        {
            get
            {
                return gameElements.Count;
            }
        }

        public void Add(GameElement item)
        {
            gameElements.Add(item);
        }

        public void Clear()
        {
            gameElements.Clear();
        }

        public bool Contains(GameElement item)
        {
            return gameElements.Contains(item);
        }

        public void CopyTo(GameElement[] array, int arrayIndex)
        {
            gameElements.CopyTo(array, arrayIndex);
        }

        public IEnumerator<GameElement> GetEnumerator()
        {
            return gameElements.GetEnumerator();
        }

        public int IndexOf(GameElement item)
        {
            return gameElements.IndexOf(item);
        }

        public void Insert(int index, GameElement item)
        {
            gameElements.Insert(index, item);
        }

        public bool Remove(GameElement item)
        {
            return gameElements.Remove(item);
        }

        public void RemoveAt(int index)
        {
            gameElements.RemoveAt(index);
        }

        //IEnumerator IEnumerable.GetEnumerator()
        //{
        //    return gameElements.GetEnumerator();
        //}
        #endregion

        public override string ToString()
        {
            return string.Format("[GameElementList]:{0}:{1}", ElementType.Name, Count);
        }

        public static implicit operator bool(GameElementList item)
        {
            return (item != null);
        }
    }
}
