using UnityEngine;

[CreateAssetMenu (fileName = nameof(RadiusEffect), menuName = ("OneTimeEffect/" + nameof(RadiusEffect)))]

public class RadiusEffect : PassiveEffect
{
    [SerializeField] private float[] _boost;
    public override void Activate()
    {
        base.Activate();
        _player.Stats.RadiusBoost += _boost[Level - 1];
    }
}
