using UnityEngine;

[CreateAssetMenu (fileName = nameof(CooldownEffect), menuName = ("OneTimeEffect/" + nameof(CooldownEffect)))]

public class CooldownEffect : PassiveEffect
{
    [SerializeField] private float[] _boost;
    public override void Activate()
    {
        base.Activate();
        _player.Stats.AddCooldownBoost(_boost[Level - 1]);
    }
}