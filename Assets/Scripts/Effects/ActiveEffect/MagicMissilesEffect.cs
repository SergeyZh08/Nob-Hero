using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(MagicMissilesEffect), menuName = ("ContinuousEffect/" + nameof(MagicMissilesEffect)))]
public class MagicMissilesEffect : ActiveEffect
{
    private Pool<MagicMissiles> _pool;
    [SerializeField] private MagicMissiles _magicMisslesPrefab;
    private readonly WaitForSeconds _delay = new WaitForSeconds(0.2f);

    protected override void FirstTimeActivate()
    {
        base.FirstTimeActivate();
        _pool = new Pool<MagicMissiles>(_magicMisslesPrefab, 4, 4, null);
    }

    protected override void Produce()
    {
        base.Produce();

        _enemySpawner.StartCoroutine(MagicMisslesProcess());
    }

    private IEnumerator MagicMisslesProcess()
    {
        Enemy[] enemies = _enemySpawner.GetClosest(_player.transform.position, ApplyNumberBoost(Current.Number));

        if (enemies.Length > 0)
        {
            for (int i = 0; i < enemies.Length; i++)
            {
                MagicMissiles newMagicMissles = _pool.Get();
                newMagicMissles.transform.position = _player.transform.position;
                newMagicMissles.Init(Current.Speed, ApplyDamageBoost(Current.Damage), enemies[i], _pool.Release);
                
                yield return _delay;
            }
        }
    }
}
