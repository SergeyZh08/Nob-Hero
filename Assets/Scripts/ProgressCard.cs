using System;
using UnityEngine;
using UnityEngine.UI;

public class ProgressCard : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private int _startCost;
    public event Action OnBuy;
    public event Action<int> OnCalculate;
    public int Level {get; private set;}
    private int _currentCost;
    private Player _player;

    public void Init(Player player)
    {
        _player = player;
        _button.onClick.AddListener(Buy);
    }

    public void SetLevel(int level)
    {
        Level = level;
        CalculateCost();
    }

    private void CalculateCost()
    {
        _currentCost = _startCost + (_startCost * Level);
        OnCalculate?.Invoke(_currentCost);
    }

    private void Buy()
    {
        if (_player.Inventory.CoinCount < _currentCost)
        {
            return;
        }

        _player.Inventory.SpendCoin(_currentCost);

        OnBought();
    }

    protected virtual void OnBought()
    {
        Level++;
        CalculateCost();
    }
}
