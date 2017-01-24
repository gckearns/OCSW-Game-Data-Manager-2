using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

namespace GameDataManager
{
    public class TestObject
    {

        float facing = 90;
        string footprint = "footprintId";

        TestObject parent;
        List<ElementField> fieldOverrides = new List<ElementField>();

        //Dictionary<string, object> fields
        //{
        //    get
        //    {
        //        Dictionary<string, object> dict = new Dictionary<string, object>();
        //        dict.Add("facing", facing.ToString());
        //        dict.Add("footprint", footprint);
        //        return dict;
        //    }
        //}

        //Dictionary<string, object> Fields
        //{
        //    get
        //    {
        //        Dictionary<string, object> dict = new Dictionary<string, object>((parent != null) ? parent.Fields : fields);
        //        foreach (var item in FieldOverrides)
        //        {
        //            dict[item.Key] = item.Value;
        //        }
        //        return dict;
        //    }
        //}

        List<ElementField> fields
        {
            get
            {
                List<ElementField> list = new List<ElementField>();
                list.Add(new ElementField("facing", facing));
                list.Add(new ElementField("footprint", footprint));
                return list;
            }
        }

        List<ElementField> Fields
        {
            get
            {
                List<ElementField> list = new List<ElementField>((parent != null) ? parent.Fields : fields);
                foreach (var item in FieldOverrides)
                {
                    if (list.Exists(x => x.index == item.index))
                        list.Find(x => x.index == item.index).value = item.value;
                }
                return list;
            }
        }

        float Facing
        {
            get
            {
                return (float) Get("facing");
            }
            set
            {
                Set("facing", value);
            }
        }

        string Footprint
        {
            get
            {
                return Get("footprint") as string;
            }
            set
            {
                Set("footprint", value);
            }
        }

        List<ElementField> FieldOverrides
        {
            get
            {
                return fieldOverrides;
            }
            set
            {
                fieldOverrides = value;
            }
        }

        object Get(string index)
        {
            if (FieldOverrides.Exists(x => x.index == index))
                return (string)FieldOverrides.Find(x => x.index == index).value;
            else if (parent.FieldOverrides.Exists(x => x.index == index))
                return (string)parent.FieldOverrides.Find(x => x.index == index).value;
            else
                return (string)Fields.Find(x => x.index == index).value;
        }

        void Set(string index, object value)
        {
            if (FieldOverrides.Exists(x => x.index == index))
                FieldOverrides.Find(x => x.index == index).value = value;
            else
                FieldOverrides.Add(new ElementField(index, value));
        }

        [MenuItem("Manager/Test")]
        public static void Test()
        {
            TestObject unitA = new TestObject();
            foreach (var item in unitA.Fields)
            {
                Debug.Log("UnitA " + item.index + ": " + item.value); // 90 - footprintId
            }
            unitA.Facing = 999f;
            foreach (var item in unitA.Fields)
            {
                Debug.Log("UnitA " + item.index + ": " + item.value); // 999 - footprintId
            }
            TestObject unitB = new TestObject();
            foreach (var item in unitB.Fields)
            {
                Debug.Log("UnitB " + item.index + ": " + item.value); // 90 - footprintId
            }
            unitB.parent = unitA;
            foreach (var item in unitB.Fields)
            {
                Debug.Log("UnitB " + item.index + ": " + item.value); // 999 - footprintId
            }
            unitA.Facing = 222;
            unitB.Footprint = "newFootprint";
            foreach (var item in unitA.Fields)
            {
                Debug.Log("UnitA " + item.index + ": " + item.value); // 222 - footprintId
            }
            foreach (var item in unitB.Fields)
            {
                Debug.Log("UnitB " + item.index + ": " + item.value); // 999 - newFootprint
            }

            //MyStruct nestedA = new MyStruct();
            //nestedA.value = "Aaa";
            //MyStruct nestedB = new MyStruct();
            //nestedB.value = "Bbb";
            //MyStruct nestedC = new MyStruct();
            //nestedC.value = "Ccc";
            //MyStruct nestedD = new MyStruct();
            //nestedD.value = "Ddd";
            //MyStruct[] structs = new MyStruct[] { nestedA, nestedB };

            //List<MyStruct> list = new List<MyStruct>(structs);
            //List<MyStruct> anotherList = new List<MyStruct>(list);

            //Debug.Log("List = " + list[0]);
            //Debug.Log("Another = " + anotherList[0]);
            //list[0] = nestedC;
            //Debug.Log("List = " + list[0]);
            //Debug.Log("Another = " + anotherList[0]);
            //nestedC.value = "Ddd";
            //Debug.Log("List = " + list[0]);
            //Debug.Log("Another = " + anotherList[0]);
        }
    }
    public struct MyStruct
    {
        public object value;

        public object Value
        {
            get
            {
                //Debug.Log(value.GetType().Name + ": " + value);
                return value;
            }
            set
            {
                this.value = value;
            }

        }

        public override string ToString()
        {
            return string.Format(value.ToString());
        }
    }
    
    public class ElementField
    {
        public string index = "";
        public object value;

        public ElementField(string index, object value)
        {
            this.index = index;
            this.value = value;
        }
    }

    public enum MyEnum
    {
        Aenum,
        Benum,
        Cenum
    }
}

