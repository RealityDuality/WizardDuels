using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

//namespace Assets.Scripts
//{
    public class GameFileHandler : MonoBehaviour
    {
        public GameSettings gs;
        public string GameSettingPath = "Assets/Resources/test.txt";

        private void Start()
        {
            string json = ReadString(GameSettingPath); // Load from file

            if (json == "")
            {
                string s = JsonUtility.ToJson(gs);
                WriteString(GameSettingPath, s);
            }
            else
            {
                JsonUtility.FromJsonOverwrite(json, gs);
            }

        }

        static void WriteString(string path, string toWrite)
        {
            //Write some text to the test.txt file
            StreamWriter writer = new StreamWriter(path, true);
            writer.Write(toWrite);

            writer.Close();
        }

        static string ReadString(string path)
        {
            // path = "Assets/Resources/test.txt";
            StreamReader reader = null;
            try
            {
                //Read the text from directly from the test.txt file
                reader = new StreamReader(path);
                return reader.ReadToEnd();
            }
            catch (Exception ex)
            {
                return "";
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
        }



    }
//}
