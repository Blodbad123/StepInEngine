using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace StepInEngine
{
    public static class LoadSave
    {
        public static void Save(Serialize saveGame)
        {

            var bf = new BinaryFormatter();
            FileStream file = File.Create(@"C:\Users\steff\Desktop\" + saveGame.saveName + ".sav");
            bf.Serialize(file, saveGame);
            file.Close();
            Debug.Log("Saved Game: " + saveGame.saveName);

        }

        public static Serialize Load(string gameToLoad)
        {
            if (File.Exists(@"C:\Users\steff\Desktop\" + gameToLoad + ".sav"))
            {
                var bf = new BinaryFormatter();
                FileStream file = File.Open(@"C:\Users\steff\Desktop\" + gameToLoad + ".sav", FileMode.Open);
                var loadedGame = (Serialize)bf.Deserialize(file);
                file.Close();
                Debug.Log("Loaded Game: " + loadedGame.saveName);
                return loadedGame;
            }
            else
            {
                Debug.Log("File doesn't exist!");
                return null;
            }

        }
    }
}

