using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TestObject {

    float facing = 90;
    string footprint = "footprintId";

    TestObject parent;

    Dictionary<string, string> fields { get; set; }

    Dictionary<string, string> fieldOverrides
    {
        get;
        set;
    }

    public static void Test()
    {
        TestObject unitA = new TestObject();
        unitA.fields = new Dictionary<string, string>();
        unitA.fields.Add("facing", unitA.facing.ToString());
        unitA.fields.Add("footprint", unitA.footprint);
        unitA.fieldOverrides = new Dictionary<string, string>();
        unitA.fieldOverrides.Add("footprint", "newid");
        TestObject unitB = new TestObject();
        unitB.parent = unitA;
        unitB.facing = 222;
        unitB.fieldOverrides = new Dictionary<string, string>();
        unitB.fieldOverrides.Add("facing", unitB.facing.ToString());
        foreach (var item in unitA.fields)
        {
            Debug.Log("UnitA " + item.Key + ": " + item.Value);
        }
        foreach (var item in unitA.fieldOverrides)
        {
            unitA.fields[item.Key] = new string(item.Value.ToCharArray());
        }
        unitB.fields = new Dictionary<string, string> (unitB.parent.fields);
        foreach (var item in unitB.fieldOverrides)
        {
            unitB.fields[item.Key] = new string(item.Value.ToCharArray());
        }
        foreach (var item in unitA.fields)
        {
            Debug.Log("UnitA " + item.Key + ": " + item.Value);
        }
        foreach (var item in unitB.fields)
        {
            Debug.Log("UnitB " + item.Key + ": " + item.Value);
        }
        string[] strings = new List<string>(unitA.fields.Values).ToArray();
    }
}
