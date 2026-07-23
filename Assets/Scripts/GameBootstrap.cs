using UnityEngine;

public class GameBootstrap : MonoBehaviour
{
    [SerializeField] private LootManager _lootManager;
    [SerializeField] private GameStateManager _gameStateManager;
    [SerializeField] private EffectManager _effectManager;
    [SerializeField] private ExperienceManager _experienceManager;
    [SerializeField] private CardManager _cardManager;
    [SerializeField] private PlayerHealth _playerHealth;
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private TopIconManager _topIconManager;
    [SerializeField] private Player _player;

    private void Awake()
    {
        _effectManager.Init(_enemySpawner, _cardManager, _topIconManager, _player);
        _cardManager.Init(_effectManager, _gameStateManager);
        _experienceManager.Init();
        _gameStateManager.Init();
        _enemySpawner.Init(_lootManager, _player);
        _lootManager.Init();

        _playerHealth.OnPlayerDie += _gameStateManager.SetLose;
    }

    private void OnDestroy()
    {
        _playerHealth.OnPlayerDie -= _gameStateManager.SetLose;
    }
}
