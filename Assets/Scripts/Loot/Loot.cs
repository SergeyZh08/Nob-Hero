using System;
using System.Collections;
using UnityEngine;

public class Loot : MonoBehaviour, IPoolable
{
    private Action<Loot> OnTaken;
    private bool _isCollected;
    private Coroutine _currentCoroutine;
    [SerializeField] private Collider _collider;

    public void Init(Action<Loot> action)
    {
        OnTaken = action;
    }

    public void Collect(Collector collector)
    {
        if (_isCollected)
        {
            return;
        }

        _isCollected = true;
        _collider.enabled = false;

        if (_currentCoroutine != null)
        {
            StopCoroutine(_currentCoroutine);
        }

        _currentCoroutine = StartCoroutine(CollectProcess(collector));
    }

    private IEnumerator CollectProcess(Collector collector)
    {
        Vector3 a = transform.position;
        Vector3 b = a + Vector3.up * 2;

        for (float t = 0; t < 1; t += Time.deltaTime * 3)
        {
            Vector3 d = collector.transform.position;
            Vector3 c = d + Vector3.up * 2;

            transform.position = Bezier.GetPoint(a, b, c, d, t);

            yield return null;
        }

        Take(collector);
    }

    protected virtual void Take(Collector collector)
    {
        OnTaken?.Invoke(this);
    }

    public void OnGetFromPool()
    {
        _collider.enabled = true;
        _isCollected = false;
    }

    public void OnReleaseToPool()
    {
        if (_currentCoroutine != null)
        {
            StopCoroutine(_currentCoroutine);
        }
        
        _collider.enabled = false;
        OnTaken = null;
    }
}
