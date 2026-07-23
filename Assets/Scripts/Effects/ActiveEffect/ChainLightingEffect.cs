using System.Collections;
using UnityEngine;

[CreateAssetMenu (fileName = nameof(ChainLightingEffect), menuName = ("ContinuousEffect/" + nameof(ChainLightingEffect)))]
public class ChainLightingEffect : ActiveEffect
{
    private Pool<ChainLighting> _pool;
    [SerializeField] private ChainLighting _chainLightingPrefab;
    private readonly WaitForSeconds _delay = new WaitForSeconds(0.2f);

    protected override void FirstTimeActivate()
    {
        base.FirstTimeActivate();
        _pool = new Pool<ChainLighting>(_chainLightingPrefab, 3, 2, null);
    }

    protected override void Produce()
    {
        base.Produce();

        _enemySpawner.StartCoroutine(ChainLightingProcess());
    }

    private IEnumerator ChainLightingProcess()
    {
        Enemy[] enemies = _enemySpawner.GetClosest(_player.transform.position, ApplyNumberBoost(Current.Number));

        for (int i = 0; i < enemies.Length; i++)
        {
            ChainLighting newLinghting = _pool.Get();
            newLinghting.transform.position = _player.transform.position;
            newLinghting.Init(ApplyPassCountBoost(Current.PassCount), Current.Speed, ApplyDamageBoost(Current.Damage), enemies[i], _pool.Release);
            yield return _delay;
        }
    }
}
