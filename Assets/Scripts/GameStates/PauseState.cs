using UnityEngine;
using UnityEngine.UI;

public class PauseState : GameState
{
    [SerializeField] private Button _continueButton;
    [SerializeField] private Button _pauseButton;

    public override void Init(GameStateManager gameStateManager)
    {
        base.Init(gameStateManager);
        _continueButton.onClick.AddListener(gameStateManager.SetAction);
        _pauseButton.onClick.AddListener(gameStateManager.SetPause);
    }

    public override void Enter()
    {
        base.Enter();
        Time.timeScale = 0f;
        _continueButton.gameObject.SetActive(true);
        _pauseButton.gameObject.SetActive(false);
    }

    public override void Exit()
    {
        base.Exit();
        Time.timeScale = 1f;
        _continueButton.gameObject.SetActive(false);
        _pauseButton.gameObject.SetActive(true);
    }
}
