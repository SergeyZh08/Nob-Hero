using UnityEngine;

public abstract class Effect : ScriptableObject
{
    public virtual float CooldownProgress => 0f;
    public string Name;
    public string Description;
    public Sprite Sprite;
    public int Level;
    protected Player _player;
    protected EnemySpawner _enemySpawner;

    public void Init(Player player, EnemySpawner enemySpawner)
    {
        _player = player;
        _enemySpawner = enemySpawner;
    }

    public virtual void Activate()
    {
        Level++;

        if (Level == 1)
        {
            FirstTimeActivate();
        }
    }

    protected virtual void FirstTimeActivate()
    {
        
    }
}
