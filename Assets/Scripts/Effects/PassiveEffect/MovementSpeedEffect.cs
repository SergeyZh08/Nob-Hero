using UnityEngine;

[CreateAssetMenu (fileName = nameof(MovementSpeedEffect), menuName = ("OneTimeEffect/" + nameof(MovementSpeedEffect)))]
public class MovementSpeedEffect : PassiveEffect
{
    [SerializeField] private float[] _boost;
    public override void Activate()
    {
        base.Activate();
        _player.Stats.AddSpeedBoost(_boost[Level - 1]);
    }
}
