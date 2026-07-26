using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameStateManager _gameStateManager;
    private EnemySpawner _enemySpawner;
    private Player _player;

    public void Init(GameStateManager gameStateManager, EnemySpawner enemySpawner, Player player)
    {
        _gameStateManager = gameStateManager;
        _enemySpawner = enemySpawner;
        _player = player;

        _player.Health.OnPlayerDie += SetLose;
        _enemySpawner.AllEnemiesDie += SetWin;
    }

    private void OnDestroy()
    {
        _player.Health.OnPlayerDie -= SetLose;
        _enemySpawner.AllEnemiesDie -= SetWin;
    }

    private void SetLose() => _gameStateManager.SetLose();
    private void SetWin() => _gameStateManager.SetWin();
}
