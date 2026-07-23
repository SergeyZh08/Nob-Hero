using UnityEngine;

[CreateAssetMenu (fileName = nameof(ShadowMissilesEffect), menuName = ("ContinuousEffect/" + nameof(ShadowMissilesEffect)))]
public class ShadowMissilesEffect : ActiveEffect
{
    private Pool<ShadowMissiles> _pool;
    [SerializeField] private ShadowMissiles _shadowMissilesPrefab;

    protected override void FirstTimeActivate()
    {
        base.FirstTimeActivate();
        _pool = new Pool<ShadowMissiles>(_shadowMissilesPrefab, 3, 2, null);
    }

    protected override void Produce()
    {
        base.Produce();

        Vector3 position = _player.transform.position;

        int count = ApplyNumberBoost(Current.Number);
        
        for (int i = 0; i < count; i++)
        {
            Quaternion rotation = Quaternion.Euler(0, 360f / count * i, 0);
            ShadowMissiles newShadowMissiles = _pool.Get();
            newShadowMissiles.transform.SetPositionAndRotation(position, rotation);
            newShadowMissiles.Init(ApplyPassCountBoost(Current.PassCount), Current.Speed, ApplyDamageBoost(Current.Damage), Current.LifeTime, _pool.Release);
        }
    }
}
