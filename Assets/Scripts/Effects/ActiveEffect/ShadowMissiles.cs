using System;
using UnityEngine;

public class ShadowMissiles : MonoBehaviour, IPoolable
{
    [SerializeField] private Rigidbody _rigidbody;
    private int _passCount;
    private float _speed;
    private float _damage;
    private float _lifeTime;
    private float _timer;
    private bool _isDead = false;
    private Action<ShadowMissiles> _release;

    public void Init(int passCount, float speed, float damage, float lifetime, Action<ShadowMissiles> action)
    {
        _passCount = passCount;
        _speed = speed;
        _damage = damage;
        _release = action;
        _lifeTime= lifetime;
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer >= _lifeTime)
        {
            Die();
        }
    }

    private void FixedUpdate()
    {
        _rigidbody.linearVelocity = transform.forward * _speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Enemy enemy))
        {
            enemy.TakeDamage(_damage);
            _passCount --;

            if (_passCount == 0)
            {
                Die();
            }
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
        _timer = 0;
        _isDead = false;
    }

    public void OnReleaseToPool()
    {
        _passCount = 0;
        _speed = 0;
        _damage = 0;
        _release = null;

        _rigidbody.linearVelocity = Vector3.zero;
    }
}
