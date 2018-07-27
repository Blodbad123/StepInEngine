using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace StepInEngine
{
    public class MetaData : EditorWindow
    {
        #region variables

        private static List<Translation> _titles;
        private static List<string> _authers;
        private static List<Texture2D> _images;
        
        

        #endregion

        [MenuItem("Window/Meta Data")]
        public static void Window()
        {
            GetWindow(typeof(MetaData), false, "MetaData");
            _titles = new List<Translation>
            {
                new Translation("Dansk", "En test"),
                new Translation("English", "A test")
            };

           // WebService_v2.instance.OnAwake();
            _authers = new List<string>();
            _images = new List<Texture2D>();
        }

        private void OnGUI()
        {
            ShowTitle();
            //ShowAuther();
            ShowImage();
        }

        /// <summary>
        /// TODO
        /// </summary>
        private static void ShowImage()
        {
            GUILayout.Label("Images");
            for (var i = 0; i < _images.Count; i++)
            {
                EditorGUILayout.BeginHorizontal();
                if (GUILayout.Button("X", GUILayout.Width(20)))
                {
                    _images.Remove(_images[i]);
                    return;
                }

                _images[i] = (Texture2D) EditorGUILayout.ObjectField(_images[i], typeof(Texture2D), true);
                EditorGUILayout.EndHorizontal();

                Debug.Log(GUIUtility.hotControl);
            }

            Texture2D t = (Texture2D)EditorGUILayout.ObjectField(null, typeof(Texture2D), true);
            if (t != null)
            {
                _images.Add(t);
            }
        }

        /// <summary>
        /// TODO
        /// </summary>
        private static void ShowTitle()
        {
            GUILayout.Label("Titels");
            foreach (Translation t in _titles)
            {
                EditorGUILayout.BeginHorizontal();                
                if (GUILayout.Button("X", GUILayout.Width(20)))
                {
                    _titles.Remove(t);
                    return;
                }
                
                t.language = EditorGUILayout.TextField(t.language);
                EditorGUILayout.EndHorizontal();
                
                t.content = EditorGUILayout.TextField(t.content);
            }

            if (GUILayout.Button("Add new title", GUILayout.Height(20)))
            {
                _titles.Add(new Translation("", ""));
            }
        }

        /// <summary>
        /// TODO
        /// </summary>
        private static void ShowAuther()
        {
            GUILayout.Label("Authers");
            for (var i = 0; i < _authers.Count; i++)
            {
                EditorGUILayout.BeginHorizontal();
                if (GUILayout.Button("X", GUILayout.Width(20)))
                {
                    _authers.Remove(_authers[i]);
                    return;
                }

                _authers[i] = EditorGUILayout.TextField(_authers[i]);
                EditorGUILayout.EndHorizontal();
            }

            if (GUILayout.Button("Add new auther", GUILayout.Height(20)))
            {
                _authers.Add("");
            }
        }
    }
}

