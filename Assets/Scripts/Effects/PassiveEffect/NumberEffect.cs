using UnityEngine;

[CreateAssetMenu (fileName = nameof(NumberEffect), menuName = ("OneTimeEffect/" + nameof(NumberEffect)))]
public class NumberEffect : PassiveEffect
{
    [SerializeField] private int[] _boost;
    public override void Activate()
    {
        base.Activate();
        _player.Stats.NumberBoost += _boost[Level - 1];
    }
}
