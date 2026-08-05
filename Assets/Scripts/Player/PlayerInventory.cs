using System;
using UnityEngine;

public class PlayerInventory : MonoBehaviour, ISaved
{
    public event Action<int> OnCoinAdded;
    public Player Player { get; private set; }
    private int _coinCount;

    public void Init(Player player)
    {
        Player = player;
    }

    public void AddCoin(int value)
    {
        _coinCount += value;
        OnCoinAdded?.Invoke(_coinCount);
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
        OnCoinAdded?.Invoke(_coinCount);
    }
}
