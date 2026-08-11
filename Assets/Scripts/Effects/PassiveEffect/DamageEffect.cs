using UnityEngine;

[CreateAssetMenu (fileName = nameof(DamageEffect), menuName = ("OneTimeEffect/" + nameof(DamageEffect)))]

public class DamageEffect : PassiveEffect
{
    [SerializeField] private float[] _boost;
    public override void Activate()
    {
        base.Activate();
        _player.Stats.AddDamageBoost(_boost[Level - 1]);
    }
}
