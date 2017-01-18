using UnityEngine;
using System.Collections;

public class GUIBox {

    public GUIContent guiContent { get; set; }
    public GUIStyle guiStyle { get; set; }
    public GUILayoutOption[] Options
    {
        get
        {
            return options.options;
        }
    }
    private GUILayoutOptions options;
    public Rect rect { get; set; }

    public GUIBox(string text, GUIStyle guiStyle, params GUILayoutOption[] options)
    {
        this.guiContent = new GUIContent(text);
        this.guiStyle = guiStyle;
        this.options = new GUILayoutOptions(options);
    }

    public GUIBox(GUIContent guiContent, GUIStyle guiStyle, params GUILayoutOption[] options)
    {
        this.guiContent = guiContent;
        this.guiStyle = guiStyle;
        this.options = new GUILayoutOptions(options);
    }
}
