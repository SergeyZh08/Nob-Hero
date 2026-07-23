using System;
using UnityEngine;

public class LevelupManager : MonoBehaviour
{
    [SerializeField] private ExperienceManager _experienceManager;
    [SerializeField] private EffectManager _effectManager;
    [SerializeField] private ExperienceVisual _experienceVisual;

    private void OnEnable()
    {
        _experienceManager.OnLevelUp += LevelUp;
        _experienceVisual.OnAnimationEnd += ShowEffect;
    }

    private void OnDisable()
    {
        _experienceVisual.OnAnimationEnd -= ShowEffect;
        _experienceManager.OnLevelUp -= LevelUp;
    }

    private void LevelUp(int level)
    {
        _experienceVisual.Play();
    }

    private void ShowEffect()
    {
        _effectManager.ShowEffect();
    }
}
