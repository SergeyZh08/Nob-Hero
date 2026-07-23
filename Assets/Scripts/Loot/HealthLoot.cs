using UnityEngine;

public class HealthLoot : Loot
{
    [SerializeField] private int _healthCount = 10;
    protected override void Take(Collector collector)
    {
        base.Take(collector);
        collector.TakeHealth(_healthCount);
    }
}
