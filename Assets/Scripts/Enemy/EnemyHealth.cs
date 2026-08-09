using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public Enemy Enemy {get; private set;}
    [SerializeField] private float _maxHealth;
    [SerializeField] private float _health;
    public event Action<float> OnEnemyHit;

    public void Init(Enemy enemy)
    {
        Enemy = enemy;
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
        Enemy.Die();
    }

    private void OnEnable()
    {
        _health = _maxHealth;
    }

    private void OnDisable()
    {
        OnEnemyHit = null;
    }
}
