using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : MonoBehaviour
{
    [SerializeField] private float _timeForChangeDirection;
    private List<Enemy> _enemies = new List<Enemy>();
    private Vector3 _startPosition;
    private Vector3 _direction;
    private float _timer;
    private float _dps;
    private float _lifeTIme;


    public void Init(float dps, float lifeTime)
    {
        _dps = dps;
        _lifeTIme = lifeTime;
        _direction = ChangeDirection();
        StartCoroutine(DieProcess(_lifeTIme));
        StartCoroutine(HitRoutine());
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer > _timeForChangeDirection)
        {
            _direction = ChangeDirection();
            _timer = 0;
        }

        transform.position = Vector3.Lerp(_startPosition, _direction, _timer / _timeForChangeDirection);
    }

    private Vector3 ChangeDirection()
    {
        Vector2 randomVector = UnityEngine.Random.insideUnitCircle.normalized * 2;
        _startPosition = transform.position;
        return transform.position + new Vector3(randomVector.x, 0, randomVector.y);
    }

    private IEnumerator DieProcess(float lifeTime)
    {
        yield return new WaitForSeconds(lifeTime);

        for (float t = 0; t <= 1; t += Time.deltaTime)
        {
            transform.localScale = transform.localScale * (1 - t);
            yield return null;
        }

        Deactivate();
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

    public void Activate()
    {
        gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        _enemies.Clear();
        gameObject.SetActive(false);
    }
}
