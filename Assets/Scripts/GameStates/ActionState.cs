using UnityEngine;

public class ActionState : GameState
{
    [SerializeField] private Joystick _joystick;
    [SerializeField] private PlayerMove _playerMove;
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private ExperienceManager _experienceManager;

    public override void Init(GameStateManager gameStateManager)
    {
        base.Init(gameStateManager);
    }

    public override void EnterFirstTime()
    {
        base.EnterFirstTime();
        _enemySpawner.NextWave(0);
        _experienceManager.UpdateNextLevelValue(0);
    }

    public override void Enter()
    {
        base.Enter();
        _joystick.Activate();
        _playerMove.enabled = true;
    }

    public override void Exit()
    {
        base.Exit();
        _joystick.Deactivate();
        _playerMove.enabled = false;
    }
}
