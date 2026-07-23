using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrostAura : MonoBehaviour
{
    private float _dps;
    private Transform _target;
    private List<Enemy> _enemies = new List<Enemy>();
    private Coroutine _currentCoroutine;

    public void Init(float dps, Transform target)
    {
        _dps = dps;
        _target = target;

        if (_currentCoroutine == null)
        {
            _currentCoroutine = StartCoroutine(HitRoutine());
        }
    }

    private void LateUpdate()
    {
        transform.position = _target.position;
    }

    private IEnumerator HitRoutine()
    {
        float del = 0.2f;

        var delay = new WaitForSeconds(del);

        while (true)
        {
            for (int i = _enemies.Count - 1; i >= 0; i--)
            {
                _enemies[i].TakeDamage(_dps * del);
            }

            yield return delay;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Enemy enemy))
        {
            enemy.OnEnemyDie += RemoveEnemy;
            _enemies.Add(enemy);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Enemy enemy))
        {
            RemoveEnemy(enemy);
        }
    }

    private void RemoveEnemy(Enemy enemy)
    {
        enemy.OnEnemyDie -= RemoveEnemy;
        _enemies.Remove(enemy);
    }
}
