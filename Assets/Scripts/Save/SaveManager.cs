using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    private IStorageService _storageService;
    private List<ISaved> _savedData = new List<ISaved>();

    public void Init()
    {
        _storageService = new JsonSave();
    }

    public void Register(ISaved saved)
    {
        _savedData.Add(saved);
    }

    [ContextMenu("Load")]
    public void Load()
    {
        SaveData data = _storageService.Load();

        foreach (var s in _savedData)
        {
            s.LoadFrom(data);
        }
    }

    [ContextMenu("Save")]
    public void Save()
    {
        SaveData data = new SaveData();

        foreach (var s in _savedData)
        {
            s.SaveTo(data);
        }

        _storageService.Save(data);
    }
    
    [ContextMenu("Delete")]
    public void DeleteSave()
    {
        _storageService.Delete();
    }
}
