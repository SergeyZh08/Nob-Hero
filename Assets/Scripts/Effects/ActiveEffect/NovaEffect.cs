using UnityEngine;

[CreateAssetMenu(fileName = nameof(NovaEffect), menuName = ("ContinuousEffect/" + nameof(NovaEffect)))]
public class NovaEffect : ActiveEffect
{
    [SerializeField] private Nova _novaPrefab;
    private Nova _nova;

    protected override void FirstTimeActivate()
    {
        base.FirstTimeActivate();
        _nova = Instantiate(_novaPrefab);
        _nova.gameObject.SetActive(false);
    }

    protected override void Produce()
    {
        base.Produce();
        _nova.Init(ApplyDamageBoost(Current.Damage), ApplyRadiusBoost(Current.Radius), _player.transform.position);
    }
}
