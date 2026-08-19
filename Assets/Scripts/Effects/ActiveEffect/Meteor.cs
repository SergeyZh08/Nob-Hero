using System;
using System.Collections;
using UnityEditor;
using UnityEngine;

public class Meteor : MonoBehaviour, IPoolable
{
    [SerializeField] private LayerMask _layerMask;
    private readonly static WaitForSeconds _delayBeforeExplo = new WaitForSeconds(0.75f);
    private readonly static WaitForSeconds _delayAfterExplo = new WaitForSeconds(1f);
    private Collider[] _colliders = new Collider[25];
    private float _damage;
    private float _radius;
    private Action<Meteor> _release;
    private Coroutine _currentCoroutine;

    public void Init(float damage, float radius, Action<Meteor> action)
    {
        _damage = damage;
        _radius = radius;
        _release = action;

        _currentCoroutine = StartCoroutine(LifeProcess());
    }

    private IEnumerator LifeProcess()
    {
        yield return _delayBeforeExplo;

        int len = Physics.OverlapSphereNonAlloc(transform.position, _radius, _colliders, _layerMask, QueryTriggerInteraction.Ignore);

        for (int i = 0; i < len; i++)
        {
            if (_colliders[i].TryGetComponent(out Enemy enemy))
            {
                enemy.TakeDamage(_damage);
            }
        }

        yield return _delayAfterExplo;

        Die();
    }

    private void Die()
    {
        _currentCoroutine = null;
        _release?.Invoke(this);
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Handles.DrawWireDisc(transform.position, Vector3.up, _radius);
    }
#endif

    public void OnGetFromPool()
    {
        
    }
        

    public void OnReleaseToPool()
    {
        _damage = 0;
        _radius = 0;
        _release = null;

        if (_currentCoroutine != null)
        {
            StopCoroutine(_currentCoroutine);
            _currentCoroutine = null;
        }
    }
}
