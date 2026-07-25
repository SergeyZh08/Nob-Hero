using System;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
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
}
