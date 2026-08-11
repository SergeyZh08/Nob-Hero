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

    public void Init(Player player)
    {
        Player = player;
        PermanentStats = new PermanentStats();
    }
    
    public float CooldownBoost;
    public float DamageBoost
    {
        get
        {
            return _damage + PermanentStats.Damage;
        }
        set
        {
            _damage = value;
        }
    }

    public float MaxHPBoost
    {
        get
        {
            return _maxHP + PermanentStats.Health;
        }
        set
        {
            _maxHP = value;
        }
    }

    public float MovementSpeed
    {
        get
        {
            return _speed + PermanentStats.Speed;
        }
        set
        {
            _speed = value;
        }
    }

    public float RadiusBoost;
    public float RegenerationBoost;
    public float MagnetBoost;
    
    public int NumberBoost;
    public int PassCountBoost;

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
