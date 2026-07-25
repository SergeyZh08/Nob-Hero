using UnityEngine;

[System.Serializable]
public struct LevelStats
{
    public float Cooldown;
    public float Damage;
    public float Radius;
    public int Number;
    public float DPS;
    public int PassCount;
    public float LifeTime;
    public float Speed;
}

public class ActiveEffect : Effect
{
    [SerializeField] private LevelStats[] _level = new LevelStats[10];
    public LevelStats Current => _level[Mathf.Clamp(Level - 1, 0, _level.Length - 1)];
    public override float CooldownProgress => _timer / ApplyCooldownBoost(Current.Cooldown);
    private float _timer = 0;

    public void Tick(float value)
    {
        _timer += value;

        if (_timer >= ApplyCooldownBoost(Current.Cooldown))
        {
            Produce();
            _timer = 0;
        }
    }

    protected virtual void Produce()
    {
        
    }

    protected float ApplyDamageBoost(float value) => value * (1 + _player.Stats.DamageBoost);
    protected float ApplyRadiusBoost(float value) => value * (1 + _player.Stats.RadiusBoost);
    protected float ApplyCooldownBoost(float value) => value * (1 - _player.Stats.CooldownBoost);
    protected int ApplyNumberBoost(int value) => value + _player.Stats.NumberBoost;
    protected int ApplyPassCountBoost(int value) => value + _player.Stats.PassCountBoost;
}
