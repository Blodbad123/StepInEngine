using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StepInEngine
{
    public class LoadSaveMenu : MonoBehaviour
    {

        public string text = "";
        private string saveGameName = "My Saved Game";

        void OnGUI()
        {
            saveGameName = GUI.TextArea(new Rect(20, 0, 50, 50), saveGameName);
            text = GUI.TextArea(new Rect(20, 50, 50, 50), text);

            if (GUI.Button(new Rect(20, 100, 50, 50), "Save"))
            {
                var newSaveGame = new Serialize
                {
                    saveName = saveGameName,
                    testString = text,
                };
                LoadSave.Save(newSaveGame);
            }

            if (GUI.Button(new Rect(20, 150, 50, 50), "Load"))
            {
                Serialize loadedGame = LoadSave.Load(saveGameName);
                if (loadedGame != null)
                {
                    text = loadedGame.testString;
                    Debug.Log(text);
                }
            }
        }
    }
}
