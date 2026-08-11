using System.IO;
using UnityEngine;

public class JsonSave : IStorageService
{
    private string _path = Path.Combine(Application.persistentDataPath, "save.json");

    public SaveData Load()
    {
        if (!File.Exists(_path))
        {
            return CreateDefaultData();
        }

        string json = File.ReadAllText(_path);
        SaveData data = JsonUtility.FromJson<SaveData>(json);

        return data;
    }

    public void Save(SaveData data)
    {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(_path, json);
    }

    private SaveData CreateDefaultData()
    {
        return new SaveData
        {
            Wawe = 0,
            Coins = 0,
            ProgressDataLevels = null,
            PermanentStats = null
        };
    }

    public void Delete()
    {
        if (File.Exists(_path))
        {
            File.Delete(_path);
        }
    }
}
