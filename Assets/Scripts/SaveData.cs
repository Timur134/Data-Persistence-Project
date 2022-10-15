using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class SaveData 
{
    [SerializeField]
    private List<Score> scores = new List<Score>();

    [NonSerialized]
    private static SaveData instance = new SaveData();

    private const int MAX_SCORES_NUMBER = 10;

    [NonSerialized]
    private static bool needLoading = true;

    [Serializable]
    public struct Score
    {
        public string name;
        public int points;
    }

    public static SaveData Instance
    {
        get
        {
            if(needLoading)
            {
                load();

                needLoading = false;
            }
            return instance;
        }
    }

    public Score[] Scores
    {
        get
        {
            return scores.ToArray();
        }
    }

    public void AddScore(string name, int points)
    {
        if(scores.Count < MAX_SCORES_NUMBER)
        {
            Score s = new Score();
            s.name = name;
            s.points = points;

            scores.Add(s);
        }
    }

    private static void load()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            instance = JsonUtility.FromJson<SaveData>(json);
        }
    }

    public static void Save()
    {
        string path = Application.persistentDataPath + "/savefile.json";

        File.WriteAllText(path, JsonUtility.ToJson(instance));
    }
}
