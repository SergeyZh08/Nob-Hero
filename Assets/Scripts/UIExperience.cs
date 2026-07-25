using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIExperience : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private TextMeshProUGUI _level;
    [SerializeField] private Image _experienceAmount;

    private void OnEnable()
    {
        _player.Experience.OnExperienceAdded += UpdateExperience;
        _player.Experience.OnLevelUp += UpdateLevel;
    }

    private void OnDisable()
    {
        _player.Experience.OnExperienceAdded -= UpdateExperience;
        _player.Experience.OnLevelUp -= UpdateLevel;
    }

    private void UpdateExperience(ExperienceData data)
    {
        _experienceAmount.fillAmount = data.Experience / data.ExperienceToNextLevel;
    }

    private void UpdateLevel(int level)
    {
        _level.text = level.ToString();
    }
}
