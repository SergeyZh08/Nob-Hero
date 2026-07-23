using System;
using System.Collections;
using UnityEngine;

public class Collector : MonoBehaviour
{
    [SerializeField] private float _radiusForScan;
    [SerializeField] private LayerMask _lootMask;
    [SerializeField] private Player _player;
    private readonly Collider[] _colliders = new Collider[16];
    public event Action<int> OnExperienceTaken;
    public event Action<int> OnHealthTaken;
    public event Action<int> OnCoinTaken;

    private void Start()
    {
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
        float radius = _radiusForScan + (1 * _player.RadiusBoost);

        int count = Physics.OverlapSphereNonAlloc(transform.position, radius, _colliders, _lootMask, QueryTriggerInteraction.Ignore);

        for (int i = 0; i < count; i++)
        {
            if (_colliders[i].TryGetComponent(out Loot loot))
            {
                loot.Collect(this);
            }
        }
    }

    public void TakeExperience(int value)
    {
        OnExperienceTaken?.Invoke(value);
    }

    public void TakeHealth(int value)
    {
        OnHealthTaken?.Invoke(value);
    }

    public void TakeCoin(int value)
    {
        OnCoinTaken?.Invoke(value);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, _radiusForScan);
        Gizmos.DrawWireSphere(transform.position, _radiusForScan + (1 * _player.RadiusBoost));
    }
}
