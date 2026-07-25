using System;
using System.Collections;
using UnityEngine;

public class Collector : MonoBehaviour
{
    [SerializeField] private float _radiusForScan;
    [SerializeField] private LayerMask _lootMask;
    public Player Player { get; private set; }
    private readonly Collider[] _colliders = new Collider[16];

    public void Init(Player player)
    {
        Player = player;
        StartCoroutine(ScanRoutine());
    }

    private IEnumerator ScanRoutine()
    {
        var delay = new WaitForSeconds(0.1f);

        while (true)
        {
            ScanArea();
            yield return delay;
        }
    }

    private void ScanArea()
    {
        float radius = _radiusForScan + (1 * Player.Stats.RadiusBoost);

        int count = Physics.OverlapSphereNonAlloc(transform.position, radius, _colliders, _lootMask, QueryTriggerInteraction.Ignore);

        for (int i = 0; i < count; i++)
        {
            if (_colliders[i].TryGetComponent(out Loot loot))
            {
                loot.Collect(Player);
            }
        }
    }

    // private void OnDrawGizmos()
    // {
    //     Gizmos.DrawWireSphere(transform.position, _radiusForScan);
    //     Gizmos.DrawWireSphere(transform.position, _radiusForScan + (1 * Player.Stats.RadiusBoost));
    // }
}
