using UnityEngine;

public class CoinLoot : Loot
{
    [SerializeField] private int _cointCount = 10;
    protected override void Take(Collector collector)
    {
        base.Take(collector);
        collector.TakeCoin(_cointCount);
    }

}
