using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveData : Singleton<SaveData>
{
    public int BestScore;
    public int CurrentScore;
    public int BonusPoints;

    public Action OnScoreUpdate;

    protected override void Awake()
    {
        Load();
        CurrentScore = 0;
        BonusPoints = 0;
        base.Awake();
    }

    [Serializable]
    class SaveManager
    {
        public int BestScore;
        public int CurrentScore;
        public int BonusPoints;
    }

    public void Save()
    {
        SaveManager data = new SaveManager();
        data.BestScore = BestScore;
        data.CurrentScore = CurrentScore;
        data.BonusPoints = BonusPoints;

        string json = JsonUtility.ToJson(data);
        BinaryFormatter binaryFormatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/saveFile.dat";

        using (Stream output = File.Create(path))
        {
            binaryFormatter.Serialize(output, json);
        }
    }

    public void Load()
    {
        BinaryFormatter bFormatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/saveFile.dat";

        if (File.Exists(path))
        {
            using (Stream input = File.OpenRead(path))
            {
                string json = (string)bFormatter.Deserialize(input);
                SaveManager data = JsonUtility.FromJson<SaveManager>(json);
                BestScore = data.BestScore;
                CurrentScore = data.CurrentScore;
                BonusPoints = data.BonusPoints;
            }
        }
    }
}
