using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

namespace StepInEngine
{
    public class ChapterText : EditorWindow
    {        
        #region Variables

        private Vector2 scrollPos = Vector2.zero;

        private string oldPagePath = string.Empty; 
        
        private static List<string> _texts = new List<string>();

		static string _TrimmedHtmlText; 

        #endregion
        
        #region Functions

        [MenuItem("Tools/Window/Chapter texts")]
        public static void Open()
        {
            GetWindow(typeof(ChapterText), false, "Chapter texts");
        }

        private static void GetParagraphs(string pagePath)
        {
            _texts = new List<string>();
            string xhtml = File.ReadAllText(pagePath);
			_TrimmedHtmlText = xhtml; 

			//MatchCollection m = Regex.Matches(xhtml, @"<p.*?(?=>)>\s*(.+?)\s*</p>");

			// <[a-zA-Z\/][^>]*>
			// @"<[a-zA-Z\/\s][^>]*(.*)>"
			//MatchCollection m = Regex.Matches(xhtml, @"<[a-zA-Z\/][^>]*>"); 

			//MatchCollection span = Regex.Matches(xhtml, @"<span.*?(?=>)>\s*(.+?)\s*</span>");


			// Replace all html tags with an empty string... 
			_TrimmedHtmlText = Regex.Replace(_TrimmedHtmlText, @"<[a-zA-Z\/][^>]*>", ""); 

			/*
            foreach (Match match in m)
            {
                _texts.Add(match.Groups[0].Value);
            }

			foreach(Match match in span){
				_texts.Add(match.Groups[0].Value); 
			}

*/
        }
        
        #endregion
        
        #region GUI Functions

        private void OnGUI()
        {
            if (PageSelecter.pagePath == string.Empty)
            {
                return;
            }

            if (PageSelecter.pagePath != oldPagePath)
            {
                oldPagePath = PageSelecter.pagePath;
                GetParagraphs(PageSelecter.pagePath);                   
            }
            
            scrollPos = EditorGUILayout.BeginScrollView(scrollPos);            
            
            OnGUI_ShowTexts();
            
            EditorGUILayout.EndScrollView();

        }

		private static void OnGUI_ShowTexts()
		{
			GUILayout.BeginVertical();
			GUILayout.Label("Paragraph...");
			GUILayout.TextArea(_TrimmedHtmlText);
			GUILayout.EndVertical();
		}


		/*
        private static void OnGUI_ShowTexts()
        {
            foreach (string text in _texts)
            {
				GUILayout.BeginVertical();
				GUILayout.Label("Paragraph..."); 
				GUILayout.TextArea(_TrimmedHtmlText);
				GUILayout.EndVertical(); 
            }
        }

*/



        #endregion        
    }
}