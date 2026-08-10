using System;
using UnityEngine;

public class PlayerInventory : MonoBehaviour, ISaved
{
    public event Action<int> OnCoinChanged;
    public Player Player { get; private set; }
    public int CoinCount => _coinCount;
    private int _coinCount;

    public void Init(Player player)
    {
        Player = player;
    }

    public void AddCoin(int value)
    {
        _coinCount += value;
        OnCoinChanged?.Invoke(_coinCount);
    }

    public void SpendCoin(int value)
    {
        _coinCount -= value;
        OnCoinChanged?.Invoke(_coinCount);
    }

    public void SaveTo(SaveData data)
    {
        data.Coins = _coinCount;
        Debug.Log("Save: " + data.Coins);
    }

    public void LoadFrom(SaveData data)
    {
        _coinCount = data.Coins;
        Debug.Log("Load: " + data.Coins);
        OnCoinChanged?.Invoke(_coinCount);
    }
}
