using UnityEngine;

[CreateAssetMenu (fileName = nameof(MagnetEffect), menuName = ("OneTimeEffect/" + nameof(MagnetEffect)))]
public class MagnetEffect : PassiveEffect
{
    [SerializeField] private float[] _boost;
    public override void Activate()
    {
        base.Activate();
        _player.Stats.MagnetBoost += _boost[Level - 1];
    }
}
