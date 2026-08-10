using System;
using UnityEngine;

[AttributeUsage(AttributeTargets.Field)]
public class StatNameAttribute : Attribute
{
    public string Name;

    public StatNameAttribute(string name)
    {
        Name = name;
    }
}
[System.Serializable]
public struct LevelStats
{
    [StatName("Cooldown")] public float Cooldown;

    [StatName("Damage")] public float Damage;
    [StatName("Radius")] public float Radius;
    [StatName("Number of effects")] public int Number;
    [StatName("Damage per second")] public float DPS;
    [StatName("Pass count")] public int PassCount;
    [StatName("Life time")] public float LifeTime;
    [StatName("Speed")] public float Speed;
}

public class ActiveEffect : Effect
{
    [SerializeField] private LevelStats[] _level = new LevelStats[10];
    public LevelStats Current => _level[Mathf.Clamp(Level - 1, 0, _level.Length - 1)];
    public LevelStats NextLevel => _level[Mathf.Clamp(Level, 0, _level.Length - 1)];
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

    //100% + player stats (ex.: val * (100% + 10%))
    protected float ApplyDamageBoost(float value) => value * (1 + _player.Stats.DamageBoost);
    protected float ApplyRadiusBoost(float value) => value * (1 + _player.Stats.RadiusBoost);
    protected float ApplyCooldownBoost(float value) => value * (1 - _player.Stats.CooldownBoost);
    protected int ApplyNumberBoost(int value) => value + _player.Stats.NumberBoost;
    protected int ApplyPassCountBoost(int value) => value + _player.Stats.PassCountBoost;
}
