using UnityEngine;

public class LoseState : GameState
{
    [SerializeField] private LoseWindow _loseWindow;
    public override void Init(GameStateManager gameStateManager)
    {
        base.Init(gameStateManager);
    }

    public override void Enter()
    {
        base.Enter();
        Time.timeScale = 0f;
        _loseWindow.Show();
    }

    public override void Exit()
    {
        base.Exit();
        _loseWindow.Hide();
    }
}
