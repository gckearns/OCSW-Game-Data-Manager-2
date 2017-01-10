using UnityEngine;
using System.Collections;
using UnityEditor;

namespace GDM1
{
    [CustomEditor(typeof(Building))]
    public class BuildingInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            //Debug.Log("Running default inspector...");
            base.OnInspectorGUI();
            //Debug.Log("Ran default inspector.");
        }
    }
}
