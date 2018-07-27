using UnityEditor;
using UnityEngine;

public class LoadeFile : EditorWindow
{
    private string filePath = "";

    [MenuItem("Window/Load file")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(LoadeFile));
    }

    private void OnGUI()
    {
        EditorGUILayout.BeginVertical();
        GUILayout.Label("Select file to load");
        filePath = GUILayout.TextField(filePath);
        GUI.enabled = !string.IsNullOrEmpty(filePath);

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Discard"))
        {
            Debug.Log("Discard");
            //TODO:
        }

        if (GUILayout.Button("Save"))
        {
            Debug.Log("Save");
            //TODO: Save changes
        }

        GUI.enabled = true;
        EditorGUILayout.EndHorizontal();

        GUILayout.Label("Warning message here!");
        
        EditorGUILayout.EndVertical();
    }

}
