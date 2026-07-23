using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(MeteorEffect), menuName = ("ContinuousEffect/" + nameof(MeteorEffect)))]
public class MeteorEffect : ActiveEffect
{
    private Pool<Meteor> _pool;
    [SerializeField] private Meteor _meteorEffect;
    private readonly WaitForSeconds _dealy = new WaitForSeconds(0.2f);

    protected override void FirstTimeActivate()
    {
        base.FirstTimeActivate();
        _pool = new Pool<Meteor>(_meteorEffect, 3, 2, null);
    }

    protected override void Produce()
    {
        base.Produce();
        _enemySpawner.StartCoroutine(CreateProcess());
    }

    private IEnumerator CreateProcess()
    {
        for (int i = 0; i < ApplyNumberBoost(Current.Number); i++)
        {
            Vector3 position = _player.transform.position + new Vector3(Random.insideUnitCircle.x, 0, Random.insideUnitCircle.y) * Random.Range(0, 10f);
            Meteor newMeteor = _pool.Get();
            newMeteor.transform.position = position;
            newMeteor.Init(ApplyDamageBoost(Current.Damage), ApplyRadiusBoost(Current.Radius), _pool.Release);
            yield return _dealy;
        }

    }
}
