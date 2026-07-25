using UnityEngine;

[CreateAssetMenu (fileName = nameof(RegenerationEffect), menuName = ("OneTimeEffect/" + nameof(RegenerationEffect)))]
public class RegenerationEffect : PassiveEffect
{
    [SerializeField] private float[] _boost;
    public override void Activate()
    {
        base.Activate();
        _player.Stats.RegenerationBoost += _boost[Level - 1];
    }
}
