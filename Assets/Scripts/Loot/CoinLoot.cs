using UnityEngine;

public class CoinLoot : Loot
{
    [SerializeField] private int _coinCount = 10;
    protected override void Take(Player player)
    {
        base.Take(player);
        player.Inventory.AddCoin(_coinCount);
    }

}
