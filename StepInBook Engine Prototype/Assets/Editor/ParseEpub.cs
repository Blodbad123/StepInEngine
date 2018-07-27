using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO; 

public class ParseEpub : EditorWindow {
	
	static Rect header;
	static Rect main;
	static Rect parseArea;
	static string path = "";
	static string _title = "";
	static bool correctFileType; 
	public GUISkin skin;
	static int buttonWidth = 160;

	[MenuItem("Tools/Window/Epup Parser")]
	public static void Init()
	{
		ParseEpub window = (ParseEpub)GetWindow(typeof(ParseEpub), false, "Epub Parser");
		window.maxSize = new Vector2(600, 400); 
	}

	private void OnEnable()
	{
		
		//headerSkin = Resources.Load<GUISkin>()
		path = "";
		_title = "";
		correctFileType = false;
		skin = Resources.Load<GUISkin>("GuiStyles/guiStyles_1");
		Debug.Log(skin.button.imagePosition); 
	}

	private void OnGUI()
	{
		DrawLayouts();
		DrawHeader();
		DrawMainSection();
		if (correctFileType)
		{
			DrawParseButton();
		}
	}

	void DrawLayouts(){
		header.x = 0;
		header.y = 0;
		header.width = Screen.width;
		header.height = 40;

		main.x = (Screen.width/2)-(buttonWidth/2);
		main.y = 50;
		main.width = Screen.width;
		main.height = 100;

		parseArea.x = Screen.width / 2 - buttonWidth / 2;;
		parseArea.y = 160;
		parseArea.width = Screen.width;
		parseArea.height = 140; 
	}


	void DrawHeader (){
		GUILayout.BeginArea(header); 
		GUILayout.Label("Epub Parser!", skin.GetStyle("header_1"));
		GUILayout.EndArea(); 
	}

	void DrawMainSection(){

		string[] filters = {"Eboks", "epub"}; 
		GUILayout.BeginArea(main); 
		if (GUILayout.Button("Choose file to parse", GUILayout.Width(buttonWidth), GUILayout.Height(40)))
		{
			path = EditorUtility.OpenFilePanelWithFilters("Choose a file of format '.epub'" ,Application.dataPath, filters);
			if (path.Length > 1)
			{
				correctFileType = true;
			}
			int index = path.LastIndexOf("/", System.StringComparison.CurrentCulture);
			_title = path.Substring(index+1); 
		}

		GUILayout.EndArea(); 
	}

	void DrawParseButton(){
		GUILayout.BeginArea(parseArea);
		GUILayout.Label("Chosen File:", skin.label); 
		GUILayout.TextField(_title, skin.textField);

		if (GUILayout.Button("PARSE", GUILayout.Width(buttonWidth), GUILayout.Height(60)))
		{
			Runtime.Parse(path); 
		}
	}
}
