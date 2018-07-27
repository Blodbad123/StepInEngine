using System.IO;
using System.Collections.Generic; 
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace StepInEngine
{
    public class WindowMain : EditorWindow
    {
		#region variables:

        private static string _path = string.Empty;
        private static string _previousPath = string.Empty;

        public static string[] scenePaths = null;
		//private static string[] _assetNames = null;

		static List<string> _assetNames; 

        private static AssetBundle _bundle = null;
        private static Vector2 _scrollPos = Vector2.zero;

        public static string bundleName = string.Empty;
        private static bool _hasSaved = false;

        #endregion

        #region functions:

        [MenuItem("Tools/Window/Main")]
        public static void Open()
        {
            GetWindow(typeof(WindowMain), false, "Main");            
        }

        private void OnEnable()
        {
			Debug.Log("On Enable");
			_assetNames = new List<string>(); 
			//AssetDatabase.Refresh(); 
            if (string.IsNullOrEmpty(_path))
            {
                _path = Path.Combine(Application.streamingAssetsPath, "AssetsBundles/");
            }

			DirectoryInfo directory = new DirectoryInfo(_path);
			FileInfo[] files = directory.GetFiles();
			//_assetNames = new string[files.Length];

			for (int i = 0; i < files.Length; i++)
			{
				if(!files[i].Name.Contains(".manifest") && !files[i].Name.Contains(".meta") && !files[i].Name.Contains("AssetsBundles")){
					//_assetNames[index] = files[i].Name;
					_assetNames.Add(files[i].Name); 
				}
			}
        }

		private static void Refresh(){
			Caching.ClearCache();
			AssetDatabase.Refresh();

			DirectoryInfo directory = new DirectoryInfo(_path);
			FileInfo[] files = directory.GetFiles();
			//_assetNames = new string[files.Length];

			for (int i = 0; i < files.Length; i++)
			{
				if (!files[i].Name.Contains(".manifest") && !files[i].Name.Contains(".meta") && !files[i].Name.Contains("AssetsBundles") && !files[i].Name.Contains("DS_Store"))
				{
					//_assetNames[index] = files[i].Name;
					_assetNames.Add(files[i].Name);
				}
			}
		}

		private void OnDisable()
        {
            if (_bundle != null)
            {
                _bundle.Unload(true);
            }
        }

        #endregion

        #region interface:

        private void OnGUI()
        {
            //OnGUI_ChoosePath();

            GUILayout.Label("Titles:");

            _scrollPos = EditorGUILayout.BeginScrollView(_scrollPos);

            OnGUI_DisplayBundles();

            EditorGUILayout.EndScrollView();

            OnGUI_SaveAssetsBundle();
        }
		/*
        private static void OnGUI_ChoosePath()
        {
			
            if (GUILayout.Button("Select folder"))
            {
                _path = EditorUtility.OpenFolderPanel("Select folder with titles", _path, "");
            }

            if (!_path.Equals(_previousPath) || _hasSaved)
            {
                _assetNames = AssetDatabase.GetAllAssetBundleNames();
                _previousPath = _path;
                _hasSaved = false;
            }
        }
        */

        private static void OnGUI_DisplayBundles()
        {
			//Refresh();
			//_assetNames = AssetDatabase.GetAllAssetBundleNames();

			// This way of accessing all asset bundles does not update... // 
			Debug.Log(AssetDatabase.GetAllAssetBundleNames().Length); 
			if (_assetNames == null)
            {
                return;
            }

			if(GUILayout.Button("Refresh")){
				//Refresh();  
				Caching.ClearCache();
				AssetDatabase.Refresh();
			}

			foreach (string assetName in _assetNames)
			{
				if (GUILayout.Button(assetName, EditorStyles.toolbarPopup))
				{
					if (_bundle != null)
					{
						_bundle.Unload(true);
					}

					_bundle = AssetBundle.LoadFromFile(Path.Combine(_path, assetName));

					if (_bundle == null)
					{
						return;
					}

					scenePaths = _bundle.GetAllScenePaths();
					bundleName = assetName;


					if (PageSelecter.instance == null)
					{
						GetWindow(typeof(PageSelecter), false, "Page Selection");
					}

					if (PageSelecter.instance != null)
					{
						PageSelecter.instance.Focus();
					}
				}
			}
        }

        private static void OnGUI_SaveAssetsBundle()
        {
            if (GUILayout.Button("Save changes", GUILayout.Height(20)))
            {
                if (EditorUtility.DisplayDialog("Save changes", "Are you sure you want to save changes made to the titles?", "Yes", "No"))
                {
                    EditorSceneManager.SaveScene(SceneManager.GetActiveScene());

                    if (AssetImporter.GetAtPath(SceneManager.GetActiveScene().path).assetBundleName == string.Empty)
                    {
                        AssetImporter.GetAtPath(SceneManager.GetActiveScene().path).assetBundleName = bundleName;
                    }
                                       
                    BuildPipeline.BuildAssetBundles("Assets/StreamingAssets/AssetsBundles", BuildAssetBundleOptions.None, BuildTarget.StandaloneOSX);

                    AssetDatabase.Refresh();                    

                    _hasSaved = true;
                }
            }

            GUI.enabled = true;
        }

        #endregion
    }
}