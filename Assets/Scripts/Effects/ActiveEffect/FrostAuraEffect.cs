using UnityEngine;

[CreateAssetMenu(fileName = nameof(FrostAuraEffect), menuName = ("ContinuousEffect/" + nameof(FrostAuraEffect)))]

public class FrostAuraEffect : ActiveEffect
{
    [SerializeField] private FrostAura _frostAuraPrefab;
    private FrostAura _frostAura;

    protected override void FirstTimeActivate()
    {
        base.FirstTimeActivate();
        _frostAura = Instantiate(_frostAuraPrefab, _player.transform.position, Quaternion.identity);
        SetLevel();
    }

    public override void Activate()
    {
        base.Activate();
        SetLevel();
    }

    private void SetLevel()
    {
        _frostAura.Init(ApplyDamageBoost(Current.DPS), _player.transform);
        _frostAura.transform.localScale = Vector3.one * ApplyRadiusBoost(Current.Radius);
    }
}
