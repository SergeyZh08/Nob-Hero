using UnityEngine;

public class Player : MonoBehaviour
{
    [field: SerializeField] public Collector Collector {get; private set;}
    [field: SerializeField] public PlayerExperience Experience {get; private set;}
    [field: SerializeField] public PlayerHealth Health {get; private set;}
    [field: SerializeField] public PlayerMove Move {get; private set;}
    [field: SerializeField] public PlayerStats Stats {get; private set;}
    [field: SerializeField] public PlayerInventory Inventory {get; private set;}
    [field: SerializeField] public PlayerAnimation Animation {get; private set;}

    public void Init()
    {
        Collector.Init(this);
        Experience.Init(this);
        Health.Init(this);
        Move.Init(this);
        Stats.Init(this);
        Inventory.Init(this);
        Animation.Init(this);
    }
}
