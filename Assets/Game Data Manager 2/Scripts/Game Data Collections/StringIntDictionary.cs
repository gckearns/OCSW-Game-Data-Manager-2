using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Xml.Serialization;

namespace GameDataManager
{
    public class StringIntDictionary
    {
        private List<string> keys = new List<string>();
        private List<int> values = new List<int>();
        private Type keyType;
        private string keyTypeAssemblyName;

        public StringIntDictionary() { }

        public string KeyTypeAssemblyName
        {
            get
            {
                return keyType.AssemblyQualifiedName;
            }
            set
            {
                keyTypeAssemblyName = value;
                keyType = Type.GetType(keyTypeAssemblyName);
            }
        }

        [XmlIgnore]
        public Type KeyType
        {
            get
            {
                return keyType;
            }

            set
            {
                keyType = value;
                keyTypeAssemblyName = keyType.AssemblyQualifiedName;
            }
        }

        public string[] Keys
        {
            get
            {
                return keys.ToArray();
            }
            private set
            {
                keys = new List<string>(value);
            }
        }
                
        public int[] Values
        {
            get
            {
                return values.ToArray();
            }
            private set
            {
                values = new List<int>(value);
            }
        }
        
         #region "IDictionary interface members"
            public int this[string key]
            {
                get
                {
                    if (key == null)
                    {
                        throw new ArgumentNullException("key is null");
                    }
                    if (ContainsKey(key))
                    {
                        return values[keys.IndexOf(key)];
                    }
                    else
                    {
                        try
                        {
                            throw new KeyNotFoundException("Key does not exist in the collection.");
                        }
                        catch (KeyNotFoundException)
                        {
                            int value = new int();
                            Add(key,value);
                            return value;
                        }
                    }
                }
                set
                {
                    if (key == null)
                    {
                        throw new ArgumentNullException("key is null");
                    }
                    if (ContainsKey(key))
                    {
                        values[keys.IndexOf(key)] = value;
                    }
                    else
                    {
                        try
                        {
                            throw new KeyNotFoundException("Key does not exist in the collection.");
                        }
                        catch (KeyNotFoundException)
                        {
                            Add(key,value);
                        }
                    }
                }
            }
            
            public void Add(string key, int value)
            {
                if (key == null)
                {
                    throw new ArgumentNullException("key is null");
                }
                if (ContainsKey(key))
                {
                    throw new ArgumentException("An element with the same key already exists.");
                }
                keys.Add(key);
                values.Add(value);
            }
            
            public void Clear()
            {
                keys.Clear();
                values.Clear();
            }
            
            public bool ContainsKey(string key)
            {
                return keys.Contains(key);
            }
            
            public int Count
            {
                get
                {
                    return keys.Count;
                }
            }
            
            public bool Remove(string key)
            {
                if (key == null)
                {
                    throw new ArgumentNullException("key is null");
                }
                if (ContainsKey(key))
                {
                    int i = keys.IndexOf(key);
                    keys.RemoveAt(i);
                    values.RemoveAt(i);
                    return true;
                }
                else
                {
                    return false;
                }
            }

        public void RemoveAt(int index)
        {
            if (index < 0)
            {
                throw new ArgumentOutOfRangeException("index cannot be negative");
            }

            if (index >= Count)
            {
                throw new ArgumentOutOfRangeException("index out of range");
            }
                keys.RemoveAt(index);
                values.RemoveAt(index);
        }

        public bool TryGetValue(string key, out int value)
            {
                if (key == null)
                {
                    throw new ArgumentNullException("key is null");
                }
                if (ContainsKey(key))
                {
                    value = values[keys.IndexOf(key)];
                    return true;
                }
                else
                {
                    value = new int();
                    return false;
                }  
            }
        #endregion
    }
}
