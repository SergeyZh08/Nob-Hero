using UnityEngine;

[CreateAssetMenu (fileName = nameof(BombEffect), menuName = ("ContinuousEffect/" + nameof(BombEffect)))]

public class BombEffect : ActiveEffect
{
    [SerializeField] private Bomb _bombPrefab;
    private Pool<Bomb> _pool;

    protected override void FirstTimeActivate()
    {
        base.FirstTimeActivate();
        _pool = new Pool<Bomb>(_bombPrefab, 3, 2);
    }

    protected override void Produce()
    {
        base.Produce();

        Bomb newBomb = _pool.Get();
        newBomb.transform.position = _player.transform.position;
        newBomb.Init(ApplyRadiusBoost(Current.Radius), ApplyDamageBoost(Current.Damage), Current.LifeTime, _pool.Release);
    }
}
