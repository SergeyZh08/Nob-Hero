using UnityEngine;

public class LevelupManager : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private EffectManager _effectManager;
    [SerializeField] private ExperienceVisual _experienceVisual;

    public void Init()
    {
        _player.Experience.OnLevelUp += LevelUp;

        _experienceVisual.Init(_player.transform);
    }

    private void OnDisable()
    {
        _player.Experience.OnLevelUp -= LevelUp;
    }

    public void StartLeveling()
    {
        ShowEffect();
    }

    private void LevelUp(int level)
    {
        _experienceVisual.Play(ShowEffect);
    }

    private void ShowEffect()
    {
        _effectManager.ShowEffect();
    }
}
