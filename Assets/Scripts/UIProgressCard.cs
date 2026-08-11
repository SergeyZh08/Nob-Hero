using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIProgressCard : MonoBehaviour
{
    [SerializeField] private ProgressCard _progressCard;
    [SerializeField] private Image _coinImage;
    [SerializeField] private TextMeshProUGUI _costText;
    [SerializeField] private TextMeshProUGUI _maxLevel;
    [SerializeField] private TextMeshProUGUI _level;

    private void Start()
    {
        _progressCard.OnCostChanged += SetCost;

        SetCost(_progressCard.Config, _progressCard.Player.Inventory.CoinCount);
    }

    private void OnDestroy()
    {
        _progressCard.OnCostChanged -= SetCost;
    }

    private void SetCost(ProgressCardConfig config, int currentNoney)
    {
        bool maxLevel = config.Level >= config.MaxLevel;

        _maxLevel.gameObject.SetActive(maxLevel);
        _costText.gameObject.SetActive(!maxLevel);
        _coinImage.gameObject.SetActive(!maxLevel);

        _costText.text = config.CurrentCost.ToString();
        _level.text = config.Level.ToString();

        _costText.color = currentNoney >= config.CurrentCost ? Color.green : Color.red;
    }
}
