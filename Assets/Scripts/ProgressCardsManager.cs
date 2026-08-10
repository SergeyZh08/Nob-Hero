using UnityEngine;

public class ProgressCardsManager : MonoBehaviour, ISaved
{
    [SerializeField] private ProgressCard[] _progressCards;

    public void Init(Player player)
    {
        for (int i = 0;i < _progressCards.Length; i++)
        {
            _progressCards[i].Init(player);
        }
    }

    public void LoadFrom(SaveData data)
    {
        for (int i = 0;i < _progressCards.Length; i++)
        {
            _progressCards[i].SetLevel(data.ProgressDataLevels[i]);
        }
    }

    public void SaveTo(SaveData data)
    {
        data.ProgressDataLevels = new int[_progressCards.Length];

        for (int i = 0;i < _progressCards.Length; i++)
        {
            data.ProgressDataLevels[i] = _progressCards[i].Level;
        }
    }
}
