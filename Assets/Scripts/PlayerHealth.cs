using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct PlayerHealthData
{
    public float MaxHealth;
    public float Health;
}

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private PlayerHealthData _playerHealthData;
    [SerializeField] private Player _player;
    public event Action<PlayerHealthData> OnHealthChanged;
    public event Action OnPlayerDie;
    private float _timeForRegeneration = 1f;
    private float _timer = 0f;
    private float _startMaxHp;
    private List<IPlayerHealthModifier> _healthModifiers = new List<IPlayerHealthModifier>();

    private void Start()
    {
        SetHealth(_playerHealthData.Health);
        _startMaxHp = _playerHealthData.MaxHealth;
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer >= _timeForRegeneration)
        {
            Regeneration();
            _timer = 0;
        }
    }

    public void TakeDamage(float value)
    {
        foreach (var modifier in _healthModifiers)
        {
            value = modifier.ModifyDamage(value);
        }

        float newHealth = _playerHealthData.Health - value;
        newHealth = Mathf.Max(newHealth, 0);

        SetHealth(newHealth);

        if (newHealth == 0)
        {
            Die();
        }
    }

    private void Regeneration()
    {
        if (_playerHealthData.Health < _playerHealthData.MaxHealth)
        {
            float newHealth = _playerHealthData.Health + _player.RegenerationBoost;
            newHealth = Mathf.Min(newHealth, _playerHealthData.MaxHealth);
            SetHealth(newHealth);
        }
    }

    private void SetHealth(float health)
    {
        _playerHealthData.Health = health;
        OnHealthChanged?.Invoke(_playerHealthData);
    }

    public void SetBoostHp()
    {
        _playerHealthData.MaxHealth = _startMaxHp * (1 + _player.MaxHPBoost);
        OnHealthChanged?.Invoke(_playerHealthData);
    }

    public void AddModifiers(IPlayerHealthModifier modifier)
    {
        _healthModifiers.Add(modifier);
    }

    public void Die()
    {
        OnPlayerDie?.Invoke();
    }
}
