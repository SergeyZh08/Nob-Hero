using UnityEngine;

public class HealthLoot : Loot
{
    [SerializeField] private int _healthCount = 10;
    protected override void Take(Player player)
    {
        base.Take(player);
        player.Health.AddHealth(_healthCount);
    }
}
