using UnityEngine;

public class GameBootstrap : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private LootManager _lootManager;
    [SerializeField] private GameStateManager _gameStateManager;
    [SerializeField] private EffectManager _effectManager;
    [SerializeField] private CardManager _cardManager;
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private TopIconManager _topIconManager;
    [SerializeField] private LevelupManager _levelupManager;

    private void Awake()
    {
        _player.Init();
        _effectManager.Init(_enemySpawner, _cardManager, _topIconManager, _player);
        _cardManager.Init(_effectManager, _gameStateManager);
        _gameStateManager.Init();
        _enemySpawner.Init(_lootManager, _player);
        _lootManager.Init();
        _levelupManager.Init();

        _player.Health.OnPlayerDie += _gameStateManager.SetLose;
    }

    private void OnDestroy()
    {
        _player.Health.OnPlayerDie -= _gameStateManager.SetLose;
    }
}
