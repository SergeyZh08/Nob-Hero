
using System;
using UnityEngine;

[System.Serializable]
public struct ExperienceData
{
    public float Experience;
    public float ExperienceToNextLevel;
    public int Level;
}

public class ExperienceManager : MonoBehaviour
{
    public event Action<ExperienceData> OnExperienceAdded;
    public event Action<int> OnLevelUp;

    [SerializeField] private Collector _collector;
    [SerializeField] private AnimationCurve _experienceLevelCurve;

    private ExperienceData _experienceData;

    public void Init()
    {
        _experienceData.ExperienceToNextLevel = _experienceLevelCurve.Evaluate(0);
    }

    private void OnEnable()
    {
        _collector.OnExperienceTaken += AddExperience;
    }

    private void OnDisable()
    {
        _collector.OnExperienceTaken -= AddExperience;
    }

    private void AddExperience(int value)
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
