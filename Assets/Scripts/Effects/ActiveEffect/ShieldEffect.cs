using UnityEngine;

[CreateAssetMenu(fileName = nameof(ShieldEffect), menuName = ("ContinuousEffect/" + nameof(ShieldEffect)))]
public class ShieldEffect : ActiveEffect
{
    [SerializeField] private Shield _shieldPrefab;
    private Shield _shield;

    protected override void FirstTimeActivate()
    {
        base.FirstTimeActivate();
        _shield = Instantiate(_shieldPrefab, _player.transform.position, Quaternion.identity);
        _shield.Deactivate();

        _player.PlayerHealth.AddModifiers(_shield);
    }

    protected override void Produce()
    {
        base.Produce();
        _shield.Activate();
        _shield.Init(Current.LifeTime, _player.transform);
    }
}
