using System;
using UnityEngine;

public class ChainLighting : MonoBehaviour, IPoolable
{
    [SerializeField] private LayerMask _layerMask;
    private int _passCount;
    private float _speed;
    private float _damage;
    private Enemy _targetEmeny;
    private Collider[] _colliders = new Collider[10];
    private float _radiusForScan = 5f;
    private Action<ChainLighting> _release;

    public void Init(int passCount, float speed, float damage, Enemy enemy, Action<ChainLighting> action)
    {
        _passCount = passCount;
        _speed = speed;
        _damage = damage;
        _targetEmeny = enemy;
        _release = action;
    }

    private void Update()
    {
        if (_targetEmeny.IsAlive)
        {
            Vector3 toTarget = (_targetEmeny.transform.position - transform.position).normalized;
            transform.rotation = Quaternion.LookRotation(toTarget);
            transform.position = Vector3.MoveTowards(transform.position, _targetEmeny.transform.position, Time.deltaTime * _speed);

            if (transform.position == _targetEmeny.transform.position)
            {
                _targetEmeny.TakeDamage(_damage);
                _passCount--;

                if (_passCount == 0)
                {
                    Die();
                    return;
                }

                GetNextClosestEnemy(_targetEmeny);
            }
        }
        else
        {
            Die();
        }
    }

    private void GetNextClosestEnemy(Enemy enemy)
    {
        int len = Physics.OverlapSphereNonAlloc(transform.position, _radiusForScan, _colliders, _layerMask);

        if (len > 0)
        {
            Collider currentCollider = enemy.GetComponent<Collider>();

            int targetIndex = -1;
            float minDistance = Mathf.Infinity;

            for (int i = 0; i < len; i++)
            {
                if (currentCollider == _colliders[i])
                {
                    continue;
                }

                float currentDistance = Vector3.SqrMagnitude(_colliders[i].transform.position - transform.position);

                if (minDistance > currentDistance)
                {
                    minDistance = currentDistance;
                    targetIndex = i;
                }
            }

            if (targetIndex == -1)
            {
                Die();
                return;
            }

            if (_colliders[targetIndex].TryGetComponent(out Enemy newEnemy))
            {
                _targetEmeny = newEnemy;
            }
        }
        else
        {
            Die();
        }
    }

    private void Die()
    {
        _release?.Invoke(this);
    }

    public void OnGetFromPool()
    {
        
    }

    public void OnReleaseToPool()
    {
        _release = null;
        _targetEmeny = null;
        _passCount = 0;
        _speed = 0;
        _damage = 0;
    }
}
