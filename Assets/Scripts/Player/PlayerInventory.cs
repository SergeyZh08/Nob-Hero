using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public Player Player { get; private set; }
    private int _coinCount;

    public void Init(Player player)
    {
        Player = player;
    }

    public void AddCoin(int value)
    {
        _coinCount += value;
    }
}
