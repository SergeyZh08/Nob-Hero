using UnityEngine;
using UnityEngine.UI;

public class UIPlayerHealth : MonoBehaviour
{
    [SerializeField] private Image _scale;
    [SerializeField] private PlayerHealth _playerHealth;

    public void OnEnable()
    {
        _playerHealth.OnHealthChanged += UpdateHP;
    }

    public void OnDisable()
    {
        _playerHealth.OnHealthChanged -= UpdateHP;
    }

    private void UpdateHP(PlayerHealthData data)
    {
        _scale.fillAmount = data.Health / data.MaxHealth;
    }
}
