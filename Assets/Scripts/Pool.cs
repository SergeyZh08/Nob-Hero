using System;
using System.Collections.Generic;
using UnityEngine;

public class Pool<T> where T : MonoBehaviour, IPoolable
{
    private Stack<T> _availableItems;
    private List<T> _allItems;
    private HashSet<T> _inPool;
    private T _prefab;
    private Transform _parent;
    private int _step;

    public Pool(T prefab, int startSize = 1, int step = 1, Transform parent = null)
    {
        _availableItems = new Stack<T>(startSize);
        _allItems = new List<T>(startSize);

        _inPool = new HashSet<T>();
        _prefab = prefab;
        _parent = parent;

        _step = Mathf.Max(1, step);
        startSize = Mathf.Max(0, startSize);

        ExpandPool(startSize);
    }

    public T Get(Action<T> action = null)
    {
        if (_availableItems.Count == 0)
        {
            ExpandPool(_step);
        }

        var t = _availableItems.Pop();
        _inPool.Remove(t);

        t.OnGetFromPool();
        action?.Invoke(t);
        t.gameObject.SetActive(true);

        return t;
    }

    public void Release(T t)
    {
        if (t == null || _inPool.Contains(t))
        {
            return;
        }

        t.OnReleaseToPool();
        t.gameObject.SetActive(false);
        _inPool.Add(t);
        _availableItems.Push(t);
    }

    public void Clear()
    {
        foreach (var item in _allItems)
        {
            UnityEngine.Object.Destroy(item.gameObject);
        }

        _allItems.Clear();
        _availableItems.Clear();
        _inPool.Clear();
    }

    private void ExpandPool(int count)
    {
        for (int i = 0; i < count; i++)
        {
            AddElement();
        }
    }

    private void AddElement()
    {
        T t = UnityEngine.Object.Instantiate(_prefab, _parent);
        t.gameObject.SetActive(false);
        _allItems.Add(t);
        _availableItems.Push(t);
        _inPool.Add(t);
    }
}
