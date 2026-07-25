using System;
using UnityEngine;

public class Enemy : MonoBehaviour, IPoolable
{
    [field: SerializeField] public LootDrop LootDrop {get; private set;}
    [SerializeField] private float _maxHealth;
    [SerializeField] private float _health;
    [SerializeField] private float _dps;
    [SerializeField] private float _speed;
    [SerializeField] private float _angularSpeed;
    [SerializeField] private float _radiusForAttack;
    [SerializeField] private float _attackPeriod;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _distanceToCarry;
    public bool IsAlive => gameObject.activeInHierarchy;
    private float _timer;
    private Player _player;
    private Transform _target;
    public event Action<Enemy> OnEnemyDie;
    public event Action<float> OnEnemyHit;

    public void Init(Transform target)
    {
        _target = target;
    }

    private void Update()
    {
        if (_player)
        {
            _timer += Time.deltaTime;

            if (_timer >= _attackPeriod)
            {
                _timer = 0;
                Attack();
            }
        }
    }

    private void FixedUpdate()
    {
        _rigidbody.linearVelocity = transform.forward * _speed;

        Vector3 toTarget = _target.position - transform.position;

        Quaternion rotation = Quaternion.LookRotation(toTarget);

        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * _angularSpeed);

        if (toTarget.magnitude > _distanceToCarry)
        {
            transform.position += toTarget * 1.95f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Player player))
        {
            _player = player;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Player player))
        {
            _player = null;
        }
    }

    public void TakeDamage(float value)
    {
        _health = Mathf.Max(0, _health - value);
        OnEnemyHit?.Invoke(value);

        if (_health == 0)
        {
            Die();
        }
    }

    [ContextMenu("Die")]
    private void Die()
    {
        OnEnemyDie?.Invoke(this);
    }

    private void Attack()
    {
        _player.Health.TakeDamage(_dps * _attackPeriod);
    }

    public void OnGetFromPool()
    {
        _health = _maxHealth;
        _timer = 0;
    }

    public void OnReleaseToPool()
    {
        _player = null;
        _target = null;
        _rigidbody.linearVelocity = Vector3.zero;
        OnEnemyDie = null;
        OnEnemyHit = null;
    }
}
