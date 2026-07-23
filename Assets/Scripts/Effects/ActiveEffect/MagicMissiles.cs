using System;
using UnityEngine;

public class MagicMissiles : MonoBehaviour, IPoolable
{
    private float _speed;
    private float _damage;
    private Enemy _enemy;
    private Action<MagicMissiles> _release;
    private bool _isDead = false;

    public void Init(float speed, float damage, Enemy enemy, Action<MagicMissiles> action)
    {
        _speed = speed;
        _damage = damage;
        _enemy = enemy;
        _release = action;
    }

    public void Update()
    {
        if (_enemy.IsAlive)
        {
            transform.position = Vector3.MoveTowards(transform.position, _enemy.transform.position, Time.deltaTime * _speed);

            if (transform.position == _enemy.transform.position)
            {
                _enemy.TakeDamage(_damage);
                Die();
            }
        }
        else
        {
            Die();
        }
    }

    private void Die()
    {
        if (_isDead)
        {
            return;
        }

        _isDead = true;
        _release?.Invoke(this);
    }

    public void OnGetFromPool()
    {
        _isDead = false;
    }

    public void OnReleaseToPool()
    {
        _release = null;
        _enemy = null;
        _speed = 0;
        _damage = 0;
    }
}
