using TMPro;
using UnityEngine;

public class UiCoins : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private TextMeshProUGUI _coinText;

    private void Start()
    {
        _player.Inventory.OnCoinAdded += SetValue;
    }

    private void OnDestroy()
    {
        _player.Inventory.OnCoinAdded -= SetValue;
    }

    private void SetValue(int value)
    {
        _coinText.text = value.ToString("000");
    }
}
