using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIExperience : MonoBehaviour
{
    [SerializeField] private ExperienceManager _experienceManager;
    [SerializeField] private TextMeshProUGUI _level;
    [SerializeField] private Image _experienceAmount;

    private void OnEnable()
    {
        _experienceManager.OnExperienceAdded += UpdateExperience;
        _experienceManager.OnLevelUp += UpdateLevel;
    }

    private void OnDisable()
    {
        _experienceManager.OnExperienceAdded -= UpdateExperience;
        _experienceManager.OnLevelUp -= UpdateLevel;
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
