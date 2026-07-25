using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public Player Player { get; private set; }

    public void Init(Player player)
    {
        Player = player;
    }
    
    public float CooldownBoost;
    public float DamageBoost;
    public float RadiusBoost;
    public float RegenerationBoost;
    public float MagnetBoost;
    public float MaxHPBoost;
    public int NumberBoost;
    public int PassCountBoost;
    public float MovementSpeed;
}
