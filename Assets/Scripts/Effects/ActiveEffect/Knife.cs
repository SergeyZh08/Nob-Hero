using System;
using UnityEngine;

public class Knife : MonoBehaviour, IPoolable
{
    public event Action<Knife> OnDie;
    private int _passCount;
    private float _speed;
    private float _damage;
    private Transform _target;
    private Vector3 _lastPosition;
    private Vector3 _offset;
    private bool _isDead = false;

    public void Init(int passCount, float speed, float damage, Transform target)
    {
        _passCount = passCount;
        _speed = speed;
        _damage = damage;
        _target = target;
        _lastPosition = target.position;
    }

    private void LateUpdate()
    {
        _offset = _target.position - _lastPosition;
        _lastPosition = _target.position;

        transform.position += _offset;

        transform.RotateAround(_target.transform.position, Vector3.up, _speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Enemy enemy))
        {
            enemy.TakeDamage(_damage);
            _passCount--;

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
        OnDie?.Invoke(this);
    }

    public void OnGetFromPool()
    {
        _isDead = false;
    }

    public void OnReleaseToPool()
    {
        _target = null;
        _lastPosition = Vector3.zero;
        _offset = Vector3.zero;
        _passCount = 0;
        _speed = 0;
        _damage = 0;
    }
}
