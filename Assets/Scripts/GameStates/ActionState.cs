using UnityEngine;

public class ActionState : GameState
{
    [SerializeField] private Joystick _joystick;
    [SerializeField] private Player _player;
    
    [SerializeField] private LevelupManager _levelupManager;

    public override void Init(GameStateManager gameStateManager)
    {
        base.Init(gameStateManager);
    }

    public override void EnterFirstTime()
    {
        base.EnterFirstTime();
        _levelupManager.StartLeveling();
    }

    public override void Enter()
    {
        base.Enter();
        //_joystick.Activate();
        _player.Move.enabled = true;
    }

    public override void Exit()
    {
        base.Exit();
        //_joystick.Deactivate();
        _player.Move.enabled = false;
    }
}
