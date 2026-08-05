using TMPro;
using UnityEngine;

public class UiCoins : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private TextMeshProUGUI _coinText;

    private void Start()
    {
        _player.Inventory.OnCoinChanged += SetValue;

        SetValue(_player.Inventory.CoinCount);
    }

    private void OnDestroy()
    {
        _player.Inventory.OnCoinChanged -= SetValue;
    }

    private void SetValue(int value)
    {
        _coinText.text = value.ToString("000");
    }
}
