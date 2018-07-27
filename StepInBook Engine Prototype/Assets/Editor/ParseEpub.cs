using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor; 

public class ParseEpub : EditorWindow {

	public static Rect header;

	[MenuItem("Tools/Window/Epup Parser")]
	public static void Init()
	{
		EditorWindow window = new EditorWindow(); 
		window = GetWindow(typeof(ParseEpub), false, "Epub Parser");
		window.maxSize = new Vector2(500, 300); 
	}

	private void OnEnable()
	{
		header = new Rect(0, 0, Screen.width, 30f);
	}

	private void OnGUI()
	{
		Debug.Log("On GUI function call"); 
		DrawAreas();
	}

	void DrawAreas(){
		DrawHeader()
		 
	}

	void DrawHeader (){
		GUILayout.BeginVertical(); 
		EditorGUI.DrawRect(header, Color.blue);
		GUILayout.Label("Epub Parser!");
		GUILayout.EndVertical(); 
	}
}
