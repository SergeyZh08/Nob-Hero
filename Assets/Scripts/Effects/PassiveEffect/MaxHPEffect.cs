using UnityEngine;

[CreateAssetMenu (fileName = nameof(MaxHPEffect), menuName = ("OneTimeEffect/" + nameof(MaxHPEffect)))]
public class MaxHPEffect : PassiveEffect
{
    [SerializeField] private float[] _boost;
    public override void Activate()
    {
        base.Activate();
        _player.Stats.AddMaxHPBoost(_boost[Level - 1]);
        _player.Health.RecalculateMaxHealth();
    }
}
