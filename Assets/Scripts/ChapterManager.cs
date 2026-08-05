using System;
using UnityEngine;

public class ChapterManager : MonoBehaviour, ISaved
{
    [SerializeField] private ChapterSettings[] _settings;
    public int CurrentChapter => _currentChapter;
    private int _currentChapter;
    public event Action<int> OnNextChapter;

    public ChapterSettings GetChapterSettings => _currentChapter < _settings.Length ? _settings[_currentChapter] : _settings[_settings.Length - 1];

    public void LoadFrom(SaveData data)
    {
        _currentChapter = data.Wawe;
        Debug.Log("Load Wawe: " + data.Wawe);
    }

    public void SaveTo(SaveData data)
    {
        data.Wawe = _currentChapter;
        Debug.Log("Save Wawe: " + data.Wawe);
    }
    
    [ContextMenu("NextWawe")]
    public void NextWawe()
    {
        _currentChapter++;

        _currentChapter = Math.Clamp(_currentChapter, 0, _settings.Length - 1);

        OnNextChapter?.Invoke(_currentChapter);
    }
}
