using System;
using UnityEngine;

public class LevelupManager : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private EffectManager _effectManager;
    [SerializeField] private ExperienceVisual _experienceVisual;

    public void Init()
    {
        _player.Experience.OnLevelUp += LevelUp;
        _experienceVisual.OnAnimationEnd += ShowEffect;
    }

    private void OnDisable()
    {
        _experienceVisual.OnAnimationEnd -= ShowEffect;
        _player.Experience.OnLevelUp -= LevelUp;
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
