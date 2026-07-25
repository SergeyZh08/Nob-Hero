using UnityEngine;

public class ActionState : GameState
{
    [SerializeField] private Joystick _joystick;
    [SerializeField] private Player _player;
    [SerializeField] private EnemySpawner _enemySpawner;

    public override void Init(GameStateManager gameStateManager)
    {
        base.Init(gameStateManager);
    }

    public override void EnterFirstTime()
    {
        base.EnterFirstTime();
        _enemySpawner.NextWave(0);
        _player.Experience.UpdateNextLevelValue(0);
    }

    public override void Enter()
    {
        base.Enter();
        _joystick.Activate();
        _player.Move.enabled = true;
    }

    public override void Exit()
    {
        base.Exit();
        _joystick.Deactivate();
        _player.Move.enabled = false;
    }
}
