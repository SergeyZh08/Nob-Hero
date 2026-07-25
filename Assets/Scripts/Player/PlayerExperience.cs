
using System;
using UnityEngine;

[System.Serializable]
public struct ExperienceData
{
    public float Experience;
    public float ExperienceToNextLevel;
    public int Level;
}

public class PlayerExperience : MonoBehaviour
{
    public event Action<ExperienceData> OnExperienceAdded;
    public event Action<int> OnLevelUp;
    [SerializeField] private AnimationCurve _experienceLevelCurve;
    public Player Player { get; private set; }

    private ExperienceData _experienceData;

    public void Init(Player player)
    {
        _experienceData.ExperienceToNextLevel = _experienceLevelCurve.Evaluate(0);
        Player = player;
    }

    public void AddExperience(int value)
    {
        _experienceData.Experience += value;

        if (_experienceData.Experience >= _experienceData.ExperienceToNextLevel)
        {
            _experienceData.Level++;
            _experienceData.Experience = 0;
            UpdateNextLevelValue(_experienceData.Level);
        }

        OnExperienceAdded?.Invoke(_experienceData);
    }
    
    public void UpdateNextLevelValue(int value)
    {
        _experienceData.ExperienceToNextLevel = _experienceLevelCurve.Evaluate(value);
        OnLevelUp?.Invoke(_experienceData.Level);
    }
}
