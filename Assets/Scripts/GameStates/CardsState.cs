using UnityEngine;

public class CardsState : GameState
{
    public override void Init(GameStateManager gameStateManager)
    {
        base.Init(gameStateManager);
    }

    public override void Enter()
    {
        base.Enter();
        Time.timeScale = 0f;
    }

    public override void Exit()
    {
        base.Exit();
        Time.timeScale = 1f;
    }
}
