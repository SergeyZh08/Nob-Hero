using UnityEngine;

public class ProgressCardsManager : MonoBehaviour, ISaved
{
    [SerializeField] private ProgressCard[] _progressCards;
    private SaveManager _saveManager;

    public void Init(Player player, SaveManager saveManager)
    {
        _saveManager = saveManager;
        for (int i = 0; i < _progressCards.Length; i++)
        {
            _progressCards[i].Init(player);
            _progressCards[i].OnBuy += RecalculateAndSave;
        }
    }

    private void OnDestroy()
    {
        for (int i = 0; i < _progressCards.Length; i++)
        {
            _progressCards[i].OnBuy -= RecalculateAndSave;
        }
    }

    public void RecalculateAndSave()
    {
        for (int i = 0; i < _progressCards.Length; i++)
        {
            _progressCards[i].CalculateCost();
        }

        _saveManager.Save();
    }

    public void LoadFrom(SaveData data)
    {
        //Если нет файла, или добавлена новая карточка (на будущее), или данные изменятся
        if (data.ProgressDataLevels == null || data.ProgressDataLevels.Length != _progressCards.Length)
        {
            int[] levels = new int[_progressCards.Length];

            if (data.ProgressDataLevels != null)
            {
                for (int i = 0; i < data.ProgressDataLevels.Length; i++)
                {
                    levels[i] = data.ProgressDataLevels[i];
                }
            }

            data.ProgressDataLevels = levels;
        }

        for (int i = 0; i < _progressCards.Length; i++)
        {
            _progressCards[i].SetLevel(data.ProgressDataLevels[i]);
        }
    }

    public void SaveTo(SaveData data)
    {
        data.ProgressDataLevels = new int[_progressCards.Length];

        for (int i = 0; i < _progressCards.Length; i++)
        {
            data.ProgressDataLevels[i] = _progressCards[i].Config.Level;
        }
    }
}
