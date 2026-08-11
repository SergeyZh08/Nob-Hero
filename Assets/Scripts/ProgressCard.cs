using System;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ProgressCardConfig
{
    [HideInInspector] public int Level;
    public int MaxLevel;
    [HideInInspector] public int CurrentCost;
    public int StartCost;
    public float Percent = 0.05f;
}

public class ProgressCard : MonoBehaviour
{
    [field: SerializeField] public ProgressCardConfig Config {get; private set;}
    [SerializeField] private Button _button;
    public event Action OnBuy;
    public event Action<ProgressCardConfig, int> OnCostChanged;
    public Player Player {get; private set;}

    public void Init(Player player)
    {
        Player = player;
        _button.onClick.AddListener(Buy);
    }

    public void SetLevel(int level)
    {
        Config.Level = level;
        CalculateCost();
    }

    public void CalculateCost()
    {
        Config.CurrentCost = Config.StartCost + (Config.StartCost * Config.Level);
        OnCostChanged?.Invoke(Config, Player.Inventory.CoinCount);
    }

    private void Buy()
    {
        if (Player.Inventory.CoinCount < Config.CurrentCost || Config.Level >= Config.MaxLevel)
        {
            return;
        }

        Player.Inventory.SpendCoin(Config.CurrentCost);

        OnBought();
    }

    protected virtual void OnBought()
    {
        Config.Level++;

        OnBuy?.Invoke();
    }

    [ContextMenu("Buy")]
    public void TestBuy()
    {
        OnBought();
    }
}
