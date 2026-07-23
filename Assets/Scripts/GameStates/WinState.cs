using UnityEngine;

public class WinState : GameState
{
    [SerializeField] private WinWindow _winWindow;
    public override void Enter()
    {
        base.Enter();
        _winWindow.Show();
    }
}
