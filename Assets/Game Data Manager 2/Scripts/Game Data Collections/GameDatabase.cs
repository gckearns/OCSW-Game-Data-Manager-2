using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace GameDataManager
{
    [System.Serializable]
    public class GameDatabase 
        //: IEnumerable
    {
        [SerializeField]
        private List<GameElementList> gameElementLists = new List<GameElementList>();

        public GameDatabase() { }

        public List<GameElementList> GameElementLists
        {
            get
            {
                return gameElementLists;
            }
        }

        public GameElementList this[Type type]
        {
            get
            {
                if (!gameElementLists.Exists(x => x.ElementType.Equals(type)))
                {
                    Add(type);
                    Debug.Log("Added GameItemList of type: " + type.ToString());
                }
                return gameElementLists.Find(x => x.ElementType.Equals(type));
            }
        }

        public void Add(Type type)
        {
            if (!gameElementLists.Exists(x => x.ElementType == type))
            {
                GameElementList newList = new GameElementList();
                newList.ElementType = type;
                this.Add(newList);
            }
            else
            {
                throw new ArgumentException(string.Format("Database already contains a GameItemList with game element Type {0}", type));
            }
        }

        #region IList<> interface implementation methods
        public GameElementList this[int index]
        {
            get
            {
                return gameElementLists[index];
            }

            set
            {
                gameElementLists[index] = value;
            }
        }

        public int Count
        {
            get
            {
                return gameElementLists.Count;
            }
        }

        public void Add(GameElementList item)
        {
            gameElementLists.Add(item);
        }

        public void Clear()
        {
            gameElementLists.Clear();
        }

        public bool Contains(GameElementList item)
        {
            return gameElementLists.Contains(item);
        }

        public void CopyTo(GameElementList[] array, int arrayIndex)
        {
            gameElementLists.CopyTo(array, arrayIndex);
        }

        public IEnumerator<GameElementList> GetEnumerator()
        {
            return gameElementLists.GetEnumerator();
        }

        public int IndexOf(GameElementList item)
        {
            return gameElementLists.IndexOf(item);
        }

        public void Insert(int index, GameElementList item)
        {
            gameElementLists.Insert(index, item);
        }

        public bool Remove(GameElementList item)
        {
            return gameElementLists.Remove(item);
        }

        public void RemoveAt(int index)
        {
            gameElementLists.RemoveAt(index);
        }

        //IEnumerator IEnumerable.GetEnumerator()
        //{
        //    return gameElementLists.GetEnumerator();
        //}
        #endregion

        public override string ToString()
        {
            return string.Format("[GameDatabase] Count: {0}", Count);
        }
    }
}
