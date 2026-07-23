using UnityEngine;
using UnityEngine.UI;

public class MenuState : GameState
{
    [SerializeField] private Button _startButton;
    [SerializeField] private GameObject _menu;

    public override void Init(GameStateManager gameStateManager)
    {
        base.Init(gameStateManager);
        _startButton.onClick.AddListener(gameStateManager.SetAction);
    }

    public override void Enter()
    {
        base.Enter();
        _menu.SetActive(true);
        Time.timeScale = 0f;
    }

    public override void Exit()
    {
        base.Exit();
        _menu.SetActive(false);
        Time.timeScale = 1f;
    }
}
