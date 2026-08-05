using UnityEngine;

public class WinState : GameState
{
    [SerializeField] private WinWindow _winWindow;
    public override void Enter()
    {
        base.Enter();
        Time.timeScale = 0f;
        _winWindow.Show();
    }

    public override void Exit()
    {
        base.Exit();
        Time.timeScale = 1f;
        _winWindow.Hide();
    }
}
