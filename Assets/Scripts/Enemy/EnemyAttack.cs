using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public Enemy Enemy {get; private set;}
    [SerializeField] private float _dps;
    [SerializeField] private float _attackPeriod;
    [SerializeField] private float _radiusForAttack;
    private float _timer;
    private Player _player;

    public void Init(Enemy enemy)
    {
        Enemy = enemy;
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

    private void Attack()
    {
        _player.Health.TakeDamage(_dps * _attackPeriod);
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

    private void OnEnable()
    {
        _timer = 0;
    }

    private void OnDisable()
    {
        _player = null;
    }
}
