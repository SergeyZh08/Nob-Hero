using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    [SerializeField] private List<ActiveEffect> _continuousEffects = new List<ActiveEffect>();
    [SerializeField] private List<ActiveEffect> _collectedContinuousEffects = new List<ActiveEffect>();

    [SerializeField] private List<PassiveEffect> _oneTimeEffects = new List<PassiveEffect>();
    [SerializeField] private List<PassiveEffect> _collectedOneTimeEffects = new List<PassiveEffect>();

    private CardManager _cardManager;
    private TopIconManager _topIconManager;
    private Player _player;

    public void Update()
    {
        for (int i = 0; i < _collectedContinuousEffects.Count; i++)
        {
            _collectedContinuousEffects[i].Tick(Time.deltaTime);
        }
    }

    public void Init(EnemySpawner enemySpawner, CardManager cardManager, TopIconManager topIconManager, Player player)
    {
        _cardManager = cardManager;
        _topIconManager = topIconManager;
        _player = player;

        for (int i = 0; i < _continuousEffects.Count; i++)
        {
            _continuousEffects[i] = Instantiate(_continuousEffects[i]);
            _continuousEffects[i].Init(_player, enemySpawner);
        }

        for (int i = 0; i < _oneTimeEffects.Count; i++)
        {
            _oneTimeEffects[i] = Instantiate(_oneTimeEffects[i]);
            _oneTimeEffects[i].Init(_player, enemySpawner);
        }
    }

    public void ShowEffect()
    {
        List<Effect> effectsForShow = new List<Effect>();

        for (int i = 0; i < _collectedContinuousEffects.Count; i++)
        {
            if (_collectedContinuousEffects[i].Level < 10)
            {
                effectsForShow.Add(_collectedContinuousEffects[i]);
            }
        }

        for (int i = 0; i < _collectedOneTimeEffects.Count; i++)
        {
            if (_collectedOneTimeEffects[i].Level < 10)
            {
                effectsForShow.Add(_collectedOneTimeEffects[i]);
            }
        }

        if (_collectedContinuousEffects.Count < 4)
        {
            effectsForShow.AddRange(_continuousEffects);
        }

        if (_collectedOneTimeEffects.Count < 4)
        {
            effectsForShow.AddRange(_oneTimeEffects);
        }

        int numberOfCardForShow = Mathf.Min(effectsForShow.Count, 3);

        int[] indexes = RandomSort(effectsForShow.Count, numberOfCardForShow);

        List<Effect> effectsForCards = new List<Effect>();

        for (int i = 0; i < indexes.Length; i++)
        {
            effectsForCards.Add(effectsForShow[indexes[i]]);
        }

        _cardManager.ShowCards(effectsForCards);
    }

    public void AddEffect(Effect effect)
    {
        if (effect is ActiveEffect c_effect)
        {
            if (!_collectedContinuousEffects.Contains(c_effect))
            {
                _collectedContinuousEffects.Add(c_effect);
                _continuousEffects.Remove(c_effect);
                _topIconManager.Add(c_effect);
            }
        }

        if (effect is PassiveEffect o_effect)
        {
            if (!_collectedOneTimeEffects.Contains(o_effect))
            {
                _collectedOneTimeEffects.Add(o_effect);
                _oneTimeEffects.Remove(o_effect);
                _topIconManager.Add(o_effect);
            }
        }

        effect.Activate();
    }

    private int[] RandomSort(int lenght, int number)
    {
        int[] arr = new int[lenght];

        for (int i = 0; i < arr.Length; i++)
        {
            arr[i] = i;
        }

        for (int i = 0; i < arr.Length; i++)
        {
            int randomIndex = Random.Range(0, arr.Length);

            int temp = arr[i];
            arr[i] = arr[randomIndex];
            arr[randomIndex] = temp;
        }

        int[] result = new int[number];

        for (int i = 0; i < result.Length; i++)
        {
            result[i] = arr[i];
        }

        return result;
    }
}
