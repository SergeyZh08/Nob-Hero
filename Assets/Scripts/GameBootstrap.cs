using System.Collections.Generic;
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
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private SaveManager _saveManager;
    [SerializeField] private ChapterManager _waweManager;

    private void Awake()
    {
        _gameManager.Init(_gameStateManager, _enemySpawner, _player, _waweManager, _saveManager);
        _player.Init();
        _effectManager.Init(_enemySpawner, _cardManager, _topIconManager, _player);
        _cardManager.Init(_effectManager, _gameStateManager, _enemySpawner);
        _gameStateManager.Init();
        _lootManager.Init();
        _levelupManager.Init();
        _saveManager.Init();

        _saveManager.Register(_player.Inventory);
        _saveManager.Register(_waweManager);
        _saveManager.Load();

        _enemySpawner.Init(_lootManager, _player, _waweManager);
    }
}
