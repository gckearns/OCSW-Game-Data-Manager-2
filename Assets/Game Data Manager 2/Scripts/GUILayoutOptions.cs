using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GUILayoutOptions {
    public GUILayoutOption[] options { get; set; }

    public GUILayoutOptions(params GUILayoutOption[] options)
    {
        this.options = options;
    }
}
