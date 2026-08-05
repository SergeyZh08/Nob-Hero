using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameStateManager _gameStateManager;
    private EnemySpawner _enemySpawner;
    private ChapterManager _waweManager;
    private SaveManager _saveManager;
    private Player _player;

    public void Init(GameStateManager gameStateManager, EnemySpawner enemySpawner, Player player, ChapterManager waweManager, SaveManager saveManager)
    {
        _gameStateManager = gameStateManager;
        _enemySpawner = enemySpawner;
        _player = player;
        _saveManager = saveManager;
        _waweManager = waweManager;

        _player.Health.OnPlayerDie += SetLose;
        _enemySpawner.AllEnemiesDie += SetWin;
    }

    private void OnDestroy()
    {
        _player.Health.OnPlayerDie -= SetLose;
        _enemySpawner.AllEnemiesDie -= SetWin;
    }

    private void SetLose() => _gameStateManager.SetLose();
    private void SetWin()
    {
        _waweManager.NextWawe();
        _saveManager.Save();
        _gameStateManager.SetWin();
    }
}
