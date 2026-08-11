using UnityEngine;

[System.Serializable]
public class PermanentStats
{
    public float Health;
    public float Damage;
    public float Speed;
}

public class PlayerStats : MonoBehaviour, ISaved
{
    public Player Player { get; private set; }
    public PermanentStats PermanentStats {get; private set;}
    private float _damage = 0;
    private float _speed = 0;
    private float _maxHP = 0;
    private float _cooldown = 0;
    private float _radius = 0;
    private float _regeneration = 0;
    private float _magnet = 0;
    private int _number = 0;
    private int _passCount = 0;

    public void Init(Player player)
    {
        Player = player;
        PermanentStats = new PermanentStats();
    }
    
    public float CooldownBoost => _cooldown;
    public float DamageBoost => _damage + PermanentStats.Damage;

    public float MaxHPBoost => _maxHP + PermanentStats.Health;

    public float MovementSpeed => _speed + PermanentStats.Speed;

    public float RadiusBoost => _radius;
    public float RegenerationBoost => _regeneration;
    public float MagnetBoost => _magnet;
    
    public int NumberBoost => _number;
    public int PassCountBoost => _passCount;

    public void AddMaxHPBoost(float value)
    {
        _maxHP += value;
    }

    public void AddSpeedBoost(float value)
    {
        _speed += value;
    }

    public void AddDamageBoost(float value)
    {
        _damage += value;
    }

    public void AddCooldownBoost(float value)
    {
        _cooldown += value;
    }

    public void AddRadiusBoost(float value)
    {
        _radius += value;
    }

    public void AddRegenerationBoost(float value)
    {
        _regeneration += value;
    }

    public void AddMagnetBoost(float value)
    {
        _magnet += value;
    }

    public void AddNumberBoost(int value)
    {
        _number += value;
    }

    public void AddPassCountBoost(int value)
    {
        _passCount += value;
    }

    public void AddPermanentHealth(float value)
    {
        PermanentStats.Health += value;
        Debug.Log(PermanentStats.Health);
    }

    public void AddPermanentDamage(float value)
    {
        PermanentStats.Damage += value;
    }

    public void AddPermanentSpeed(float value)
    {
        PermanentStats.Speed += value;
    }

    public void SaveTo(SaveData data)
    {
        data.PermanentStats = new PermanentStats()
        {
            Health = PermanentStats.Health,
            Damage = PermanentStats.Damage,
            Speed = PermanentStats.Speed
        };
    }

    public void LoadFrom(SaveData data)
    {
        if (data.PermanentStats == null)
        {
            data.PermanentStats = new PermanentStats();
        }

        PermanentStats.Health = data.PermanentStats.Health;
        PermanentStats.Damage = data.PermanentStats.Damage;
        PermanentStats.Speed = data.PermanentStats.Speed;
    }
}
