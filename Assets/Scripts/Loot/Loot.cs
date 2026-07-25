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

    public void Collect(Player player)
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

        _currentCoroutine = StartCoroutine(CollectProcess(player));
    }

    private IEnumerator CollectProcess(Player player)
    {
        Vector3 a = transform.position;
        Vector3 b = a + Vector3.up * 2;

        for (float t = 0; t < 1; t += Time.deltaTime * 3)
        {
            Vector3 d = player.transform.position;
            Vector3 c = d + Vector3.up * 2;

            transform.position = Bezier.GetPoint(a, b, c, d, t);

            yield return null;
        }

        Take(player);
    }

    protected virtual void Take(Player player)
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
