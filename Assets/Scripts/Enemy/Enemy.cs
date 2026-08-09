using System;
using UnityEngine;

public class Enemy : MonoBehaviour, IPoolable
{
    [field: SerializeField] public EnemyMovement Movement { get; private set; }
    [field: SerializeField] public EnemyHealth Health { get; private set; }
    [field: SerializeField] public EnemyAttack Attack { get; private set; }
    [field: SerializeField] public LootDrop Drop { get; private set; }

    public void Awake()
    {
        Movement.Init(this);
        Health.Init(this);
        Attack.Init(this);
    }

    public bool IsAlive => gameObject.activeInHierarchy;
    public event Action<Enemy> OnEnemyDie;

    //многие компоненты обращаются к нему, вынес отдельно
    public void TakeDamage(float value)
    {
        Health.TakeDamage(value);
    }

    public void Die()
    {
        OnEnemyDie?.Invoke(this);
    }

    public void OnReleaseToPool()
    {
        OnEnemyDie = null;
    }

    public void OnGetFromPool()
    {

    }
}
