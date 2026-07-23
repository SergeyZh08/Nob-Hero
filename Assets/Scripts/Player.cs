using UnityEngine;

public class Player : MonoBehaviour
{
    [field: SerializeField] public PlayerHealth PlayerHealth { get; private set;}
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
