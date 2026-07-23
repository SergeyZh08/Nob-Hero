using UnityEngine;

[CreateAssetMenu (fileName = nameof(BlackHoleEffect), menuName = ("ContinuousEffect/" + nameof(BlackHoleEffect)))]
public class BlackHoleEffect : ActiveEffect
{
    [SerializeField] private BlackHole _blackHolePrefab; 
    private BlackHole _currentBlackHole;

    protected override void FirstTimeActivate()
    {
        base.FirstTimeActivate();
        _currentBlackHole = Instantiate(_blackHolePrefab);
        _currentBlackHole.Deactivate();
    }

    protected override void Produce()
    {
        base.Produce();

        _currentBlackHole.transform.position = _player.transform.position;
        _currentBlackHole.transform.localScale = Vector3.one * ApplyRadiusBoost(Current.Radius);
        _currentBlackHole.Activate();
        _currentBlackHole.Init(ApplyDamageBoost(Current.DPS), Current.LifeTime);
    }
}
