using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace StepInEngine
{
    public class ImageSelecter : EditorWindow 
    {
        #region Variables

        private List<Texture2D> images = new List<Texture2D>();

        private Texture2D texture2D;

        private static EditorWindow _window;

        private static Rect _rect;
        
        private static string _oldSceneName = string.Empty;

        #endregion

        #region GUI Funcitons

        private void OnGUI()
        {
            if (_oldSceneName != PageSelecter.sceneName)
            {
                GetImages();
                _oldSceneName = PageSelecter.sceneName;
            }
            
            GUI_ShowPreviewImage();
            
            GUI_ShowImages();
        }

        private void GUI_ShowImages()
        {
            for (var i = 0; i < images.Count; i++)
            {
                EditorGUILayout.BeginHorizontal();

                if (GUILayout.Button("P", GUILayout.Width(30)))
                {
                    texture2D = images[i];
                }
                
                images[i] = (Texture2D) EditorGUILayout.ObjectField(images[i], typeof(Texture2D), true);
                
                EditorGUILayout.EndHorizontal();
            }
        }

        private void GUI_ShowPreviewImage()
        {
            if (texture2D == null)
            {
                return;
            }
            
            EditorGUI.DrawPreviewTexture(_rect, texture2D);
        }

        #endregion

        #region Functions

        [MenuItem("Tools/Window/Image selecter")]
        private static void Open()
        {
           _window = GetWindow(typeof(ImageSelecter), false, "Image Selecter");           
        }
       
        private void GetImages()
        {
            images = new List<Texture2D>();
            string imagePath = Application.dataPath + "/Books/" + WindowMain.bundleName + "/images/";
            Debug.Log(imagePath);
            var d = new DirectoryInfo(imagePath);
            Debug.Log("info: " + d);
            FileInfo[] files = d.GetFiles("*.jpg");
            
            foreach (FileInfo file in files)
            {
                if (File.Exists(imagePath + file.Name))
                {
                    var fileData = File.ReadAllBytes(imagePath + file.Name);
                    var tex      = new Texture2D(2, 2);
                    tex.LoadImage(fileData);
                
                    images.Add(tex);
                }
            }

            if (_window != null)
            {
                _rect = new Rect(_window.position.width / 2 - 100, 30 * images.Count, 200, 200);
                _window.position.Set(_window.position.x, _window.position.y, _window.position.width, _window.position.height + 30 * images.Count);
            }
        }       

        #endregion               
    }
}