using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemyVisualProvider _enemyVisualProvider;
    [SerializeField] private ChapterSettings _enemyChapter;
    [SerializeField] private float _radiusForSpawn;
    [Header("Enemy Pool")]
    [SerializeField] private int _startSize;
    [SerializeField] private int _step;
    [SerializeField] private Transform _parent;

    private LootManager _lootManager;
    private Transform _player;

    private Dictionary<Enemy, Pool<Enemy>> _pools = new Dictionary<Enemy, Pool<Enemy>>();
    private Dictionary<Enemy, Pool<Enemy>> _enemyToPool = new Dictionary<Enemy, Pool<Enemy>>();
    private List<Enemy> _spawnedEnemies = new List<Enemy>();
    public event Action AllEnemiesDie;
    private bool _lastWave;
    private int _currentWave;

    public void Init(LootManager lootManager, Player player)
    {
        _lootManager = lootManager;
        _player = player.transform;
        _currentWave = -1;
    }

    public void NextWave()
    {
        StopAllCoroutines();

        _currentWave ++;

        if (_currentWave >= _enemyChapter.EnemyWaves[0].NumberPerSecund.Length)
        {
            _lastWave = true;
            //Debug.Log("true" + _currentWave);
            return;
        }

        //Debug.Log("false:" + _currentWave);

        for (int i = 0; i < _enemyChapter.EnemyWaves.Length; i++)
        {
            if (_enemyChapter.EnemyWaves[i].NumberPerSecund[_currentWave] > 0)
            {
                Enemy enemy = _enemyChapter.EnemyWaves[i].Enemy;
                if (!_pools.TryGetValue(enemy, out _))
                {
                    Pool<Enemy> pool = new Pool<Enemy>(enemy, _startSize, _step, _parent);
                    _pools.Add(enemy, pool);
                }

                StartCoroutine(SpawnRoutuine(_enemyChapter.EnemyWaves[i].Enemy, _enemyChapter.EnemyWaves[i].NumberPerSecund[_currentWave]));
            }
        }
    }

    private IEnumerator SpawnRoutuine(Enemy enemy, float enemyPerSecunds)
    {
        var delay = new WaitForSeconds(1 / enemyPerSecunds);

        while (true)
        {
            yield return delay;

            SpawnAndInit(enemy);
        }
    }

    private void SpawnAndInit(Enemy enemyPrefab)
    {
        Vector2 randomVector = UnityEngine.Random.insideUnitCircle.normalized;

        Vector3 position = _player.position + new Vector3(randomVector.x, 0, randomVector.y) * _radiusForSpawn;

        Enemy newEnemy = _pools[enemyPrefab].Get(e => e.transform.position = position);
        _enemyToPool[newEnemy] = _pools[enemyPrefab];

        newEnemy.OnEnemyDie += _enemyVisualProvider.StartEffect;
        newEnemy.OnEnemyDie += _lootManager.CreateLoot;
        newEnemy.OnEnemyDie += RemoveEnemy;

        newEnemy.Init(_player);
        _spawnedEnemies.Add(newEnemy);
    }

    private void RemoveEnemy(Enemy enemy)
    {
        enemy.OnEnemyDie -= _enemyVisualProvider.StartEffect;
        enemy.OnEnemyDie -= _lootManager.CreateLoot;
        enemy.OnEnemyDie -= RemoveEnemy;


        _spawnedEnemies.Remove(enemy);

        _enemyToPool[enemy].Release(enemy);
        _enemyToPool.Remove(enemy);

        if (_lastWave && _spawnedEnemies.Count == 0)
        {
            AllEnemiesDie?.Invoke();
        }
    }

    // private void OnDrawGizmos()
    // {
    //     Handles.color = Color.coral;
    //     Handles.DrawWireDisc(_player.position, Vector3.up, _radiusForSpawn);
    // }

    public Enemy[] GetClosest(Vector3 point, int count)
    {
        if (_spawnedEnemies != null)
        {
            _spawnedEnemies.Sort((a, b) =>
            {
                float ad = (a.transform.position - point).sqrMagnitude;
                float bd = (b.transform.position - point).sqrMagnitude;
                return ad.CompareTo(bd);
            });

            int len = Mathf.Min(count, _spawnedEnemies.Count);

            Enemy[] enemies = new Enemy[len];

            for (int i = 0; i < enemies.Length; i++)
            {
                enemies[i] = _spawnedEnemies[i];
            }

            return enemies;
        }

        return new Enemy[0];
    }
}
