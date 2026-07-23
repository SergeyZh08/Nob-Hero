using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    [SerializeField] private GameState _menuState;
    [SerializeField] private GameState _pauseState;
    [SerializeField] private GameState _winState;
    [SerializeField] private GameState _loseState;
    [SerializeField] private GameState _actionState;
    [SerializeField] private GameState _cardState;

    public void Init()
    {
        _menuState?.Init(this);
        _pauseState?.Init(this);
        _winState?.Init(this);
        _loseState?.Init(this);
        _actionState?.Init(this);
        _cardState?.Init(this);

        ChangeState(_menuState);
    }

    private GameState _currentState;

    private void ChangeState(GameState state)
    {
        if (_currentState == state)
        {
            return;
        }

        _currentState?.Exit();
        
        _currentState = state;

        _currentState?.Enter();
    }

    public void SetMenu()
    {
        ChangeState(_menuState);
    }

    public void SetPause()
    {
        ChangeState(_pauseState);
    }

    public void SetWin()
    {
        ChangeState(_winState);
    }
    
    public void SetLose()
    {
        ChangeState(_loseState);
    }

    public void SetAction()
    {
        ChangeState(_actionState);
    }

    public void SetCardState()
    {
        ChangeState(_cardState);
    }
}
