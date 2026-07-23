using System.Collections.Generic;
using UnityEngine;

public class LootManager : MonoBehaviour
{
    private Dictionary<Loot, Pool<Loot>> _pools;

    public void Init()
    {
        _pools = new Dictionary<Loot, Pool<Loot>>();
    }

    public void CreateLoot(Enemy enemy)
    {
        if (enemy == null)
        {
            return;
        }

        LootItem item = enemy.LootDrop.GetItem();
        
        if (!_pools.TryGetValue(item.Loot, out Pool<Loot> pool))
        {
            pool = new Pool<Loot>(item.Loot, 5, 5, transform);
            _pools.Add(item.Loot, pool);
        }

        Loot newLoot = pool.Get();
        newLoot.transform.position = enemy.transform.position;
        newLoot.Init(pool.Release);
    }
}
