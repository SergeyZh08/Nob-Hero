using UnityEngine;

[CreateAssetMenu (fileName = nameof(PassCountEffect), menuName = ("OneTimeEffect/" + nameof(PassCountEffect)))]

public class PassCountEffect : PassiveEffect
{
    [SerializeField] private int[] _boost;
    public override void Activate()
    {
        base.Activate();
        _player.PassCountBoost += _boost[Level - 1];
    }
}
