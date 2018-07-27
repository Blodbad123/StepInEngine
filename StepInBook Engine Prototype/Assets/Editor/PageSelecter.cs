using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace StepInEngine
{
    public class PageSelecter : EditorWindow 
    {
        #region Variables

        private static Vector2 _scrollPos = Vector2.zero;

        public static PageSelecter instance = null;

        public static string sceneName = string.Empty;

        public static string pagePath = string.Empty;

        #endregion

        public PageSelecter()
        {
            instance = this;
        }
        
        [MenuItem("Tools/Window/Page Selection")]
        public static void Open()
        {
            GetWindow(typeof(PageSelecter), false, "Page Selection");
        }

        private void OnGUI()
        {
            _scrollPos = EditorGUILayout.BeginScrollView(_scrollPos);
            GUI_SelectScene();
            EditorGUILayout.EndScrollView();
            
            GUI_NewScene();
        }

        #region GUI Functions

        private static void GUI_SelectScene()
        {
            if (WindowMain.scenePaths == null)
            {
                return;
            }

            foreach (string scenePath in WindowMain.scenePaths)
            {
                GUILayout.BeginHorizontal();
                
                if (GUILayout.Button(Path.GetFileNameWithoutExtension(scenePath)))
                {      
                    EditorSceneManager.OpenScene(scenePath);
                    
                    sceneName = Path.GetFileNameWithoutExtension(scenePath);

                    if (File.Exists(Application.dataPath + "/Books/" + WindowMain.bundleName + "/" + sceneName + ".xhtml"))
                    {
                        pagePath = Application.dataPath + "/Books/" + WindowMain.bundleName + "/" + sceneName + ".xhtml";
                        Debug.Log(pagePath);
                    }

                }

                if (GUILayout.Button("X", GUILayout.Width(20)))
                {
                    if (EditorUtility.DisplayDialog("Delete Page", "Are you sure you want to delete the page?", "Yes", "No"))
                    {                        
                        if (AssetDatabase.DeleteAsset(scenePath))
                        {
                            AssetDatabase.RemoveUnusedAssetBundleNames();
                            AssetDatabase.Refresh();
                            instance.Repaint();
                        }                        
                    }
                }
                
                GUILayout.EndHorizontal();
            }
        }

        private static void GUI_NewScene()
        {
            if (GUILayout.Button("Create new page"))
            {
                EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);                
            }
        }

        #endregion        
    }
}

