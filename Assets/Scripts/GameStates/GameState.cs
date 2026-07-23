using UnityEngine;

public class GameState : MonoBehaviour
{
protected GameStateManager _gameStateManager;

    private bool _wasSet;

    public virtual void Init(GameStateManager gameStateManager)
    {
        _gameStateManager = gameStateManager;
    }

    public virtual void EnterFirstTime()
    {
        
    }

    public virtual void Enter()
    {
        if (!_wasSet)
        {
            EnterFirstTime();
            _wasSet = true;
        }
    }

    public virtual void Exit()
    {
        
    }
}
